using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float rotationSpeed;
    public float moveSpeed;
    public Transform target;
    public List<Transform> cameras;

    private Vector3 lastMousePos;
    private Vector3 mousePos;

    private Vector3 mPos;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        DragCamera();
    }

    public void DragCamera()
    {
        // 按下鼠标右键并拖动时
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePos;
            float rotationX = deltaMousePosition.x * rotationSpeed;

            // 绕目标中心点旋转摄像机
            transform.RotateAround(target.position, Vector3.up, rotationX);
            lastMousePos = Input.mousePosition;
        }

        //x
        if (Input.GetMouseButtonUp(1))
        {
            int[] temps = { -270, -180, -90, 0, 90, 180, 270 };

            foreach (var temp in temps)
            {
                if (Mathf.Abs(this.transform.eulerAngles.y - temp) <= 45)
                {
                    switch(temp)
                    {
                        case -270:
                        case 90:
                            this.transform.position = cameras[1].position;
                            this.transform.rotation = cameras[1].rotation;
                            break;
                        case -180:
                        case 180:
                            this.transform.position = cameras[2].position;
                            this.transform.rotation = cameras[2].rotation;
                            break;
                        case -90:
                        case 270:
                            this.transform.position = cameras[3].position;
                            this.transform.rotation = cameras[3].rotation;
                            break;
                        case 0:
                            this.transform.position = cameras[0].position;
                            this.transform.rotation = cameras[0].rotation;
                            break;
                    }
                }
            }
        }

        //y
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - mousePos;
            float rotationY = deltaMousePosition.y * moveSpeed;

            transform.RotateAround(target.position, Vector3.back, rotationY);
            mousePos = Input.mousePosition;
        }


    }

    public void LookAtMenu()
    {
        mPos = transform.position;
    }

    public void UnLookMenu()
    {
        
    }


}
