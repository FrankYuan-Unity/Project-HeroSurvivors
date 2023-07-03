using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    float currentFillAmount = 1f;

    private void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }
    }

    public void UpdateStats(float currentValue, float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        fillImage.fillAmount = currentFillAmount;
    }

}
