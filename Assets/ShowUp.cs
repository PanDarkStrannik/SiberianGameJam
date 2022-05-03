using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowUp : MonoBehaviour
{
    private Image[] _images;

    private Dictionary<Image, Color> _imageToColor=new();

    private Camera _mainCamera;

    private const int toStartShow = 1;

    private const float alphaChangeStep = 0.01f;


    private void Awake()
    {
        _images = GetComponentsInChildren<Image>();
        _mainCamera=Camera.main;
        foreach (var image in _images)
        {
            var prevColor = image.color;
            _imageToColor.Add(image,prevColor);
            image.color = new Color(prevColor.r, prevColor.g, prevColor.b, 0);
        }
    }


    private void Update()
    {
        if (Vector3.Distance(_mainCamera.transform.position, transform.position) > toStartShow)
        {
            DecreaseAlpha();
        }
        else
        {
            IncreaseAlpha();
        }
    }

    private void IncreaseAlpha()
    {
        if (_images.All(image => image.color.a >= 1))
            return;
        foreach (var (image, color) in _imageToColor)
        {
            var prevColorAlpha = image.color.a;
            var changeAplha = Math.Clamp(prevColorAlpha - alphaChangeStep, 0, 1);
            image.color = new Color(color.r, color.g, color.b, changeAplha);
        }
    }

    private void DecreaseAlpha()
    {
        if(_images.All(image=>image.color.a <= 0))
            return;
        foreach (var (image, color) in _imageToColor)
        {
            var prevColorAlpha = image.color.a;
            var changeAplha = Math.Clamp(prevColorAlpha + alphaChangeStep, 0, 1);
            image.color = new Color(color.r, color.g, color.b, changeAplha);
        }
    }
}
