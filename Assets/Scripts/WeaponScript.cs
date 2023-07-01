using System.Collections;
using UnityEngine;

using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    public Transform target;
    public FixedJoystick shootJoystick;

    [SerializeField] public Transform firePointTop;
    [SerializeField] public Transform firePointMid;
    [SerializeField] public Transform firePointBottom;

    private float curRotateAngle = 180f;

    [SerializeField] public GameObject bulletPrefab1;
    [SerializeField] public GameObject bulletPrefab2;
    [SerializeField] public GameObject bulletPrefab3;

    [SerializeField, Range(0, 2)] int weaponPower = 0;


    private Vector2 mousePos; //鼠标位置
    private Vector2 direction; //朝向

    private float flipY;
    const float fireInterval = 0.5f;
    private WaitForSeconds waitForSeconds;
    private void Start()
    {
        flipY = transform.localScale.y;
        input.onRotateGun += RotateGun;
        waitForSeconds = new WaitForSeconds(fireInterval);
    }


    // Update is called once per frame
    void Update()
    {
        // if (Mouse.current.leftButton.wasPressedThisFrame)
        // {
        //     mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //     //     print(mousePos.ToString());
        // }


        if (Util.isMobile())
        {
            DetectMobileGunRotate();
        }

        if (transform.name.Equals("Gun001"))
        {
            // Debug.Log("手枪开枪");
            StartCoroutine(nameof(Fire));
        }
        else if (transform.name.Equals("Gun002"))
        {
            // Debug.Log("冲锋枪开枪");
            GatlingShoot();
        }

    }

    private float fireTime = fireInterval;
    private void GatlingShoot()
    {
        fireTime -= Time.deltaTime;
        if (fireTime <= 0)
        {
            fireTime = fireInterval;

            StartCoroutine(nameof(Fire));
            StopCoroutine(nameof(Fire));
        }
    }

    private void DetectMobileGunRotate()
    {
        Vector2 dir = Vector2.up * shootJoystick.Vertical + Vector2.right * shootJoystick.Horizontal;

        transform.right = dir.normalized;
        direction = dir;


        if (dir.x < 0)
        {
            transform.localScale = new Vector3(flipY, -flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipY, flipY, 1);
        }
    }

    private void RotateGun(Vector2 dir)
    {

        print("rotate" + dir.ToString());
        //  Vector2 dir2  = Vector2.up * shootJoystick.Vertical + Vector2.right * shootJoystick.Horizontal;

        transform.right = dir.normalized;

        if (dir.x < 0)
        {
            transform.localScale = new Vector3(flipY, -flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipY, flipY, 1);
        }
    }


    IEnumerator Fire()
    {
        while (true)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            switch (weaponPower)
            {
                case 0:
                    PoolManage.Release(bulletPrefab2, firePointMid.position);
                    break;
                case 1:
                    PoolManage.Release(bulletPrefab1, firePointTop.position);
                    PoolManage.Release(bulletPrefab3, firePointMid.position);

                    break;
                case 2:
                    PoolManage.Release(bulletPrefab1, firePointTop.position);
                    PoolManage.Release(bulletPrefab2, firePointMid.position);
                    PoolManage.Release(bulletPrefab3, firePointBottom.position);
                    break;
                default:
                    PoolManage.Release(bulletPrefab2, firePointMid.position);
                    break;

            }
            yield return waitForSeconds;
        }
    }

}
