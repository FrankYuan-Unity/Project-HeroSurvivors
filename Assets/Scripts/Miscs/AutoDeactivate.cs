using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{

    [SerializeField] bool destroyGameOj;

    [SerializeField] float lifeTime = 1f;

    WaitForSeconds waitLifetime;

    private void Awake()
    {
        waitLifetime = new WaitForSeconds(lifeTime);

    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateCoroutine());
    }
    IEnumerator DeactivateCoroutine()
    {
        yield return waitLifetime;

        if (destroyGameOj)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
