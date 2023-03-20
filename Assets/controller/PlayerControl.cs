using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControl : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    public GameObject angryPigPrefab;
    public GameObject[] guns;
    private float createEnemyTime = 2f; //每两秒随机生成一次敌人
    private int gunIndex;


    Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        guns[0].SetActive(true);
        size   = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //获取组件
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        SwitchGun();

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

        CreateEnemy();


    

        //resetPosition(true);
        //calculateDistance();
    }

    public void CreateEnemy()
    {
        createEnemyTime -= Time.deltaTime;
        Vector3 original = transform.position;


        if (createEnemyTime <= 0)
        {
            createEnemyTime = 2f;

            Instantiate(angryPigPrefab, new Vector3(Random.Range(original.x + size.x / 2, original.x - size.x / 2), Random.Range(original.y + size.y / 2, original.y - size.y / 2), 0), Quaternion.identity);

        }

    }

    private void calculateDistance()
    {

        Vector3 v = transform.position;

        if (isNeedTranslateMap(v))
        {
            EventCenter.Instance.EventTrigger("PlayerTranslate", v);
        }

    }
    public float l = 0f;
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



    private void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            SetGunIndexActive(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            SetGunIndexActive(1);
        }

    }


    private void SetGunIndexActive(int index)
    {

        int length = guns.Length;

        if (index >= length || index < 0)
        {
            return;
        }
        for (int i = 0; i < length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[index].SetActive(true);
    }

  


}
