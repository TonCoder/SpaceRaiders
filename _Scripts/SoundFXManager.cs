using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{

    [Space(5), Header("Sound Fx")]
    public AudioSource asource;
    public SO_SoundFX goodHitSoundFX;
    public SO_SoundFX badHitSoundFX;
    public SO_SoundFX perfectHitSoundFX;

    public void PlayGoodHit()
    {
        goodHitSoundFX.PlayRandomSoundFx(asource);
    }
    public void PlayBadHit()
    {
        badHitSoundFX.PlayRandomSoundFx(asource);
    }
    public void PlayPerfectHit()
    {
        perfectHitSoundFX.PlayRandomSoundFx(asource);
    }
}
