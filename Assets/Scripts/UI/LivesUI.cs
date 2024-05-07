using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] List<Image> _hearts = new List<Image>();

    public void UpdateHearts()
    {
        int lives = ClientPrefs.GetLives();
        print("Lives "+lives);
        for(int i = 0; i < _hearts.Count; i++)
            _hearts[i].enabled = false;
        for (int i = 0; i < lives; i++)
            _hearts[i].enabled = true;
    }
}
