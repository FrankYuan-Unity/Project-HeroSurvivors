using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "Player Input", order = 0)]
public class PlayerInput : ScriptableObject, GamePlayerActions.IPlayerActionsActions, GamePlayerActions.IPauseMenuActions
{
 
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction<Vector2> onRotateGun = delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction OnGamePause = delegate { };
    public event UnityAction OnGameUnpause = delegate { };


    GamePlayerActions gameActions;

    private void OnEnable()
    {
        gameActions = new GamePlayerActions();

        gameActions.PlayerActions.SetCallbacks(this);
        gameActions.PauseMenu.SetCallbacks(this);
    }

    public void SwitchActionsMap(InputActionMap actionsMap, bool isUIInput)
    {
        gameActions.Disable();
        actionsMap?.Enable();

        if (isUIInput)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnDisable()
    {
        DisableAllActions();
    }

    public void DisableAllActions()
    {
        gameActions.Disable();
    }

    public void EnableGameActionInput() => SwitchActionsMap(gameActions.PlayerActions, false);

    public void EnablePauseMenuInput() => SwitchActionsMap(gameActions.PauseMenu, true);

    public void EnableGameOverScreenInput() => SwitchActionsMap(null, true);

    public void SwithToDynamicUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

    public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

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
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("rotate performed");
            onRotateGun.Invoke(context.ReadValue<Vector2>());
        }

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnGamePause.Invoke();
        }

    }

    public void OnUnpause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnGameUnpause.Invoke();
        }
    }
}

