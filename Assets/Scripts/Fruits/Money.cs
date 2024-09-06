using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 1);
            LevelManager.playSound(sound);
            LevelManager.vibro();
            Destroy(gameObject);
        }
    }
}
