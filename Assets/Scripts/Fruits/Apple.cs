using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public Sprite sprite;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Boat.apple(BonusManager.instance.speedBoat);
            LevelManager.playSound(sound);
            LevelManager.vibro();
            BonusManager.dict["a"].Add(BonusManager.instance.timeA);
            BonusManager.bufadd(sprite, BonusManager.instance.timeA);
            Destroy(this.gameObject);
        }
    }
}
