using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    SELECTED = 0,
    SELECT = 1,
    BUY = 2
}


[CreateAssetMenu(fileName = "Boat", menuName = "ScriptableObjects/Boat", order = 2)]
public class BoatScriptableObject : ScriptableObject
{
    public Sprite boatSprite;
    public State state;
    public string nameBoat;
    public int cost;
}
