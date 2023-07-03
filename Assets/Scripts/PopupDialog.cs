using UnityEngine;
using System;
using UnityEngine.UI;
 
public class PopupDialog : MonoBehaviour
{
    public Canvas canvas;        // 提示框Canvas
    public Text messageText;    // 提示信息Text
    public Button confirmButton;// 确认Button
    public Button cancelButton; // 取消Button

    public event Action ConfirmButtonClicked;
    public event Action CancelButtonClicked;

    
    private void Start() 
    {
        canvas.gameObject.SetActive(false);

        // 确认Button点击事件
        confirmButton.onClick.AddListener(OnConfirmButtonClicked); 
        
        // 取消Button点击事件 
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    // 显示提示框
    public void ShowMessage(string message)
    {
        Debug.Log("点击 显示弹窗");
        canvas.gameObject.SetActive(true);
        messageText.text = message;
    }

    // 隐藏提示框
    public void HideMessage()
    {
        canvas.gameObject.SetActive(false);
    }
    
    // 订阅确认Button点击事件
    public void SubscribeToConfirmButton(Action handler)
    {
        ConfirmButtonClicked += handler; 
    }

    // 订阅取消Button点击事件
    public void SubscribeToCancelButton(Action handler) 
    {
        CancelButtonClicked += handler;
    }

    // 确认Button点击回调
    private void OnConfirmButtonClicked()
    {
        ConfirmButtonClicked?.Invoke();
    }
    
    // 取消Button点击回调
    private void OnCancelButtonClicked()
    {
        CancelButtonClicked?.Invoke();
    }
}

