using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartConfeti : MonoBehaviour
{
    public Animator g;
    public void St()
    {
        g.SetTrigger("con");
    }
}
