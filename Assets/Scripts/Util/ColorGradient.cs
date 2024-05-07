using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GradientColors
{
    public Color colorTop;
    public Color colorBottom;
}
public class ColorGradient : MonoBehaviour
{
    public static ColorGradient Instance;

    public List<GradientColors> gradientColors = new List<GradientColors>();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public GradientColors GetRandomGradient()
    {
        return gradientColors[UnityEngine.Random.Range(0, gradientColors.Count)];
    }
}
