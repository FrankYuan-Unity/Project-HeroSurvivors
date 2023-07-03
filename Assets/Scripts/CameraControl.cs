
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;


    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vector = transform.position;
        vector.x = target.position.x;
        vector.y = target.position.y;
    
        transform.position = vector;

    }
}
