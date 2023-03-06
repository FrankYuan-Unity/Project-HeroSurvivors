using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemiesControl : MonoBehaviour
{
    private float speed = 0.5f;

    //向target方向移动
    public Transform target;

    private Rigidbody2D rb;

    private Vector3 scaleV;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EventCenter.Instance.AddEventListener("PlayerTranslate", ResetPosition);

    }

    private void ResetPosition(Vector3 v)
    {
        Debug.Log("position:" + v.ToString());
        Vector3 vector = transform.position;
        vector.x += v.x;
        vector.y += v.y;
        transform.position = vector;
	 
    }

   
    // Update is called once per frame
    void Update()
    {
       
        //获取enemy到player的向量
        Vector3 v3 = target.position - transform.position;

        scaleV = transform.localScale;

        //如果敌人在玩家左侧，翻转
        if (v3.x > 0)
        {

            scaleV.x = -1.75f;
            transform.localScale = scaleV;
        }
        else
        {

            scaleV.x = 1.75f;
            transform.localScale = scaleV;

        }


        rb.velocity = Vector3.Normalize(v3) * speed;

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            speed = 0;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            speed = 0.5f;
        }

    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlayerTranslate", ResetPosition);
    }
}

