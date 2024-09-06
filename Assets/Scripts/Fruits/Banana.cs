using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public Sprite sprite;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Wave.speedUpdate(BonusManager.instance.strenght);
            LevelManager.playSound(sound);
            LevelManager.vibro();
            BonusManager.dict["b"].Add(BonusManager.instance.timeB);
            BonusManager.bufadd(sprite, BonusManager.instance.timeB);
            Destroy(this.gameObject);
        }
    }
}
