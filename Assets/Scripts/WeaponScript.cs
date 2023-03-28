using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public Transform target;
    public FixedJoystick shootJoystick;
    
    public Transform firePoint;

    private float curRotateAngle = 180f;

    public GameObject bulletPrefab;

    private Vector2 mousePos; //鼠标位置
    private Vector2 direction; //朝向

    private float flipY;

    private void Start()
    {
        flipY = transform.localScale.y;
    }


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateGun();

        if (transform.name.Equals("Gun001"))
        {
            Debug.Log("手枪开枪");
            Shoot();
        }
        else if (transform.name.Equals("Gun002"))
        {
            Debug.Log("冲锋枪开枪");
            GatlingShoot();
        }

    }

    void rotateGun(float angle)
    {
        //transform.Rotate(angle, angle, 0);
        //transform.RotateAround(target.position + Vector3.up * 0.35f, Vector3.back, angle);
        //curRotateAngle = curRotateAngle + 180;
    }

    const float gatlingSpeed = 0.1f;
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
#if UNITY_ANDROID  
         Vector2 dir  = Vector2.up * shootJoystick.Vertical + Vector2.right * shootJoystick.Horizontal;
     
          transform.right = dir.normalized;
          

        if (dir.x < 0) {
            transform.localScale = new Vector3(flipY, -flipY, 1);
        }
        else {
            transform.localScale = new Vector3(flipY, flipY, 1);
        }

#else
          direction = (mousePos - new Vector2(transform.position.x - 0.2f, transform.position.y + 0.3f)).normalized;
          transform.right = direction;
           if(mousePos.x < transform.position.x) {
            transform.localScale = new Vector3(flipY, -flipY, 1);	
          }
           else {
            transform.localScale = new Vector3(flipY, flipY, 1); 
          }
	#endif

    }
    
    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
