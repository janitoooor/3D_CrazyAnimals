using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static Scene _currentScene = Scene.MenuScene;

    public static void LoadSceneCallback()
    {
        SceneManager.LoadSceneAsync(_currentScene.ToString());
    }

    public static void LoadTargetScene(Scene scene)
    {
        _currentScene = scene;
        SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString());
    }

}
