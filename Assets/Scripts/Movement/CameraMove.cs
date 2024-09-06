using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform transform1;
    public float speed;
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform1.position = Vector2.Lerp(transform1.position, new Vector3(transform1.position.x, player.position.y, -10), speed);
            transform.position += new Vector3(0, 0, -10);
        }
    }
}
