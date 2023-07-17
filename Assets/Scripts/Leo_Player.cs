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
    private bool isPush;
    private bool canPush;
    private bool isPull;
    private bool canPull;
    private bool canMove;
    private Transform box;
    private Vector3 distance;

    public LayerMask raycastMask;
    public Transform mainCamera;
    public float speed;
    public float rotateSpeed;

    public bool IsPush { get { return isPush; } }
    public bool IsPull { get { return isPull; } }

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

        if (Physics.Raycast(transform.position, transform.forward, out hit, this.GetComponent<Renderer>().bounds.size.x - 0.3f, raycastMask))
        {
            if(hit.collider.tag.CompareTo("Box") == 0)
            {
                canPush = true;
                box = hit.collider.transform;
                distance = box.position - transform.position;
            }
        }
        else
        {
            isPush = false;
            canPush = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, this.GetComponent<Renderer>().bounds.size.x, raycastMask))
        {
            if (hit.collider.tag.CompareTo("Box") == 0)
            {
                canPull = true;
                box = hit.collider.transform;
                distance = box.position - transform.position;
            }
        }
        else
        {
            canPull = false;
            isPull = false;
        }

        if(box != null)
        {
            canMove = box.GetComponent<Leo_Cube>().CanMove;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Push();
        Pull();
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
            if (!isPull || !RoundJudge(input, transform.forward * -1))
                target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y, 0));
            canAdjust = false;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            input = convertDirection(mainCamera.forward * -1);
            if (!isPull || !RoundJudge(input, transform.forward * -1))
                target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y + 180, 0));
            canAdjust = false;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            input = convertDirection(mainCamera.right);
            if (!isPull || !RoundJudge(input, transform.forward * -1))
                target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y + 90, 0));
            canAdjust = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input = convertDirection(mainCamera.right * -1);
            if (!isPull || !RoundJudge(input, transform.forward * -1))
                target = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y - 90, 0));
            canAdjust = false;
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            canAdjust = true;
            input = Vector3.zero;
            isPush = false;
            isPull = false;
        }

        if(Input.GetKey(KeyCode.J))
        {
            isPull = true;
            if(box != null)
                distance = box.position - transform.position;
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            isPull = false;
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

    private void Push()
    {
        if(canPush && RoundJudge(input, transform.forward))
        {
            Debug.Log("1111");
            isPush = true;
            box.position = transform.position + distance;
        }
    }

    private void Pull()
    {
        if(isPull && RoundJudge(input, transform.forward * -1))
        {
            box.position = transform.position + distance;
        } 
    }

    private bool RoundJudge(Vector3 input, Vector3 basis)
    {
        if (Vector3.Distance(input, basis) < 0.3f)
            return true;
        return false;
    }
}
