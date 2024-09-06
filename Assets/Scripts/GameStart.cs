using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Typelevel
{
    LEVEL,
    INFINITE
}
public class GameStart : MonoBehaviour
{
    public Typelevel type;
    public GameObject BoatGameObject, Wave, Pause, LoseWin, River;
    public Wave wave;
    public BonusManager bonusManager;
    public Spawner spawner;
    public float delayTime;
    public float speedStartWave;

    public float durationLevel;
    public GameObject SliderLevel;

    public AudioClip music;
    Vector3 startPosBoat = new Vector3(-0.0200000033f, -2.99000001f, 0);
    Vector3 waveStart = new Vector3(0.236770213f, -9.13000011f, 0);
    public void SetParametrs()
    {
        Level l = LevelManager.nowLevel;
        delayTime = l.delay;
        type = l.type;
        durationLevel = l.duration;
        Restart();
    }
    public void Restart()
    {
        LevelManager.playMusic(music);
        Time.timeScale = 1;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        if (GameObject.FindGameObjectsWithTag("River").Length > 0)
        {
            foreach (GameObject t in GameObject.FindGameObjectsWithTag("River"))
            {
                Destroy(t);
            }
        }
        BoatGameObject.GetComponent<Boat>().boatSprite = LevelManager.nowBoat;
        BoatGameObject.GetComponent<Boat>().playerSprite = LevelManager.nowPlayer;
        Wave.transform.position = waveStart;
        Instantiate(River, new Vector3(0, 1.60927916f, 0), Quaternion.identity);
        Instantiate(BoatGameObject, startPosBoat, Quaternion.identity);
        Pause.SetActive(false);
        LoseWin.SetActive(false);
        wave.speed = speedStartWave;
        bonusManager.ClearBuf();
        spawner.delay = delayTime;
        if (type == Typelevel.LEVEL)
        {
            SliderLevel.SetActive(true);
            SliderLevel.GetComponent<Slider>().maxValue = durationLevel;
            SliderLevel.GetComponent<Slider>().value = 0;
        }
    }

    
}
