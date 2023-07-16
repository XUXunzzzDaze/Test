using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo_Cube : MonoBehaviour
{
    private RaycastHit hit;
    private float height;
    public LayerMask raycastMask;

    void Start()
    {
        Adjust();
    }


    private void Adjust()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2000, raycastMask))
        {
            height = this.GetComponent<Renderer>().bounds.size.y / 2 + hit.collider.GetComponent<Renderer>().bounds.size.y / 2;
            transform.position = hit.collider.transform.position + new Vector3(0, height, 0);
        }
    }
}
