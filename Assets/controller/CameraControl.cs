using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;

    private float leftBorder = -7.5f;
    private float rightBorder = 6.94f;
    private float upBorder = 3.11f;
    private float downBorder = -3.77f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vector = transform.position;
        vector.x = target.position.x;
        vector.y = target.position.y;
        if(vector.x < leftBorder) {
            vector.x = leftBorder;
	    }
        
        if(vector.x > rightBorder) {
            vector.x = rightBorder;
        }

        if (vector.y > upBorder) {
            vector.y = upBorder;
        }
        if (vector.y < downBorder) {
            vector.y = downBorder;
        }


        transform.position = vector;

    }
}
