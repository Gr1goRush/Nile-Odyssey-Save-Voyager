using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LotBoat : MonoBehaviour
{
    public BoatScriptableObject boat;
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
            y = PlayerPrefs.GetInt(boat.nameBoat, 2);
        }
        else
        {
            y = PlayerPrefs.GetInt(boat.nameBoat, 0);
        }
        switch (y)
        {
            case 0:
                boat.state = State.SELECTED;
                PlayerPrefs.SetInt(boat.nameBoat, 0);
                PlayerPrefs.SetInt("NowBoat", ind);
                break;
            case 1:
                PlayerPrefs.SetInt(boat.nameBoat, 1);
                boat.state = State.SELECT;
                break;
            case 2:
                PlayerPrefs.SetInt(boat.nameBoat, 2);
                boat.state = State.BUY;
                break;
        }
    }
    public void ButtonClick()
    {
        if (boat.state == State.SELECT)
        {
            boat.state = State.SELECTED;
            PlayerPrefs.SetInt(boat.nameBoat, 0);
            PlayerPrefs.SetInt("NowBoat", ind);
            LevelManager.playSound(sucess);
        }
        else if (boat.state == State.BUY)
        {
            if (PlayerPrefs.GetInt("Money", 0) - boat.cost >= 0)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - boat.cost);
                boat.state = State.SELECTED;
                PlayerPrefs.SetInt(boat.nameBoat, 0);
                PlayerPrefs.SetInt("NowBoat", ind);
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
        if (boat.state == State.SELECTED && PlayerPrefs.GetInt("NowBoat", 0) != ind) 
        {
            boat.state = State.SELECT;
            PlayerPrefs.SetInt(boat.nameBoat, 1);
        }
        switch(boat.state)
        {
            case State.SELECT:
                costTMP.gameObject.SetActive(false);
                buttonImage.sprite = select;
                indicator.sprite = yes;
                break;
            case State.BUY:
                costTMP.gameObject.SetActive(true);
                indicator.sprite = no;
                costTMP.text = boat.cost.ToString();
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
