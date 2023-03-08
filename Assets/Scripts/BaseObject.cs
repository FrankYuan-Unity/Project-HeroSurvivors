using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{

    public const float leftBorder = -12.5f;
    public const float rightBorder = 0.5f;
    public const float topBorder = 8f;
    public const float bottomBorder = 0;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    // Update is called once per frame
    public void resetPosition(bool isPlayer)
    {
        Vector3 vector = transform.position;
     
        Vector3 translate = new Vector3();


        if (vector.x < leftBorder)
        {
            vector.x += 13f;
            translate.x = 13f;
        }

        if (vector.x > rightBorder)
        {

            vector.x -= 13f;
            translate.x = -13f;
        }

        if (vector.y < bottomBorder)
        {
            vector.y += 8f;
            translate.y = 8f;
        }

        if (vector.y > topBorder)
        {
            vector.y -= 8f;
            translate.y = -8f;
        }

        transform.position = vector;


    }


}
