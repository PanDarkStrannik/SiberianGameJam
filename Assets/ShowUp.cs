using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    private SpriteRenderer[] _images;

    private Dictionary<SpriteRenderer, Color> _imageToColor=new();

    private Camera _mainCamera;

    private BalanceData balance => GameManager.instance.balance;


    private void Awake()
    {
        _images = GetComponentsInChildren<SpriteRenderer>();
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
        if (Vector3.Distance(_mainCamera.transform.position, transform.position) > balance.distanceToShow)
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
            var changeAplha = Math.Clamp(prevColorAlpha + balance.alphaChangeStep, 0, 1);
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
            var changeAplha = Math.Clamp(prevColorAlpha - balance.alphaChangeStep, 0, 1);
            image.color = new Color(color.r, color.g, color.b, changeAplha);
        }
    }
}
