using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    public float interval; //枪械间隔

    public GameObject bulletPrefab;

    public GameObject shellPrefab; //弹壳

    private Transform muzzlePos; // 枪口位置

    private Transform shellPos;  //弹壳位置

    private Vector2 mousePos; //鼠标位置

    private Vector2 direction; //朝向

    private float timer;

    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
