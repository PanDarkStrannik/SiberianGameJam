using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class RectTransformExtensions
{
    /// <summary>
    ///     Returns a Rect in WorldSpace dimensions using <see cref="RectTransform.GetWorldCorners" />
    /// </summary>
    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        // This returns the world space positions of the corners in the order
        // [0] bottom left,
        // [1] top left
        // [2] top right
        // [3] bottom right
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector2 min = corners[0];
        Vector2 max = corners[2];
        var size = max - min;
        return new Rect(min, size);
    }

    /// <summary>
    ///     Checks if a <see cref="RectTransform" /> fully encloses another one
    /// </summary>
    public static bool FullyContains(this RectTransform rectTransform, RectTransform other)
    {
        return FullyContains(rectTransform, other.GetWorldRect());
    }

    public static bool FullyContains(this RectTransform rectTransform, Rect otherRect)
    {
        var rect = rectTransform.GetWorldRect();

        return rect.xMin <= otherRect.xMin && rect.yMin <= otherRect.yMin && rect.xMax >= otherRect.xMax && rect.yMax >= otherRect.yMax;
    }

    public static bool Overlaps(this RectTransform rectTransform, Rect otherRect)
    {
        var rect = rectTransform.GetWorldRect();
        return rect.Overlaps(otherRect);
    }

    public static Vector2 ClampPosition(this RectTransform canvasRectTransform, Vector2 position, Vector2 size)
    {
        var canvasRect = canvasRectTransform.GetWorldRect();

        var clampedX = Math.Clamp(position.x, canvasRect.xMin + size.x / 2, canvasRect.xMax - size.x / 2);
        var clampedY = Math.Clamp(position.y, canvasRect.yMin + size.y / 2, canvasRect.yMax - size.y / 2);
        return new Vector2(clampedX, clampedY);
    }

    public static Vector3 ClampPosition(this RectTransform canvasRectTransform, Vector3 position, Vector2 size)
    {
        var clamped = ClampPosition(canvasRectTransform, new Vector2(position.x, position.y), size);
        return new Vector3(clamped.x, clamped.y, canvasRectTransform.position.z);
    }
}

public static class TMP_TextEx
{
    public static void Replace<T>(this TMP_Text target, string toReplace, T value)
    {
        target.text = target.text.Replace(toReplace, value.ToString());
    }
}

public class Linker<Cell,Plate>
{
    public delegate void LinkerEvent(Cell cell, Plate plate);

    private HashSet<Link> _links = new();

    public int Count => _links.Count;

    public event LinkerEvent onAdd;
    public event LinkerEvent onRemove;

    public void Add(Cell cell, Plate plate)
    {
        _links.Add(new(cell, plate));
        onAdd?.Invoke(cell, plate);
    }

    public void Remove(Link link)
    {
        _links.Remove(link);
        onRemove?.Invoke(link.cell, link.plate);
    }

    public Link Find(Cell cell) { return _links.FirstOrDefault(c => c.cell.Equals(cell)); }
    public Link Find(Plate plate) { return _links.FirstOrDefault(c => c.plate.Equals(plate)); }

    public bool Contains(Cell cell) { return Find(cell) != null; }
    public bool Contains(Plate plate) { return Find(plate) != null; }

    public Link Extract(Cell cell)
    {
        var result = Find(cell);
        if (result != null) Remove(result);
        return result;
    }

    public Link Extract(Plate plate)
    {
        var result = Find(plate);
        if (result != null) Remove(result);
        return result;
    }

    public class Link
    {
        public readonly Cell cell;
        public readonly Plate plate;

        public Link(Cell cell, Plate plate)
        {
            this.cell = cell;
            this.plate = plate;
        }
    }
}
