using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinite : MonoBehaviour
{
    public Level infinite;
    public GameObject bg;

    [SerializeField] private StartTime _guideObject;

    private void OnValidate()
    {
        _guideObject ??= FindObjectOfType<StartTime>();
    }
    public void Click()
    {
        _guideObject.Activate();
        bg.SetActive(false);
        LevelManager.nowLevel = infinite;
    }
}
