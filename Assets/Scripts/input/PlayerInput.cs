using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "Player Input", order = 0)]
public class PlayerInput : ScriptableObject, GamePlayerActions.IPlayerActionsActions, GamePlayerActions.IWeaponActionsActions
{

    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction<Vector2> onRotateGun = delegate { };

    public event UnityAction onStopMove = delegate { };
    GamePlayerActions gameActions;

    private void OnEnable()
    {
        gameActions = new GamePlayerActions();

        gameActions.PlayerActions.SetCallbacks(this);
    }

    void OnDisable()
    {
        DisableAllActions(); 
    }

    public void DisableAllActions()
    {
        gameActions.PlayerActions.Disable();
    }

    public void EnableGameActionInput()
    {
        gameActions.PlayerActions.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void OnFire(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }

    public void OnRotateGun(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed){
            onRotateGun.Invoke(context.ReadValue<Vector2>());
        }

    }
}

