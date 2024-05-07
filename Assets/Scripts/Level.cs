using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    [SerializeField] float _speed;

    [SerializeField] Texture2D _bgTexture;

    private void Awake()
    {
    }
    void Start()
    {
        InitilizeLevel();

        GameManager.Instance.GameIdle();
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Over)
            transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    }
    public void InitilizeLevel()
    {
        SetBackground();
    }
    public void SetBackground()
    {
        GradientColors bgGradient = ColorGradient.Instance.GetRandomGradient();
        _bgTexture.SetPixel(0, 0, bgGradient.colorTop);
        _bgTexture.SetPixel(0, 1, bgGradient.colorBottom);
        _bgTexture.Apply();
    }
}
