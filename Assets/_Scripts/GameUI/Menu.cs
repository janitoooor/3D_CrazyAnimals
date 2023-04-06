using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _componentsToDisable;
    [Space]
    [SerializeField] private BackgroundMusic _backgroundMusic;
    [Space]
    [SerializeField] private float _timeScaleOnMenuOpen = 0.01f;
    [Space]
    [SerializeField] private Button _buttonOpenMenu;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWinPanel;
    [SerializeField] private GameObject _loadingPanel;
    [Header("Panel Menu Button")]
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonGoToMenu;
    [SerializeField] private Button[] _buttonsRestart;
    [SerializeField] private Slider _soundVolumeSlider;
    [SerializeField] private Toggle _musicToggle;

    private PlayerHealth _playerHealth;

    private float _startFixedDeltaTime;

    private AdressablesLoader _adressablesLoader;

    [Inject]
    private void Constructor(AdressablesLoader adressablesLoader, PlayerHealth playerHealth)
    {
        _adressablesLoader = adressablesLoader;
        _playerHealth = playerHealth;
    }

    #region Mono
    private void Awake()
    {
        _buttonOpenMenu.onClick.AddListener(() =>
        {
            OpenMenuWindow();
        });

        _buttonGoToMenu.onClick.AddListener(() =>
        {
            Loader.LoadTargetScene(Scene.MenuScene);
        });

        _buttonContinue.onClick.AddListener(() =>
        {
            HidePanel(_menuPanel);
        });

        foreach (var button in _buttonsRestart)
        {
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }

        _soundVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            SetVolume(value);
        });

        _musicToggle.onValueChanged.AddListener((bool isSelected) =>
        {
            SetMusicEnabled(isSelected);
        });
    }

    private void Start()
    {
        _playerHealth.OnPlayerDie += PlayerHealth_OnPlayerDie;
        _playerHealth.OnPlayerTakeFinish += PlayerHealth_OnPlayerTakeFinish;
        _adressablesLoader.OnStartLoadLvl += AdressablesLoader_OnStartLoadLvl;
        _adressablesLoader.OnEndLoadLvl += AdressablesLoader_OnEndLoadLvl;

        _startFixedDeltaTime = Time.fixedDeltaTime;
        SetVolume(1f);
        SetMusicEnabled(true);

        HidePanel(_menuPanel);
        HidePanel(_gameOverPanel);
        HidePanel(_gameWinPanel);
        HidePanel(_loadingPanel);
    }

    private void AdressablesLoader_OnEndLoadLvl(object sender, System.EventArgs e)
    {
        HidePanel(_loadingPanel);
    }

    private void AdressablesLoader_OnStartLoadLvl(object sender, System.EventArgs e)
    {
        ShowPanel(_loadingPanel);
    }

    #endregion

    public void OpenMenuWindow()
    {
        ShowPanel(_menuPanel);
    }

    private void PlayerHealth_OnPlayerDie(object sender, System.EventArgs e)
    {
        ShowPanel(_gameOverPanel);
    }

    private void PlayerHealth_OnPlayerTakeFinish(object sender, System.EventArgs e)
    {
        ShowPanel(_gameWinPanel);
    }

    private void HidePanel(GameObject panelObj)
    {
        panelObj.SetActive(false);

        foreach (var component in _componentsToDisable)
            component.enabled = true;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }

    private void ShowPanel(GameObject panelObj)
    {
        panelObj.SetActive(true);

        foreach (var component in _componentsToDisable)
            component.enabled = false;

        Time.timeScale = _timeScaleOnMenuOpen;
        Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void SetMusicEnabled(bool isEnabled)
    {
        _backgroundMusic.EnabledAudioSource(isEnabled);
    }
}
