using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFruit : MonoBehaviour
{
    public Sprite sprite;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !Boat.Instance.Invulnerable)
        {
            LevelManager.playSound(sound);
            if (BonusManager.instance.minusSpeed)
            {
                Boat.apple(-BonusManager.instance.speed);
            }
            else
            {
                Boat.offaction(true);
            }
            BonusManager.dict["bad"].Add(BonusManager.instance.timeBAD);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            BonusManager.bufadd(sprite, BonusManager.instance.timeBAD);
            Destroy(this.gameObject);
        }
    }
}
