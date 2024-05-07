
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BGCreator : MonoBehaviour
{
    public Color colorTop;
    public Color colorBottom;
    public Texture2D texture;
    public string filename;
    public bool colorIt;

    public void Update()
    {
        if(colorIt)
        {
            colorIt = false;
            texture.SetPixel(0,0,colorTop);
            texture.SetPixel(0,1,colorBottom);
            texture.Apply();
            File.WriteAllBytes("E:\\Blender\\StackBallAssets\\"+filename+".png",texture.EncodeToPNG());
        }
    }
}
