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

    // Update is called once per frame
    void Update()
    {
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
        //rotateGun(1f);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void rotateGun(float angle)
    {
        //transform.Rotate(angle, angle, 0);
        transform.RotateAround(target.position + Vector3.up * 0.35f, Vector3.back, angle);
        //curRotateAngle = curRotateAngle + 180;
    }
}
