using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Running, Over, Paused}
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameState GameState { get; private set; }

    [SerializeField] int _currentLevel;

    public UnityEvent<int> OnGameBegen = new UnityEvent<int>();
    public UnityEvent<int> OnGameIdle = new UnityEvent<int>();
    public UnityEvent OnGameEnd = new UnityEvent();

    public UnityEvent OnLevelCleared = new UnityEvent();

    public ICurrency currency = new Diamond();
    BannerAd bannerAd;

    public int CurrentLevel { 
        get { 
            return _currentLevel; 
        }
        private set
        {
            _currentLevel = value;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        currency.Load();
        Application.targetFrameRate = 120;
        if(SceneManager.GetActiveScene().buildIndex!=ClientPrefs.GetLevelProgress())
        {
            _currentLevel = ClientPrefs.GetLevelProgress();
            SceneManager.LoadScene(ClientPrefs.GetLevelProgress()-1);
        }
        GameCanvas.Instance.LivesUI.UpdateHearts();

    }
    private void Start()
    {
        Init();

        GameState = GameState.Idle;
        bannerAd = FindObjectOfType<BannerAd>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameIdle()
    {
        GameState = GameState.Idle;
        OnGameIdle?.Invoke(_currentLevel);
        InterstitialAd interstitialAd = FindObjectOfType<InterstitialAd>();
        interstitialAd.ShowAd();
        bannerAd.ShowBannerAd();
    }
    public void StartGame()
    {
        if(GameState == GameState.Paused)
            return;
        GameState = GameState.Running;
        bannerAd.HideBannerAd();
        OnGameBegen?.Invoke(_currentLevel);
    }
    public void EndGame()
    {
        GameState = GameState.Over;
        OnGameEnd?.Invoke();
    }
    public void GoToNextLevel()
    {
        OnLevelCleared?.Invoke();
        
        if (SceneManager.sceneCountInBuildSettings > _currentLevel)
        {
            SceneManager.LoadScene("Level" + ++_currentLevel);
        }
        else
        {
            _currentLevel = 1;
            SceneManager.LoadScene("Level1");
        }
        if (_currentLevel >= ClientPrefs.GetLevelProgress())
        {
            ClientPrefs.SetLevelProgress(_currentLevel);
        }
    }
    public void RestartGame()
    {
        
        if(ClientPrefs.GetLives()-1>0)
        {
            ClientPrefs.SetLives(ClientPrefs.GetLives()-1);
            GameCanvas.Instance.LivesUI.UpdateHearts();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else
        {
            ClientPrefs.SetLives(ClientPrefs.defaultLives);
            GameCanvas.Instance.LivesUI.UpdateHearts();
            _currentLevel = 1;
            ClientPrefs.SetLevelProgress(0);
            SceneManager.LoadScene(0);
        }
    }
    public void PauseGame()
    {
        GameState = GameState.Paused;

    }
    public void ResumeGame()
    {
        GameState = GameState.Idle;
    }
}
