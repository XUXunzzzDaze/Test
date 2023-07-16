using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float rotationSpeed;
    public float moveSpeed;
    public Transform target;

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
        // ��������Ҽ����϶�ʱ
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePos;
            float rotationX = deltaMousePosition.x * rotationSpeed;

            // ��Ŀ�����ĵ���ת�����
            transform.RotateAround(target.position, Vector3.up, rotationX);
            lastMousePos = Input.mousePosition;
        }


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
