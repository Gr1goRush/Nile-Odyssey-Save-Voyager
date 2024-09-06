using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float delay;
    float time;
    public GameObject[] prefabs;
    public GameObject[] spawnPoints;
    void Update()
    {
        if (Time.timeScale == 1 && Wave.can && GameObject.FindGameObjectWithTag("Player"))
        {
            time += Time.deltaTime;
            if (time > delay)
            {
                time = 0;
                List<GameObject> list = spawnPoints.ToList();
                List<GameObject> obj = new List<GameObject>();
                print(LevelManager.nowLevel.Apple);
                for (int k  = 0; k < LevelManager.nowLevel.Apple; k++)
                {
                    obj.Add(prefabs[0]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Banana; k++)
                {
                    obj.Add(prefabs[2]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Stone; k++)
                {
                    obj.Add(prefabs[Random.Range(5, 9)]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Money; k++)
                {
                    obj.Add(prefabs[9]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Granat; k++)
                {
                    obj.Add(prefabs[4]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Bad; k++)
                {
                    obj.Add(prefabs[1]);
                }
                for (int k = 0; k < LevelManager.nowLevel.Funnel; k++)
                {
                    obj.Add(prefabs[3]);
                }
                for (int i = 0; i < LevelManager.nowLevel.countObj; i++)
                {
                    int j = Random.Range(0, list.Count - 1);
                    GameObject p = obj[Random.Range(0, obj.Count - 1)];
                    if (p)
                    {
                        GameObject a = Instantiate(obj[Random.Range(0, obj.Count - 1)], list[j].transform.position, Quaternion.identity);
                        list.RemoveAt(j);
                        a.GetComponent<Rigidbody2D>().gravityScale = 0.06f;
                    }
                }
                obj.Clear();
            }
        }
    }
}
