using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
    public int ind;
    Level level;
    public LevelManager manager;
    public GameStart gameStart;

    [Header("UI")]
    public Sprite locking, open;
    public Button play;

    [SerializeField] private StartTime _guideObject;

    private void OnValidate()
    {
        _guideObject ??= FindObjectOfType<StartTime>();
    }
    void Update()
    {
        level = manager.levelList[ind];
        if (level.IsOpen)
        {
            play.interactable = true;
            play.gameObject.GetComponent<Image>().sprite = open;
        }
        else
        {
            play.interactable = false;
            play.gameObject.GetComponent<Image>().sprite = locking;
        }
    }
    public void UpdateLevel()
    {
        _guideObject.Activate();
        LevelManager.nowLevel = level;
    }
}
