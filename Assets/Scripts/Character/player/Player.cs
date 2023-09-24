using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    private Rigidbody2D rb;
    public GameObject angryPigPrefab;
    public GameObject[] guns;
    private float createEnemyTime = 0.2f; //每两秒随机生成一次敌人
    private int gunIndex;


    public float fireTime = 1f; //初始每秒钟打出一发子弹，增加射速后此参数减小
    Vector3 size;

    [SerializeField] int experiencePoint = 0;
    [SerializeField] int currentLevel = 1;
    int[] levelExps = new int[] { 2, 5, 9 };

    [Header("------UI-----")]
    private int killedNo = 0;
    [SerializeField] Text levelText;
    [SerializeField] Text killedText;

    [SerializeField] Image expLevelBar;



    [SerializeField] Slider hpSlider;
    [SerializeField] ParticleSystem bleeding;
    [SerializeField] GameObject GameOverWindow;
    static Player instance;
    float attackSpeed;
    float expAdditional;
    int luck;
    bool isColliding;

    private Player() { }


      protected override void Initialize()
    {
        base.Initialize();
        GameOverWindow.SetActive(false);
        instance = this;
        attackSpeed = 200f;
        expAdditional = 1000f;
        luck = 0;
        hpSlider.maxValue = GetHealthPoint();
        hpSlider.value = GetHealthPoint();
        isColliding = false;

        GetFirstWeapon();
    }



    // Start is called before the first frame update
    void Start()
    {
        guns[1].SetActive(true);
        size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //获取组件
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // rb.gravityScale = 0f;
        input.EnableGameActionInput();
    }

    public void GainGems(int exp)
    {
        experiencePoint += exp;

        for (var i = 0; i < levelExps.Length; i++)
        {
            if (i == 0)
            {
                currentLevel = 1;
                expLevelBar.fillAmount = (float)experiencePoint / levelExps[i];

            }
            else
            {
                if (experiencePoint >= levelExps[i - 1] && experiencePoint < levelExps[i])
                {
                    currentLevel = i + 1;
                    expLevelBar.fillAmount = (float)(experiencePoint - levelExps[i - 1]) / (levelExps[i] - levelExps[i - 1]);
                }
                else if (experiencePoint > levelExps[levelExps.Length - 1])
                {
                    currentLevel = levelExps.Length;
                    expLevelBar.fillAmount = 1f;
                }

            }
        }
        levelText.text = "Lv." + currentLevel;
        guns[1].GetComponent<WeaponScript>().changeWeaponPower(currentLevel - 1);
    }


    public void addKilled()
    {
        killedNo++;

        killedText.text = killedNo.ToString();
    }

    public float moveSpeed = 5f;

    private Vector2 direction;
    // public InputActionAsset inputActions;
    // public InputAction movement;
    // public InputAction fire;


    private float getAnimParam(float dir)
    {
        float res = (float)(dir > 0 ? Math.Ceiling(dir) : Math.Floor(dir));
        Debug.Log("calculateResult = " + res);
        return res;
    }
    public void CreateEnemy()
    {
        createEnemyTime -= Time.deltaTime;
        Vector3 original = transform.position;
        if (createEnemyTime <= 0)
        {
            createEnemyTime = 2f;

            PoolManage.Release(angryPigPrefab, Viewport.Instance.RandomRightHalfPosition(0, 0));
        }
    }


    private void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGunIndexActive(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGunIndexActive(1);
        }

    }

    private void SetGunIndexActive(int index)
    {
        int length = guns.Length;
        if (index >= length || index < 0)
        {
            return;
        }
        for (int i = 0; i < length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[index].SetActive(true);
    }

    //复活
    public void Revive()
    {
        health = maxHealth;
        ShowOnHeadHealthBar();
        input.EnableGameActionInput();
        GameManager.GameState = GameState.Playing;

    }

    public override void Die()
    {
        base.Die();

        GameManager.onGameOver?.Invoke();
        GameManager.GameState = GameState.GameOver;

          PlayerMove.GetInstance().isDead = true;
        StartCoroutine(DieAnimation());
    }


     public static Player GetInstance()
    {
        return instance;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetExpAdditional()
    {
        return expAdditional;
    }

    public int GetLuck()
    {
        return luck;
    }

    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= value;
    }

    public void IncreaseExpAdditional(float value)
    {
        expAdditional += value;
    }

    public void IncreaseLuck(int value)
    {
        luck += value;
    }

   

    protected override IEnumerator DieAnimation()
    {
        GetAnimator().SetBool("Death", true);

        yield return new WaitForSeconds(1f);

        GameOverWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    void GetFirstWeapon()
    {
        switch (GetComponentInParent<Player>().GetCharacterType())
        {
            case CharacterData.CharacterType.Knight:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Whip);
                break;
            case CharacterData.CharacterType.Bandit:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Axe);
                break;
        }
    }

    public override void ReduceHealthPoint(int damage)
    {
        if (!PlayerMove.GetInstance().isDead)
        {
            base.ReduceHealthPoint(damage);

            hpSlider.value = GetHealthPoint();
            bleeding.Play();

            isColliding = true;

            if (hitCoroutine == null)
                hitCoroutine = StartCoroutine(UnderAttack());
        }
    }

    protected override IEnumerator UnderAttack()
    {
        spriteRenderer.color = Color.red;

        do
        {
            isColliding = false;
            yield return new WaitForSeconds(0.2f);
        }
        while (isColliding);

        spriteRenderer.color = Color.white;
        hitCoroutine = null;
    }
}
