using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private int damage = 40;// 子弹伤害
    private Vector3 startPosition;

    public float speed = 40f; // 子弹速度
    public float lifeTime = 2f; // 子弹寿命


    private void Start()
    {
    //     // rb = GetComponent<Rigidbody2D>();
        // rb.velocity = Vector2.right * speed;

        // 设置子弹寿命
        // Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // 在每一帧中移动子弹
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 当子弹触发其他碰撞器时调用
    private void OnTriggerEnter(Collider other)
    {
        // 如果碰到敌人
        if (other.CompareTag("Enemy"))
        {
            // 对敌人造成伤害
            other.GetComponent<EnemiesControl>().TakeDamage(damage);
            // 播放命中效果
            // ...

        }
        // 如果碰到其他物体
        else
        {
            // 播放其他效果
            // ...
            // 销毁子弹
            Destroy(gameObject);
        }
    }


}
