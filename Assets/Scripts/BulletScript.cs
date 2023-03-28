using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private int damage = 40;
    private float destoryDistance = 40f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * speed;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPosition).sqrMagnitude;

        if(distance > destoryDistance) {
            Destroy(gameObject);
        }

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
