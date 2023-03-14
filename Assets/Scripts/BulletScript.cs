using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private int damage = 40;

    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //飞出摄像机视野销毁子弹
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        EnemiesControl enemies = collision.GetComponent<EnemiesControl>();
        if (enemies != null)
        {
            enemies.TakeDamage(damage);
            Debug.Log("enemy name" + collision.name);
        }
        Destroy(gameObject);
    }

}
