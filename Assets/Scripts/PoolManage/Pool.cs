using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    Queue<GameObject> queue;

    [SerializeField] int size = 1;

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }

    public int Size => size;

    public int RuntimeSize => queue.Count;


    [SerializeField] public GameObject prefab;

    Transform parent;

    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        GameObject oj = GameObject.Instantiate(prefab, parent);
        oj.SetActive(false);
        return oj;
    }

    private GameObject AvailableGameObject()
    {
        GameObject availableGameObject = null;

        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            availableGameObject = queue.Dequeue();
        }
        else
        {
            availableGameObject = Copy();
        }
        queue.Enqueue(availableGameObject);
        return availableGameObject;

    }

    public GameObject PrepareObject()
    {
        GameObject gameObject = AvailableGameObject();
        gameObject.SetActive(true);
        return gameObject;
    }

    public GameObject PrepareObject(Vector3 position)
    {
        GameObject gameObject = AvailableGameObject();
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        return gameObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = AvailableGameObject();
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        return gameObject;
    }

    public GameObject PrepareObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject gameObject = AvailableGameObject();
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = localScale;
        return gameObject;
    }
}
