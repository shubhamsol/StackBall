using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public enum PlayerState { Idle, Falling }

public class Player : MonoBehaviour
{
    float _speed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _groundOffset;
    [SerializeField] float _unit;
    [SerializeField] Animator _animator;
    [SerializeField] Vector3 _offset;
    [SerializeField] Ball _ball;
    PlayerState _state;

    Vector3 _position;

    
    public PlayerState State 
    { 
        get 
        { 
            return _state; 
        } 
        private set
        {
            _state = value;
        }
    }
    private void Start()
    {
        _position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.Paused) return;

        if (GameManager.Instance.GameState == GameState.Over)
        {
            _animator.SetBool("Fall", false);
            return;

        }
        if (Input.GetMouseButton(0))
        {
            if(GameManager.Instance.GameState != GameState.Running)
                GameManager.Instance.StartGame();

            if (_speed <= 0)
            {
                State = PlayerState.Falling;
                _speed = _maxSpeed;
                //transform.position = _position;
                transform.DOLocalMoveY(_position.y, 0.1f);
                _position.y -= _unit;
                _animator.SetBool("Fall", true);
                _ball.Rb.DOMoveY(0, 0.1f);
                //transform.GetChild(0).position = Vector3.zero;
                //transform.GetChild(0).DOLocalMoveY(0f, 0.1f);

            }
            _speed -= Time.deltaTime;
        }
        else
        {
            _animator.SetBool("Fall", false);

            State = PlayerState.Idle;
            if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit hitInfo))
            {
                transform.position = hitInfo.point + _offset;
            }
        }
    }
    
}
