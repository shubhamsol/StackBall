using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Part> parts = new List<Part>();
    private void Start()
    {
        parts = GetComponentsInChildren<Part>().ToList();
    }
    private void OnDestroy()
    {
        
    }
}
