using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net;
using System;
using UnityEngine.Networking;

public class CheckInternet
{
    
    public static IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            action(false);
        }
        else
        {
            Debug.Log("Success");
            action(true);
        }
    }
}
