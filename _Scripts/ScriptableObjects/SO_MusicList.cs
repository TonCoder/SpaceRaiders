using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Music List", menuName = "Music/MusicList")]
public class SO_MusicList : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] internal float musicVolume;
    [SerializeField] internal List<MusicInfo> musicList;
}

[System.Serializable]
public struct MusicInfo
{
    [SerializeField] internal string _sceneName;
    [SerializeField] internal AudioClip _audioClip;
}