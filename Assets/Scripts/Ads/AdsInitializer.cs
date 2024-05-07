using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeAds();
        
        BannerAd bannerAd = GetComponent<BannerAd>();
        bannerAd.LoadBanner();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOSGameId
        : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        InterstitialAd interstitialAd = GetComponent<InterstitialAd>();
        interstitialAd.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}