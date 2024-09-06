using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private Transform indicator;
    [SerializeField] private GameObject GameObject;
    public GameObject prefabRiver;
    void Update()
    {
        float y = Camera.main.WorldToViewportPoint(indicator.position).y;
        if (y < 1.5f)
        {
            Instantiate(prefabRiver, GameObject.transform.position, Quaternion.identity);
            this.GetComponent<River>().enabled = false;
        }
    }
}
