using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(Dropdown))] private int _startScene;
    [SerializeField, ValueDropdown(nameof(Dropdown))] private int _startAnimScene;
    [SerializeField, ValueDropdown(nameof(Dropdown))] private int _gameScene;
    [SerializeField, ValueDropdown(nameof(Dropdown))] private int _endScene;

    private IEnumerable<ValueDropdownItem<int>> Dropdown()
    {
        var allScenes = new Dictionary<int,string>();
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            allScenes.Add(i,sceneName);
        }

        return allScenes.Select(scene => new ValueDropdownItem<int>(scene.Value, scene.Key));
    }


    public static GameSceneManager instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Exist few {nameof(GameSceneManager)}!");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStartScene()
    {
        LoadScene(_startScene);
    }

    public void LoadStartAnimScene()
    {
        LoadScene(_startAnimScene);
    }

    public void LoadGameScene()
    {
        LoadScene(_gameScene);
    }

    public void LoadEndScene()
    {
        LoadScene(_endScene);
    }

    public void StartNextScene()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
    private static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}