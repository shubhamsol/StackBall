using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _splash;

    [SerializeField] AudioClip _bounce;
    [SerializeField] AudioClip _destory;

    [SerializeField] AudioSource _source;
    [SerializeField] AudioSource _levelSource;

    public Rigidbody Rb;
    private void OnTriggerEnter(Collider other)
    {
        if (_player.State == PlayerState.Falling)
        {
            if(other.CompareTag("Finish"))
            {
                GameManager.Instance.GoToNextLevel();
                
                return;
            }
            if(other.GetComponent<Part>().isSafe)
                HandleDestory(other);
            else
            {
                GameManager.Instance.RestartGame();
                other.transform.parent.DOScale(other.transform.parent.localScale * 1.2f, .3f).OnComplete(() =>
                {
                    other.transform.parent.DOScale(new Vector3(1,1,1), .3f);
                });
                Destroy(gameObject);
            }
        }
        else
        {
            if(_player.State == PlayerState.Idle)
            {
                Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 0.2f);
                {
                    GameObject splash = Instantiate(_splash, hit.point, Quaternion.identity);
                    Destroy(splash,5f);
                    splash.transform.parent = other.transform;
                }
            }
        }
    }

    public void HandleDestory(Collider other)
    {
        _levelSource.PlayOneShot(_destory);
        
        other.transform.parent.parent = null;
        other.transform.parent.eulerAngles += new Vector3(0, Random.Range(-90, 90), 0);
        foreach(var partInObs in other.GetComponentInParent<Obstacle>().parts)
            if(partInObs.TryGetComponent<Animator>(out Animator partAnimator))
            {
                partInObs.GetComponent<MeshCollider>().enabled = false;
                partAnimator.SetTrigger("Fly");
            }
    }

    public void Bounce()
    {
        _source.PlayOneShot(_bounce);
    }
}
