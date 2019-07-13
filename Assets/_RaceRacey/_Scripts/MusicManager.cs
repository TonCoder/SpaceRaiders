using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public SO_MusicList _audioLibrary;
    private AudioSource ASource;
    private AudioClip currentMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ASource = GetComponent<AudioSource>();
    }

    private void Start()
    {
    //     float musicLvl = GamePrefs.GetMusicLevel();
    //     ASource.volume = musicLvl <= 0 ? 1 : musicLvl;
    }

    public void LevelFinishedLoading(Scene scene)
    {
        // Debug.Log("Now playing: " + scene.name +", Scene Index: " + scene.buildIndex);
        try
        {
            for (int i = 0; i < _audioLibrary.musicList.Count; i++)
            {
                if(_audioLibrary.musicList[i]._sceneName == scene.name && _audioLibrary.musicList[i]._audioClip != currentMusic) {         
                    currentMusic = _audioLibrary.musicList[i]._audioClip;
                    ASource.clip = currentMusic;
                    ASource.loop = true;
                    ASource.Play();
                    ASource.volume = _audioLibrary.musicVolume;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error loading Music");
        }
    }

    public void PlayGameOverMusic(string scene = "GameOver"){
        for (int i = 0; i < _audioLibrary.musicList.Count; i++)
        {
            if (_audioLibrary.musicList[i]._sceneName == scene && _audioLibrary.musicList[i]._audioClip != currentMusic)
            {
                currentMusic = _audioLibrary.musicList[i]._audioClip;
                ASource.clip = currentMusic;
                ASource.loop = true;
                ASource.Play();
                ASource.volume = _audioLibrary.musicVolume;
            }
        }
    }

    public void SetLevelSceneName(string scene)
    {
        // Debug.Log("Now playing: " + scene.name +", Scene Index: " + scene.buildIndex);
        try
        {
            for (int i = 0; i < _audioLibrary.musicList.Count; i++)
            {
                if (_audioLibrary.musicList[i]._sceneName == scene && _audioLibrary.musicList[i]._audioClip != currentMusic)
                {
                    currentMusic = _audioLibrary.musicList[i]._audioClip;
                    ASource.clip = currentMusic;
                    ASource.loop = true;
                    ASource.Play();
                    ASource.volume = _audioLibrary.musicVolume;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error loading Music");
        }
    }

    public void SetMainMusicLevel(float volume)
    {
        _audioLibrary.musicVolume = volume;
        ASource.volume = volume;
    }

}
