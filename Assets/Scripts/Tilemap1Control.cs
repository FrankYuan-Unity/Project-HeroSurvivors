using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap1Control : MonoBehaviour

{
    public Tilemap tilemap;

    public Transform target;

    public Vector3 translate = new Vector3();

    public Vector3 original = new Vector3(0.5f, 0.5f, 0);
    public Vector3Int v3Int = new Vector3Int();

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener("PlayerTranslate", ResetPosition);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPosition(Vector3 v)
    {
        translate = target.position - original;

        int sizeX = (int)translate.x / 13;
        int sizeY = (int)translate.y / 8;

        if (sizeX % 2 == 1) //奇数
        {
            tilemap.transform.Translate(new Vector3(sizeX * 13 + 13, 0, 0));
        }
        else
        {
            tilemap.transform.Translate(new Vector3(sizeX * 13, 0, 0));

        }

        if(sizeY % 2 == 1) {
            tilemap.transform.Translate(new Vector3(0, sizeY * 8 + 8, 0));
        }
        else {
            tilemap.transform.Translate(new Vector3(0, sizeY * 8, 0));
        }

    }
}
