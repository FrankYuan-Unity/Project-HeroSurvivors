using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("---- DEATH ----")]
    [SerializeField] GameObject deathVFX;
    // [SerializeField] AudioData[] deathSFX;

    [Header("---- HEALTH ----")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] bool showOnHeadHealthBar = true;
    [SerializeField] StatusBar onHeadHealthBar;

    public float health = 100f;

    [SerializeField] public float Damage = 1f;

    protected virtual void OnEnable()
    {
        health = maxHealth;

        if (showOnHeadHealthBar)
        {
            ShowOnHeadHealthBar();
        }
        else
        {
            HideOnHeadHealthBar();
        }
    }

    public void ShowOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.UpdateStats(health, maxHealth);
    }

    public void HideOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage)
    {
        if (health <= 0f)
        {
            Die();
        }
        if (health == 0f) return;

        health -= damage;

        Debug.Log("Take damage : " + damage + "gameobject name is :" + name);
        if (showOnHeadHealthBar)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }
    }

    public virtual void Die()
    {
        health = 0f;
        // AudioManager.Instance.PlayRandomSFX(deathSFX);
        PoolManage.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
        HideOnHeadHealthBar();
    }

    public virtual void RestoreHealth(float value)
    {
        if (health == maxHealth) return;

        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        health = Mathf.Clamp(health + value, 0f, maxHealth);

        if (showOnHeadHealthBar)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }
    }

    protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health < maxHealth)
        {
            yield return waitTime;

            RestoreHealth(maxHealth * percent);
        }
    }

    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health > 0f)
        {
            yield return waitTime;

            TakeDamage(maxHealth * percent);
        }
    }
}