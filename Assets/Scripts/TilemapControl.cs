using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapControl : MonoBehaviour
{

    GameObject mainCam;

    float map_width = 13;
    float map_height = 8;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mapPosition = transform.position;


        if (mainCam.transform.position.x - transform.position.x > map_width)
        {
            mapPosition.x += map_width * 2;


        }
        else if (mainCam.transform.position.x - transform.position.x < -map_width)
        {
            mapPosition.x -= map_width * 2;

        }

        if (mainCam.transform.position.y - transform.position.y > +map_height)
        {
            mapPosition.y += map_height * 2;
            // Debug.Log("3" + mapPosition.ToString());
        }
        else if (mainCam.transform.position.y - transform.position.y < -map_height)
        {
            mapPosition.y -= map_height * 2;
            // Debug.Log("4" + mapPosition.ToString());
        }

        transform.position = mapPosition;

    }
}
