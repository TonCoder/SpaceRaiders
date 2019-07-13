using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSoundFx", menuName = "Sound/SoundFx")]
public class SO_SoundFX : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] internal float soundcVolume;
    [SerializeField, MinMaxSlider] internal Vector2 pitch;
    [SerializeField] internal List<AudioClip> _soundClip;

    public void PlayRandomSoundFx(AudioSource asource){
        if(_soundClip.Count <= 0) return;

        asource.clip = _soundClip[Random.Range(1, _soundClip.Count - 1)];
        asource.volume = soundcVolume;
        asource.pitch = Random.Range(pitch.x, pitch.y);
        asource.Play();
    }
}
