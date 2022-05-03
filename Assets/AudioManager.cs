using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSource;
    [SerializeField] private AudioSource _temSource;

    [SerializeField, AssetsOnly] private AudioClip _buttonClick;
    [SerializeField, AssetsOnly] private AudioClip _startGameClicked;
    [SerializeField, AssetsOnly] private AudioClip _pankScream;
    [SerializeField, AssetsOnly] private AudioClip _startMenuTem;
    [SerializeField, AssetsOnly] private AudioClip _win;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Exist few instances of {nameof(AudioManager)}", instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        StartMenuTem();
    }

    public void ButtonClickAudio()
    {
        PlayShotAudio(_buttonClick);
    }

    public void StartGameClicked()
    {
        PlayShotAudio(_startGameClicked);
    }

    public void PankScream()
    {
        PlayShotAudio(_pankScream);
    }

    public void Win()
    {
        PlayShotAudio(_win);
    }

    public void StartMenuTem()
    {
        PlayLoopAudio(_startMenuTem);
    }


    public async void StartTem(AudioClip shotStart, AudioClip looped)
    {
        _temSource.clip = null;
        _shotSource.PlayOneShot(shotStart);
        await Task.Delay(TimeSpan.FromSeconds(shotStart.length));
        _temSource.clip = looped;
        _temSource.Play();
    }


    private void PlayShotAudio(AudioClip clip)
    {
        _shotSource.PlayOneShot(clip);
    }

    private void PlayLoopAudio(AudioClip clip)
    {
        _temSource.clip=clip;
        _temSource.Play();
    }

}
