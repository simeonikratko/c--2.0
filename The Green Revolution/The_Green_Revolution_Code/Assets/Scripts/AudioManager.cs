using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }
    #region List of sounds effects
    //Sound Effects
    public AudioClip sfx_landing, sfx_cherry;
    #endregion
    #region List of musics
    public AudioClip music_wow, music_woda;
    //Current music object
    public GameObject currentMusicObject;
    #endregion
    #region Sound Object
    public GameObject soundObject;
    #endregion


    public void PlaySFX(string sfxName)
    {
        #region Playing sound effects
        switch (sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_landing);
                break;
            case "cherry":
                SoundObjectCreation(sfx_cherry);
                break;
            default:
                break;
        }
        #endregion
    }

    void SoundObjectCreation(AudioClip clip)
    {
        #region create sound
        //Create SoundObject gameobject]
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosourse
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
        #endregion
    }

    public void PlayMusic(string musicName)
    {
        #region Playing sound effects
        switch (musicName)
        {
            case "wow":
                MusicObjectCreation(music_wow);
                break;
            case "woda":
                MusicObjectCreation(music_woda);
                break;
            default:
                break;
        }
        #endregion
    }

    void MusicObjectCreation(AudioClip clip)
    {
        #region Create music
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
        {
            Destroy(currentMusicObject);
        }
        //Create SoundObject gameobject]
        currentMusicObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosourse
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Make the source looping
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Play the audio
        currentMusicObject.GetComponent<AudioSource>().Play();
        #endregion
    }
}
