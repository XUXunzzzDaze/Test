using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo_Cube : MonoBehaviour
{
    private RaycastHit hit;
    private float height;
    private bool canMove;

    public LayerMask raycastMask;
    public Leo_Player player;
    public bool CanMove { get { return canMove; } }

    void Start()
    {
        Adjust();
        JudgeCanMove();
    }

    private void Update()
    {
        if (!player.IsPush && !player.IsPull)
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

    public void JudgeCanMove()
    {

    }
}
