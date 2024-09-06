using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    public Transform player;
    public AudioClip sound;
    public float redius = 2.3f;
    public float strenght = 0.6f;
    bool start = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {        
        if (player == null || !GameObject.FindGameObjectWithTag("Player"))
        {
            StopAllCoroutines();
            Destroy(gameObject);
            return;
        }

        float y = Vector2.Distance(transform.position, player.position);
        if (y <= redius && !Boat.Instance.Invulnerable)
        {
            if (Boat.xSmech.Count < 2)
            {
                Boat.xSmech.Add(0);
            }
            if (Boat.ySmech.Count < 2)
            {
                Boat.ySmech.Add(0);
            }
            Boat.xSmech[1] = (transform.position - player.position).x * strenght;
            Boat.ySmech[1] = (transform.position - player.position).y * strenght;
        }
        else
        {
            if (Boat.xSmech.Count > 1)
            {
                Boat.xSmech.RemoveAt(Boat.xSmech.Count - 1);
            }
            if (Boat.ySmech.Count > 1)
            {
                Boat.ySmech.RemoveAt(Boat.ySmech.Count - 1);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !Boat.Instance.Invulnerable)
        {
            if (start)
            {
                StartCoroutine(StartDeath());
            }
        }
    }

    public IEnumerator StartDeath()
    {
        start = false;
        Boat.Instance.moveSpeed = 0;
        strenght = 4;
        redius = 4;
        while (player.transform.localScale.x > 0 && player.transform.localScale.y > 0)
        {
            player.localScale -= new Vector3(0.3f, 0.3f);
            yield return new WaitForSeconds(0.01f);
        }
        LevelManager.playSound(sound);
        LevelManager.vibro();
        LevelManager.loseA();
        Destroy(player.gameObject);
        Destroy(gameObject);
        yield return null;
    }
}
