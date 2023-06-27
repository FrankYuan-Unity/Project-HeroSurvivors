using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] PlayerInput input;

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
        input.onRotateGun += RotateGun;
    }




    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (transform.name.Equals("Gun001"))
        {
            // Debug.Log("手枪开枪");
            Shoot();
        }
        else if (transform.name.Equals("Gun002"))
        {
            // Debug.Log("冲锋枪开枪");
            GatlingShoot();
        }

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

    private void RotateGun(Vector2 dir)
    {

        print(dir.ToString());


        print(dir.ToString());
        //  Vector2 dir  = Vector2.up * shootJoystick.Vertical + Vector2.right * shootJoystick.Horizontal;

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

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
