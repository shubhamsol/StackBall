using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowHandler : MonoBehaviour
{
    [SerializeField] Ball _ball;
    public void OnTriggerEnter(Collider collision)
    {
        if(!collision.CompareTag("Finish"))
        {
            if (collision.GetComponent<Part>().isSafe)
                _ball.HandleDestory(collision);
            else
            {
                GameManager.Instance.RestartGame();
                
                //Destroy(gameObject);
            }
        }
        else
        {
            GameManager.Instance.GoToNextLevel();

            return;
        }
    }
}
