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
    private float createEnemyTime = 0.2f; //每两秒随机生成一次敌人
    private int gunIndex;
    public FixedJoystick joystick; //摇杆

    public int blood = 1000; //血量

    Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        guns[1].SetActive(true);
        size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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
        float vertical = Input.GetAxisRaw("Vertical");
        if (Util.isMobile())
        {
            Debug.Log("判断是否是移动端");

            horizontal = getAnimParam(joystick.Horizontal);

            vertical = getAnimParam(joystick.Vertical);
        }
        Debug.Log("horizontal = " + horizontal);
        Debug.Log("vertical = " + vertical);

        ////按下左或者右
        if (horizontal != 0)
        {
            ani.SetFloat("Horizontal", -1);
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
        Debug.Log("direction = " + dir.magnitude);
        ani.SetFloat("Speed", dir.magnitude);

        //朝该方向移动
        rb.velocity = dir * 2f;

        CreateEnemy();

        //resetPosition(true);
        //calculateDistance();
    }
    private float getAnimParam(float dir)
    {
        float res = (float)(dir > 0 ? Math.Ceiling(dir) : Math.Floor(dir));
        Debug.Log("calculateResult = " + res);
        return res;
    }

    public void TakeDamage(int damage)
    {
        blood -= damage;
        Debug.Log("当前血量 ---" + blood);
        if (blood <= 0)
        {
            //joystick.gameObject.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<MemuList>().Pause();
        }
    }

    public void Revive() {
        blood = 1000;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemies"))
        {
            EnemiesControl enemies = collision.gameObject.GetComponent<EnemiesControl>();
            if (enemies != null)
               TakeDamage(enemies.damage);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.tag.Equals("Enemies"))
        //{
        //    speed = 0.5f;
        //}

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
