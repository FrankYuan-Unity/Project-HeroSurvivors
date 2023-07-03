using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
 

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
