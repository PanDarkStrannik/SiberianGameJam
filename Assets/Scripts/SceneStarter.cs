using System;
using System.Threading.Tasks;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] private float _startSceneAfterDelay;


    public async void StartNextScene()
    {
        await Task.Delay(TimeSpan.FromSeconds(_startSceneAfterDelay));
        GameSceneManager.instance.StartNextScene();
    }

}
