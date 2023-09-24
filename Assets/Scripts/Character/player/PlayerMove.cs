using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static PlayerMove instance;
    [SerializeField] PlayerInput input;
    SpriteRenderer spriteRenderer;
    Player character;
    float horizontal;
    float vertical;
    bool lookingLeft;
    public bool isDead;
    public FixedJoystick joystick; //摇杆
    private Animator ani;

    void Awake()
    {
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lookingLeft = false;
        instance = this;
        isDead = false;
    }
private void OnEnable() {
    
#if UNITY_EDITOR
        input.onMove += Move;
        input.onStopMove += StopMove;

#endif
        Debug.Log("movedirection");
        GameManager.GameState = GameState.Playing;
    }


    private void OnDisable()
    {
#if UNITY_EDITOR
        input.onMove -= Move;
        input.onStopMove -= StopMove;
#endif
    }


    private void StopMove()
    {
        rb.velocity = Vector2.zero;
        ani.SetFloat("Speed", 0);
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (!Level.GetIsLevelUpTime())
            {
                horizontal = _Horizontal();
                vertical = _Vertical();
            }

            if (Mathf.Abs(horizontal) >= 0.7f && Mathf.Abs(vertical) >= 0.7f)
            {
                horizontal = Mathf.Clamp(horizontal, -0.7f, 0.7f);
                vertical = Mathf.Clamp(vertical, -0.7f, 0.7f);
            }

            if (horizontal != 0f || vertical != 0f)
            {
            

                if (horizontal > 0f)
                {
                    spriteRenderer.flipX = false;
                    lookingLeft = false;
                }
                else if (horizontal < 0f)
                {
                    spriteRenderer.flipX = true;
                    lookingLeft = true;
                }
            }
           
        Vector2 moveInput = new  Vector2(horizontal, vertical);

        if (moveInput.x != 0)
        {
            ani.SetFloat("Horizontal", moveInput.x);
            ani.SetFloat("Vertical", 0);
        }
        if (moveInput.y != 0)
        {
            ani.SetFloat("Vertical", moveInput.y);
            ani.SetFloat("Horizontal", 0);
        }
        ani.SetFloat("Speed", moveInput.magnitude);

            if (!isDead)
            {
                transform.Translate(Vector2.right * horizontal * character.GetSpeed() / 10f * Time.deltaTime);
                transform.Translate(Vector2.up * vertical * character.GetSpeed() / 10f * Time.deltaTime);
            }
        }
    }

    private float _Horizontal(){
        #if UNITY_EDITOR
           return Input.GetAxisRaw("Horizontal");;
         #else
           return joystick.horizontal;
        #endif
    }

  private float _Vertical(){
        #if UNITY_EDITOR
          return Input.GetAxisRaw("Vertical");;
        #else
          return joystick.vertical;
        #endif
    }


    public static PlayerMove GetInstance()
    {
        return instance;
    }

    public bool GetLookingLeft()
    {
        return lookingLeft;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public float GetVertical()
    {
        return vertical;
    }
}