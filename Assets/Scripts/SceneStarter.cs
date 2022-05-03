using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] private float _startSceneAfterDelay;
    [SerializeField, ValueDropdown(nameof(Dropdown))] private int _sceneToStart;
    [SerializeField] private bool _startOnAwake = false;

    private void Awake()
    {
        if (_startOnAwake)
            StartNextScene();
    }

    private IEnumerable<ValueDropdownItem<int>> Dropdown()
    {
        var allScenes = new Dictionary<int, string>();
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            allScenes.Add(i, sceneName);
        }

        return allScenes.Select(scene => new ValueDropdownItem<int>(scene.Value, scene.Key));
    }

    public async void StartNextScene()
    {
        await Task.Delay(TimeSpan.FromSeconds(_startSceneAfterDelay));
        GameSceneManager.LoadScene(_sceneToStart);
    }

}
