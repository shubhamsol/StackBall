using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public bool isSafe;

    private void Start()
    {
        if(!isSafe)
            GetComponent<MeshRenderer>().material.color = Color.black;
    }
    public void Destory()
    {
        Destroy(gameObject);
    }
}
