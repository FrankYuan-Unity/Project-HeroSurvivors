using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemiesControl : Character
{
    [SerializeField] float speed = 1f;

    [SerializeField] GameObject gemObject;
    GameObject target;
    private int blood = 70;
    public int damage = 200;

    private Vector3 scaleV;

    private int killedNo = 0;
    

    // float paddingX, paddingY;

    Rigidbody2D rb;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    protected virtual void Awake()
    {
        // var size = transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        // paddingX = size.x / 2f;
        // paddingY = size.y / 2f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rb = GetComponent<Rigidbody2D>();
        // StartCoroutine(MoveBehavior());
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Die()
    {
        base.Die();
        PoolManage.Release(gemObject, transform.position);
        target.GetComponent<PlayerControl>().addKilled();
    }


    private void Update()
    {
        if (target == null)
            return;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision has been triggered :" + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player"))
        {
            bool canDamage = collision.gameObject.TryGetComponent<Character>(out Character character);
            if (canDamage)
                collision.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
        }
    }

}

