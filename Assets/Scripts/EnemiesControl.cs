using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemiesControl : MonoBehaviour
{
    private float speed = 0.5f;
    public GameObject target;
    private Rigidbody2D rb;

    private int blood = 70; 
    public int damage = 200; 
    
    private Vector3 scaleV;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) {
            return;
        }
        Vector3 v3 = target.transform.position - transform.position;
        scaleV = transform.localScale;
        if (v3.x > 0)
        {
            scaleV.x = -1.75f;
            transform.localScale = scaleV;
        }
        else
        {
            scaleV.x = 1.75f;
            transform.localScale = scaleV;
        }
        rb.velocity = Vector3.Normalize(v3) * speed;
    }

    public void TakeDamage(int damage)
    {
        blood -= damage;

        if (blood <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            speed = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            speed = 0.5f;
        }
    }
}
