using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject topObj;
    public LayerMask targetLayer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        LayerMask layer = gameObject.layer;
        Debug.Log(layer);
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1, targetLayer))
        {
            topObj = hit.collider.gameObject;
        }
    }
    public Tile GetNextTile(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 1.5f, targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Tile")) 
            {
                return hit.collider.gameObject.GetComponent<Tile>();
            }
        }
        return gameObject.GetComponent<Tile>();
    }
    public void SetTopObj(GameObject obj)
    {
        topObj= obj;
    }
    public GameObject GetTopObj()
    {
        return topObj;
    }

}
