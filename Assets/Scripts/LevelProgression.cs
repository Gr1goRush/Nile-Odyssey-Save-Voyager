using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour
{
    Slider me;
    public LevelManager LevelManager;
    private void Start()
    {
        me = GetComponent<Slider>();
    }
    void Update()
    {
        if (Time.timeScale > 0 && Wave.can)
        {
            me.value += Time.deltaTime;
            if (me.value >= me.maxValue)
            {
                LevelManager.Win();
            }
        }
    }
}
