using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public static Boat Instance;
    public Sprite boatDef;
    public Sprite playerSprite;
    public Sprite boatSprite;
    public Animator animator;
    public int ind;
    public GameObject empty;

    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody2D player;
    public float moveSpeed;
    private float moveSpeedFixed => moveSpeed * Time.deltaTime;
    public bool Invulnerable = false;
    public bool off = false;
    public static Action<float> apple;
    public static Action<bool> granat, offaction;
    public static List<float> xSmech, ySmech;
    float h, v;
    private void Awake()
    {
        empty = Instantiate(empty, transform.position, Quaternion.identity);
        animator.SetBool("Drive", false);
        animator.SetInteger("Ind", PlayerPrefs.GetInt("NowPlayer", 0));
        //if (boatDef == boatSprite)
        //{
        //    animator.enabled = true;
        //}
        //else
        //{
        //    animator.enabled = false;
        //}
        GetComponent<SpriteRenderer>().sprite = boatSprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();  
        Instance = this;
        xSmech = new List<float>();
        ySmech = new List<float>();
        xSmech.Add(0f);
        ySmech.Add(0f);
        apple += AppleUpdate;
        granat += Granat;
        offaction += OffControl;
    }
    void FixedUpdate()
    {
        if (Wave.can)
        {
            player.gravityScale = -2;
        }
        if (!off)
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.33)
            {
                h = joystick.Horizontal;
            }
            if (Mathf.Abs(joystick.Vertical) > 0.33)
            {
                v = joystick.Vertical;
            }
            xSmech[0] = Mathf.Clamp(joystick.Horizontal * moveSpeedFixed, -1 * moveSpeedFixed, 1f * moveSpeedFixed);
            ySmech[0] = Mathf.Clamp(joystick.Vertical * moveSpeedFixed, -1 * moveSpeedFixed, 1f * moveSpeedFixed);
            if (xSmech[0] != 0 || ySmech[0] != 0 && !Wave.can)
            {
                Wave.can = true;
            }
            if (xSmech[0] != 0 || ySmech[0] != 0)
            {
                empty.transform.position = new Vector3(transform.position.x + joystick.Horizontal, 
                    transform.position.y + joystick.Vertical, 0f);
                animator.SetBool("Drive", true);
            }
            else
            {
                empty.transform.position = new Vector3(transform.position.x + h, transform.position.y + v, 0f);
                animator.SetBool("Drive", false);
            }
            player.velocity = new Vector2(xSmech.Sum(), ySmech.Sum());
        }
        else
        {
            empty.transform.position = new Vector3(transform.position.x + h, transform.position.y + v, 0f);
            player.velocity = Vector2.zero;
        }
        if (Wave.can)
        {
            Vector2 point = empty.transform.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg - 90);
        }
    }

    public void AppleUpdate(float val)
    {
        moveSpeed += val;
    }
    public void Granat(bool val)
    {
        Invulnerable = val;
    }
    public void OffControl(bool val)
    {
        off = val;
    }
}
