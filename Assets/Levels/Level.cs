using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public Typelevel type;
    public int index;
    public float duration;
    public float delay;
    public bool IsOpen = false;
    public int countObj;
    [Header("Precents Show")]
    public int Apple;
    public int Banana;
    public int Granat;
    public int Bad;
    public int Stone;
    public int Funnel;
    public int Money;
    public int Empty;

    public Level(Typelevel type, float duration, float delay, bool isOpen, int index, int countObj)
    {
        this.type = type;
        this.duration = duration;
        this.delay = delay;
        IsOpen = isOpen;
        this.index = index;
        this.countObj = countObj;
    }
}
