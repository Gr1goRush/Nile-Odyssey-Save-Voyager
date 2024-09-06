using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 2;
    public AudioClip sound;
    public static Action<float> speedUpdate;
    public LevelManager levelManager;
    public static bool can = true; 
    private void Awake()
    {
        can = false;
        speedUpdate += SpeedUpdate;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelManager.playSound(sound);
            levelManager.Lose();
        }
        Destroy(collision.gameObject);
    }
    void Update()
    {
        if (Time.timeScale == 1 && can)
        {
            speed = Mathf.Clamp(speed, 1, 100000000);
            transform.position += new Vector3(0, 0.01f * speed * Time.deltaTime, 0);
        }
    }
    public void SpeedUpdate(float val)
    {
        speed -= val;
    }
}
