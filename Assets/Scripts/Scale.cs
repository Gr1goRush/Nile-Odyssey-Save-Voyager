using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    public float scale = 486f;
    public bool x, y;
    private void Awake()
    {
        if (!canvas)
        {
            canvas = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<RectTransform>();
        }
        float x1 = transform.localScale.x;
        float y1 = transform.localScale.y;
        if (x)
        {
            x1 = (float)canvas.sizeDelta.x / scale;
        }
        if (y)
        {
            y1 = (float)canvas.sizeDelta.x / scale;
        }
        transform.localScale = new Vector3(x1, y1, transform.localScale.z);
    }
}
