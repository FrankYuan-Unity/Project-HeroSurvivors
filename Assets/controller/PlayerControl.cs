using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : BaseObject
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
        rb.velocity = dir * 2f;

        resetPosition(true);
    }
}
