using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BufUI : MonoBehaviour
{
    public float maxTime;
    public Sprite Sprite;
    Slider slider;
    public Image im;
    void Start()
    {
        slider = this.GetComponent<Slider>();
        slider.maxValue = maxTime;
        slider.value = 0;
        im.sprite = Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            slider.value += Time.deltaTime;
            if (slider.value >= maxTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
