using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Money", 0).ToString();
    }
}
