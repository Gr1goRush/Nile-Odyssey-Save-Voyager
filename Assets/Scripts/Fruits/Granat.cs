using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granat : MonoBehaviour
{
    public Sprite sprite;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Boat.granat(true);
            LevelManager.playSound(sound);
            LevelManager.vibro();
            BonusManager.dict["g"].Add(BonusManager.instance.timeG);
            BonusManager.bufadd(sprite, BonusManager.instance.timeG);
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
