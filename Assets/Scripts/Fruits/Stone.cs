using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Sprite sprite;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !Boat.Instance.Invulnerable)
        {
            Boat.apple(-BonusManager.instance.stoneMinus);
            LevelManager.playSound(sound);
            LevelManager.vibro();
            BonusManager.dict["s"].Add(BonusManager.instance.timeS);
            BonusManager.bufadd(sprite, BonusManager.instance.timeS);
            Destroy(this.gameObject);
        }
    }
}
