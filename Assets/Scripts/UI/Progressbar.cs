using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] TextMeshProUGUI _currentLevelText;
    [SerializeField] TextMeshProUGUI _nextLevelText;

    Transform _player;
    Vector3 _startPosition;
    Vector3 _currentPosition;
    Vector3 _endPosition;

    float _totalDistance;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _currentLevelText.text = GameManager.Instance.CurrentLevel.ToString();
        _nextLevelText.text = (GameManager.Instance.CurrentLevel+1).ToString();
        _player = FindObjectOfType<Player>().transform;
        _startPosition = _player.position;
        _endPosition = FindObjectOfType<Level>().transform.position;
        _totalDistance = Vector3.Distance(_endPosition, _startPosition);
    }
    private void Update()
    {
        if(_player == null)
            return;
        float progress =  Vector3.Distance(_player.transform.position, _endPosition)/ _totalDistance;
        FillImage(progress);
    }
    public void FillImage(float per)
    {
        _fillImage.fillAmount = per;
    }
}
