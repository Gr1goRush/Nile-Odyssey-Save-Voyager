using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LotPlayer : MonoBehaviour
{
    public PlayerScriptableObject player;
    public Sprite select, selected, buy, yes, no;
    public AudioClip alarm, sucess;
    public int ind;
    public Image buttonImage, indicator;
    public TextMeshProUGUI costTMP;
    private void Awake()
    {
        int y;
        if (ind != 0)
        {
            y = PlayerPrefs.GetInt(player.namePlayer, 2);
        }
        else
        {
            y = PlayerPrefs.GetInt(player.namePlayer, 0);
        }
        switch (y)
        {
            case 0:
                player.state = State.SELECTED;
                PlayerPrefs.SetInt(player.namePlayer, 0);
                PlayerPrefs.SetInt("NowPlayer", ind);
                break;
            case 1:
                PlayerPrefs.SetInt(player.namePlayer, 1);
                player.state = State.SELECT;
                break;
            case 2:
                PlayerPrefs.SetInt(player.namePlayer, 2);
                player.state = State.BUY;
                break;
        }
    }
    public void ButtonClick()
    {
        if (player.state == State.SELECT)
        {
            player.state = State.SELECTED;
            PlayerPrefs.SetInt(player.namePlayer, 0);
            PlayerPrefs.SetInt("NowPlayer", ind);
            LevelManager.playSound(sucess);
        }
        else if (player.state == State.BUY)
        {
            if (PlayerPrefs.GetInt("Money", 0) - player.cost >= 0)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - player.cost);
                player.state = State.SELECTED;
                PlayerPrefs.SetInt(player.namePlayer, 0);
                PlayerPrefs.SetInt("NowPlayer", ind);
                LevelManager.playSound(sucess);
            }
            else
            {
                LevelManager.playSound(alarm);
            }
        }
    }
    private void Update()
    {
        if (player.state == State.SELECTED && PlayerPrefs.GetInt("NowPlayer", 0) != ind)
        {
            player.state = State.SELECT;
            PlayerPrefs.SetInt(player.namePlayer, 1);
        }
        switch (player.state)
        {
            case State.SELECT:
                costTMP.gameObject.SetActive(false);
                buttonImage.sprite = select;
                indicator.sprite = yes;
                break;
            case State.BUY:
                costTMP.gameObject.SetActive(true);
                indicator.sprite = no;
                costTMP.text = player.cost.ToString();
                buttonImage.sprite = buy;
                break;
            case State.SELECTED:
                indicator.sprite = yes;
                costTMP.gameObject.SetActive(false);
                buttonImage.sprite = selected;
                break;
        }
    }
}
