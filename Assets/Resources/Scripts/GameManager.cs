using System;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float rotateSpeed;
    public bool rotateAble;

    public bool isPause;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        GetInput();
    }

    
    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                UIManager.instance.ActiveMenu();
                CameraController.instance.LookAtMenu();
            }
            else
            {
                UIManager.instance.CloseMenu();
                CameraController.instance.UnLookMenu();
            }
        }

    }

    /*private void GetInput()
    {
        if (Input.GetButtonDown("Fire1") && rotateAble)
            NextView();
        else if (Input.GetButtonDown("Fire2") && rotateAble)
            LastView();
    }
    private readonly Vector3[] directions = new Vector3[]
    {
        new Vector3(0, 0, 0),   //A面
        new Vector3(0, 90, 0),  //B面
        new Vector3(0, 180, 0), //C面
        new Vector3(0, 270, 0)  //D面
    };
    public enum Dir
    {
        A = 1,
        B = 2, 
        C = 3, 
        D = 4,
    }
    public void CheckView()
    {
        if(masks.rotation.y < 0 && masks.rotation.y % 90 == 0)
        {
            masks.rotation = Quaternion.Euler(new Vector3(0, masks.rotation.y + 360, 0));
        }
        //标准角判断
        if(masks.rotation.y == directions[GetDirection()-1].y)
        {
            rotateAble= true;
        }
        else
        {
            rotateAble= false;  
        } 
    }
    public int GetDirection() {
        return (int)dir;
    }
    public void NextView()
    {
        int value = ( GetDirection() % 4) + 1;
        dir = (Dir)Enum.ToObject(typeof(Dir),value);
        targetRotation = Quaternion.Euler(directions[GetDirection() - 1]);
    }
    public void LastView()
    {
        int value = ( GetDirection() + 2) % 4 + 1;
        dir = (Dir)Enum.ToObject(typeof(Dir),value);
        targetRotation = Quaternion.Euler(directions[GetDirection() - 1]);
    }
    private void UpdateRotation()
    {
        masks.rotation = Quaternion.Lerp(masks.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
    }*/

}
