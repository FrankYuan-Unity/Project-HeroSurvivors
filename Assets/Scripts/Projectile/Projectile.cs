using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public GameObject hitVFX;
    [SerializeField] public float damage;
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
   

}
