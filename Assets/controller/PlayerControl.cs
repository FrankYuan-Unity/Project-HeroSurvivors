using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
 

    // Start is called before the first frame update
    void Start()
    {
        //获取组件
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //获取水平轴 -1 0 1
        float horizontal = Input.GetAxisRaw("Horizontal");
        //垂直轴
        float vertical = Input.GetAxisRaw("Vertical");
        //按下左或者右
        if (horizontal != 0)
        {
            ani.SetFloat("Horizontal", horizontal);
            ani.SetFloat("Vertical", 0);
        }
        //按下上或者下
        if (vertical != 0)
        {
            ani.SetFloat("Vertical", vertical);
            ani.SetFloat("Horizontal", 0);
        }
        //切换运动
        Vector2 dir = new Vector2(horizontal, vertical);
        ani.SetFloat("Speed", dir.magnitude);

        //朝该方向移动
        rb.velocity = dir * 5f;

        //resetPosition(true);
        //calculateDistance();
    }

    private void calculateDistance()
    {

        Vector3 v = transform.position;

        if (isNeedTranslateMap(v))
        {
            EventCenter.Instance.EventTrigger("PlayerTranslate", v);
        }

    }
    public float l =0f;
    public float r = 13f;
    public float t = 0f;
    public float b = -8f;

    private bool isNeedTranslateMap(Vector3 v)
    {

        if (v.x < l)
        {
            l -= 13;
            r -= 13;
            //printLog(v, 1);
            return true;
        }

        if (v.x > r)
        {
            l += 13;
            r += 13;
            printLog(v, 2);
            return true;
        }
        if (v.y > t)
        {
            t += 8;
            b += 8;
            //printLog(v, 3);
            return true;
        }
        
        if (v.y < b)
        {
            t -= 8;
            b -= 8;
            //printLog(v, 4);
            return true;
        }
        return false;
    }

    void printLog(Vector3 v, int index) { 
        Debug.Log(index + "translate position:" + v.ToString());
        Debug.Log(index + "leftborder :" + l);
        Debug.Log(index + "rightborder :" + r);
        Debug.Log(index + "topborder :" + t);
        Debug.Log(index + "bottomborder :" + b);

    }
}
