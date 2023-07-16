using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo_Player : MonoBehaviour
{
    private float height;
    private RaycastHit hit;
    private bool canAdjust;
    private Vector3 input;
    private Quaternion target;

    public LayerMask raycastMask;
    public Transform mainCamera;
    public float speed;
    public float rotateSpeed;

    private void Start()
    {
        Adjust();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (canAdjust)
            Adjust();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Adjust()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2000, raycastMask))
        {
            height = this.GetComponent<Renderer>().bounds.size.y / 2 + hit.collider.GetComponent<Renderer>().bounds.size.y / 2;
            transform.position = hit.collider.transform.position + new Vector3(0, height, 0);
        }
    }

    private void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            input = convertDirection(mainCamera.forward);
            target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y, 0));
            canAdjust = false;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            input = convertDirection(mainCamera.forward * -1);
            target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y + 180, 0));
            canAdjust = false;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            input = convertDirection(mainCamera.right);
            target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y + 90, 0));
            canAdjust = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input = convertDirection(mainCamera.right * -1);
            target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y - 90, 0));
            canAdjust = false;
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            canAdjust = true;
        }
    }

    private Vector3 convertDirection(Vector3 input)
    {
        Vector3 output;
        output = new Vector3(input.x, 0, input.z).normalized;

        return output;
    }

    private void Move()
    {
        transform.position += input * speed * Time.deltaTime;
        TurnAround(target);
    }

    private void TurnAround(Quaternion target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateSpeed);
    }
}
