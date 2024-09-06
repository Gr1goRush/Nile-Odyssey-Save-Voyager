using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    void Start()
    {
        LevelManager.playMusic(menuMusic);
    }
}
