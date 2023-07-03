using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUpControl : MonoBehaviour
{
    [SerializeField] int exp = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("gameObject name :" + other.gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.TryGetComponent<Character>(out Character character))
            {
                other.gameObject.GetComponent<PlayerControl>().GainGems(exp);

                gameObject.SetActive(false);
            }
        }
    }
}
