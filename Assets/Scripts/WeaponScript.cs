using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform transform;
    public Transform target;

    public Transform firePoint;

    private float curRotateAngle = 180f;

    public GameObject bulletPrefab;

    private Vector2 mousePos; //鼠标位置
    private Vector2 direction; //朝向

    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateGun();

        //获取水平轴 -1 0 1
        float horizontal = Input.GetAxisRaw("Horizontal");
        //if (horizontal < 0 && curRotateAngle ==0)
        //{
        //    rotateGun(180f);
        //    curRotateAngle = 180f;
        //}
        //else if (horizontal > 0 && curRotateAngle == 180f)
        //{
        //    rotateGun(180f);
        //    curRotateAngle = 0f;
        //}
        rotateGun(1f);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && transform.name.Equals("Gun002"))
        {
            GatlingShoot();
        }

    }

    void rotateGun(float angle)
    {
        //transform.Rotate(angle, angle, 0);
        //transform.RotateAround(target.position + Vector3.up * 0.35f, Vector3.back, angle);
        //curRotateAngle = curRotateAngle + 180;
    }

    const float gatlingSpeed = 0.01f;
    private float gatlingTime = gatlingSpeed;

    private void GatlingShoot()
    {
        gatlingTime -= Time.deltaTime;
        if (gatlingTime <= 0)
        {
            Shoot();
            gatlingTime = gatlingSpeed;
        }

    }

    private void RotateGun()
    {
        direction = (mousePos - new Vector2(transform.position.x - 0.2f, transform.position.y + 0.3f)).normalized;
        transform.right = direction;
    }
    private void Shoot()
    {


        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
