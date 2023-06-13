using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] PlayerInput input;
    private Animator ani;
    private Rigidbody2D rb;
    public GameObject angryPigPrefab;
    public GameObject[] guns;
    private float createEnemyTime = 0.2f; //每两秒随机生成一次敌人
    private int gunIndex;
    // public FixedJoystick joystix1ck; //摇杆

    public int blood = 1000; //血量

    Vector3 size;

    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    private void Move(Vector2 moveInput)
    {
        print("我移动了" + moveInput.ToString());
        rb.velocity = moveInput * moveSpeed;

        if (moveInput.x != 0)
        {
            ani.SetFloat("Horizontal", -1);
            ani.SetFloat("Vertical", 0);
        }
        if (moveInput.y != 0)
        {
            ani.SetFloat("Vertical", moveInput.y);
            ani.SetFloat("Horizontal", 0);
        }
    }

    private void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        guns[1].SetActive(true);
        size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //获取组件
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // rb.gravityScale = 0f;
        input.EnableGameActionInput();

        // movement.Enable();
        // fire.Enable();
    }

    public float moveSpeed = 5f;
    public int health = 100;
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;
    private float nextFire;

    private Vector2 direction;
    // public InputActionAsset inputActions;
    // public InputAction movement;
    // public InputAction fire;


    void Update()
    {

        // SwitchGun();

        // 读取移动输入
        // Vector2 directionInput = movement.ReadValue<Vector2>();
        // direction = directionInput;

        // 读取射击输入
        // if (fire.triggered)
        // {
        //     Shoot();
        // }

        // 根据鼠标位置计算射击方向
        // Vector2 mousePos = Mouse.current.position.ReadValue();
        // mousePos.z = 0;
        // Vector2 aimDir = Camera.main.ScreenToWorldPoint(mousePos) - rb.position;
        // float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    void Shoot()
    {
        // 实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, rb.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 6;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // 触碰敌人
        if (coll.tag == "Enemy")
        {
            health -= 10;
            Debug.Log(health);
        }
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

    public void Revive()
    {
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