using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float damage;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] Vector2 moveDirection;
    Coroutine coroutine;

    void OnEnable()
    {
        coroutine = StartCoroutine(MoveDirectly());
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator MoveDirectly()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("bullet has been triggered");

        if (other.gameObject.TryGetComponent<Character>(out Character character))
        {
            character.TakeDamage(damage);
            var contactPoint = other.GetContact(0);
            PoolManage.Release(hitVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            gameObject.SetActive(false);
        }

    }

}
