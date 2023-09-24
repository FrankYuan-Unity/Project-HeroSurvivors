using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("---- DEATH ----")]
    [SerializeField] GameObject deathVFX;
    // [SerializeField] AudioData[] deathSFX;

    [Header("---- HEALTH ----")]
    [SerializeField] bool showOnHeadHealthBar = true;
    [SerializeField] StatusBar onHeadHealthBar;

    [SerializeField] CharacterData characterData;
     Sprite sprite;
    [SerializeField] public float Damage = 1f;

      [Header("---- New ----")]
    RuntimeAnimatorController controller;
    Animator animator;
    int healthPoint;
    int attackPower;
    int defencePower;
    int speed;
    int maxHealth;
    internal Coroutine hitCoroutine;
    internal SpriteRenderer spriteRenderer;
    public float health = 100f;

   protected virtual void Initialize()
    {
        healthPoint = characterData.GetHealthPoint();
        attackPower = characterData.GetAttackPower();
        defencePower = characterData.GetDefencePower();
        speed = characterData.GetSpeed();
        maxHealth = characterData.GetHealthPoint();
        sprite = characterData.GetSprite();
        controller = characterData.GetController();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = GetSprite();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = GetController();
        hitCoroutine = null;
    }

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



    protected void InitHealthPoint()
    {
        healthPoint = characterData.GetHealthPoint();
    }

    public int GetHealthPoint()
    {
        return healthPoint;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetDefencePower()
    {
        return defencePower;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public RuntimeAnimatorController GetController()
    {
        return controller;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    protected Animator GetAnimator()
    {
        return animator;
    }

    public virtual void ReduceHealthPoint(int damage)
    {
        if(healthPoint <= damage)
        {
            healthPoint = 0;
            Die();
        }
        else
        {
            healthPoint -= damage;
        }
    }

    public void RecoverHealthPoint(int amount)
    {
        if (healthPoint + amount > maxHealth)
        {
            healthPoint = maxHealth;
        }
        else
        {
            healthPoint += amount;
        }
    }

    public void IncreaseAttackPower(int value)
    {
        attackPower += value;
    }

    public void IncreaseDefencePower(int value)
    {
        defencePower += value;
    }

    public void IncreaseSpeed(int value)
    {
        speed += speed * value / 100;
    }

    public CharacterData.CharacterType GetCharacterType()
    {
        return characterData.GetCharacterType();
    }

    // public abstract void Die();

    protected abstract IEnumerator DieAnimation();

    protected abstract IEnumerator UnderAttack();
}