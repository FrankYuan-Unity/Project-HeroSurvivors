using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toast : MonoBehaviour
{
    public Text toastText;
    public float duration = 3.0f;

    public void Show(string text)
    {
        toastText.text = text;
        StartCoroutine(ShowToast());
    }

    IEnumerator ShowToast()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}

