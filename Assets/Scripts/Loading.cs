using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class Loading : MonoBehaviour
{
    public GameObject g;
    public Slider s;
    public TextMeshProUGUI t;

    public void St()
    {
        s.value = 0; // ���������� �������� � 100, ���� ��� ��������� ���� �����
        g.SetActive(true);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        t.text = ((int)s.value).ToString() + "%";
    }
}
