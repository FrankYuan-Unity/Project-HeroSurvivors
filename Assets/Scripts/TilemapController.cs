using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public abstract class TilemapController : MonoBehaviour
{

    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        //EventCenter.Instance.AddEventListener("PlayerTranslate", ResetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public abstract void ResetPosition(Vector3Int v); 
}
