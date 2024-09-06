using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public static BonusManager instance;
    public static Action<Sprite, float> bufadd;
    public static Action clear;

    [Header("Apple")]
    [Range(0, 100)]
    public float speedBoat;
    public float timeA;
    [Header("Banana")]
    [Range(0, 100)]
    public float strenght;
    public float timeB;
    [Header("Granat")]
    public float timeG;
    [Header("Bad fruit")]
    public bool offControl, minusSpeed;
    [Range(0, 100)]
    public float speed;
    public float timeBAD;
    [Header("Stone")]
    [Range(0, 100)]
    public float stoneMinus;
    public float timeS;
    [Header("Additioanl Options")]
    public GameObject BufUI;
    public GameObject UIParentBuf;
    [Header("Debug")]
    public List<float> a = new List<float>();
    public List<float> b = new List<float>();
    public List<float> g = new List<float>();
    public List<float> bad = new List<float>();
    public List<float> s = new List<float>();
    [SerializeField] public static Dictionary<string, List<float>> dict = new Dictionary<string, List<float>>();

    private void OnValidate()
    {
        Debug.Log(gameObject.name);
    }
    public void ClearBuf()
    {
        dict = new Dictionary<string, List<float>>
        {
            { "a", new List<float>() },
            { "b", new List<float>() },
            { "g", new List<float>() },
            { "bad", new List<float>() },
            { "s", new List<float>() }
        };
    }
    void Awake()
    {
        clear += ClearBuf;
        instance = this;
        bufadd += BufAdd;
        dict.Add("a", new List<float>());
        dict.Add("b", new List<float>());
        dict.Add("g", new List<float>());
        dict.Add("bad", new List<float>());
        dict.Add("s", new List<float>());
    }

    public void BufAdd(Sprite sprite, float time)
    {
        GameObject a = Instantiate(BufUI, UIParentBuf.transform);
        a.GetComponent<BufUI>().maxTime = time;
        a.GetComponent<BufUI>().Sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var key in dict.Keys)
        {
            int k = 0;
            while (k < dict[key].Count)
            {
                switch (key)
                {
                    case "a":
                        a = dict[key];
                        break;
                    case "b":
                        b = dict[key];
                        break;
                    case "g":
                        g = dict[key];
                        break;
                    case "s":
                        s = dict[key];
                        break;
                    case "bad":
                        bad = dict[key];
                        break;
                }
                dict[key][k] -= Time.deltaTime;
                if (dict[key][k] <= 0)
                {
                    switch (key)
                    {
                        case "a":
                            Boat.apple(-speedBoat);
                            break;
                        case "b":
                            Wave.speedUpdate(-strenght);
                            break;
                        case "g":
                            Boat.granat(false);
                            break;
                        case "s":
                            Boat.apple(stoneMinus);
                            break;
                        case "bad":
                            if (minusSpeed)
                            {
                                Boat.apple(speed);
                            }
                            else
                            {
                                Boat.offaction(false);
                            }
                            break;
                    }
                    dict[key].RemoveAt(k);
                    k--;
                }
                k++;
            }
        }
    }
}
