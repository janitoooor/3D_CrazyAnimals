using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(() =>
        {
            Loader.LoadTargetScene(Scene.PlatformerScene);
        });

        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
