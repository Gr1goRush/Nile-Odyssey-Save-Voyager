using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static Sprite nowBoat, nowPlayer;
    public static Action<AudioClip> playSound, playMusic;
    public static Action loseA;
    public static Action vibro;
    public static Level nowLevel;

    public GameStart GameStart;
    public StartConfeti startConfetti;
    [Header("Bool")]
    public bool musicPlay, volumePlay, vibroPlay;
    [Header("Audio")]
    public AudioClip menuMusic;
    public AudioSource musicSource, volumeSource;
    public AudioClip winSound, loseSound;
    [Header("Sprites")]
    public Sprite yes, no;
    public Sprite lose, win;
    public List<Sprite> boats = new List<Sprite>();
    public List<Sprite> players = new List<Sprite>();
    [Header("UI Elements")]
    public Image Boat, player, LoseWinImage;
    public GameObject PauseObj, WinObj, restart, nextLevel, game, menuBG;
    public GameObject menu, shop, settings, chooseLevel;
    public Image Vibro, Volume, Volume1, Music;
    [Header("Levels")]
    public List<Level> levelList = new List<Level>();
    private void Awake()
    {
        loseA += Lose;
        PlayerPrefs.SetInt("0", 1);
        vibroPlay = Convert.ToBoolean(PlayerPrefs.GetInt("Vibro", 1));
        musicPlay = Convert.ToBoolean(PlayerPrefs.GetInt("Music", 1));
        volumePlay = Convert.ToBoolean(PlayerPrefs.GetInt("Volume", 1));
        for (int i = 0;i < levelList.Count; i++)
        {
            levelList[i].IsOpen = Convert.ToBoolean(PlayerPrefs.GetInt(i.ToString(), 0));
        }
        playSound += PlaySound;
        playMusic += PlayMusic;
        vibro += PlayVibro;
        if (vibroPlay)
        {
            Vibro.sprite = yes;
        }
        else
        {
            Vibro.sprite = no;
        }
        if (musicPlay)
        {
            Music.sprite = yes;
            musicSource.enabled = true;
            musicSource.Play();
        }
        else
        {
            Music.sprite = no;
            musicSource.enabled = false;
        }
        if (volumePlay)
        {
            Volume.sprite = yes;
            Volume1.sprite = yes;
            volumeSource.enabled = true;
            volumeSource.clip = null;
        }
        else
        {
            Volume.sprite = no;
            Volume1.sprite = no;
            volumeSource.enabled = false;
        }
    }
    public void Continue()
    {
        Time.timeScale = 1.0f;
        PauseObj.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        PauseObj.SetActive(true);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        menuBG.SetActive(true);
        game.SetActive(false);
        settings.SetActive(false);
        menu.SetActive(false);
        menu.SetActive(true);
        shop.SetActive(false);
        chooseLevel.SetActive(false);
        Wave.can = false;
    }
    public void Restart()
    {
        GameStart.Restart();
        Wave.can = false;
    }
    public void NextLevel()
    {
        if (nowLevel.index < levelList.Count - 1)
        {
            nowLevel = levelList[nowLevel.index + 1];
            GameStart.SetParametrs();
        }
        Wave.can = false;
    }
    public void Win()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Obstacles"))
        {
            Destroy(g);
        }
        musicSource.Stop();
        playSound(winSound);
        Time.timeScale = 0;
        LoseWinImage.sprite = win;
        WinObj.SetActive(true);
        restart.SetActive(false);
        if (nowLevel.index == levelList.Count - 1)
        {
            nextLevel.SetActive(false);
        }
        else
        {
            nextLevel.SetActive(true);
            levelList[nowLevel.index + 1].IsOpen = true;
            PlayerPrefs.SetInt((nowLevel.index + 1).ToString(), 1);
        }
        Wave.can = false;
        startConfetti.St();
    }
    public void Lose()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Obstacles")) 
        { 
            Destroy(g); 
        }
        musicSource.Stop();
        playSound(loseSound);
        Time.timeScale = 0;
        LoseWinImage.sprite = lose;
        WinObj.SetActive(true);
        restart.SetActive(true);
        nextLevel.SetActive(false);
        Wave.can = false;
    }
    public void PlayMenu()
    {
        menu.SetActive(false);
        game.SetActive(false);
        shop.SetActive(false);
        settings.SetActive(false);
        chooseLevel.SetActive(true);
    }
    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
        shop.SetActive(false);
    }
    public void Shop()
    {
        menu.SetActive(false);
        chooseLevel.SetActive(false);
        game.SetActive(false);
        settings.SetActive(false);
        shop.SetActive(true);
    }
    public void VibroOnOff()
    {
        vibroPlay = !vibroPlay;
        PlayerPrefs.SetInt("Vibro", vibroPlay ? 1 : 0);
        if (vibroPlay)
        {
            Vibro.sprite = yes;
        }
        else
        {
            Vibro.sprite = no;
        }
    }
    public void MusicOnOff()
    {
        musicPlay = !musicPlay;
        PlayerPrefs.SetInt("Music", musicPlay ? 1 : 0);
        if (musicPlay)
        {
            Music.sprite = yes;
            musicSource.enabled = true;
            musicSource.Play();
        }
        else
        {
            Music.sprite = no;
            musicSource.enabled = false;
        }
    }
    public void VolumeOnOff()
    {
        volumePlay = !volumePlay;
        PlayerPrefs.SetInt("Volume", volumePlay ? 1 : 0);
        if (volumePlay)
        {
            Volume.sprite = yes;
            Volume1.sprite = yes;
            volumeSource.enabled = true;
            volumeSource.clip = null;
        }
        else
        {
            Volume1.sprite = no;
            Volume.sprite = no;
            volumeSource.enabled = false;
        }
    }
    public void PlaySound(AudioClip audio)
    {
        if (volumePlay)
        {
            volumeSource.clip = audio;
            volumeSource.PlayOneShot(audio);
        }
    }
    public void PlayMusic(AudioClip audio)
    {
        if (musicPlay)
        {
            musicSource.clip = audio;
            musicSource.Play();
        }
    }
    public void PlayVibro()
    {
        if (vibroPlay)
        {
            Handheld.Vibrate();
        }
    }
    private void Update()
    {
        nowBoat = boats[PlayerPrefs.GetInt("NowBoat", 0)];
        nowPlayer = players[PlayerPrefs.GetInt("NowPlayer", 0)];
        Boat.sprite = nowBoat;
        player.sprite = nowPlayer;

    }
}
