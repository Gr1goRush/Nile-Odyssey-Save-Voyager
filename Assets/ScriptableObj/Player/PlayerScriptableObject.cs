using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 3)]
public class PlayerScriptableObject : ScriptableObject
{
    public Sprite playerSprite;
    public State state;
    public string namePlayer;
    public int cost;
}
