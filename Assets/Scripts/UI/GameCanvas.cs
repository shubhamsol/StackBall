using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] TextMeshProUGUI _currencyText;
    [SerializeField] GameObject _tapToPlayText;
    [SerializeField] public LivesUI LivesUI;
    [SerializeField] GameObject _notConnectedPanel;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GameManager.Instance.OnGameIdle.AddListener(UpdateLevelText);
        ICurrency currency = GameManager.Instance.currency;
        _currencyText.text = currency.Amount.ToString();
        InvokeRepeating(nameof(OnClickRetry),1f,10f);
    }
    private void OnEnable()
    {
        //GameManager.Instance.OnGameBegen.AddListener(ToggleTapToPlayText);
    }

    public void UpdateLevelText(int level)
    {
        _levelText.text = level.ToString();
        //OnClickRetry();
    }
    public void ToggleTapToPlayText(bool value)
    {
        _tapToPlayText.SetActive(value);
    }
    private void OnDestroy()
    {
        //GameManager.Instance.OnGameBegen.RemoveListener(ToggleTapToPlayText);
    }
    public void UpdateCurrency()
    {
        if (ClientPrefs.GetMaxLevelReached() > GameManager.Instance.CurrentLevel)
            return;
        ICurrency currency = GameManager.Instance.currency;
        currency.Deposite(20);
        _currencyText.text = currency.Amount.ToString();
        currency.Save();

    }
    public void OnClickRetry()
    {
        StartCoroutine(CheckInternet.CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                _notConnectedPanel.SetActive(false);
                InterstitialAd interstitialAd = FindObjectOfType<InterstitialAd>();
                interstitialAd.LoadAd();
                if (GameManager.Instance.GameState == GameState.Paused)
                    GameManager.Instance.ResumeGame();

            }
            else
            {
                _notConnectedPanel.SetActive(true);
                if(GameManager.Instance.GameState != GameState.Paused)
                    GameManager.Instance.PauseGame();
            }
        }));
    }
}
