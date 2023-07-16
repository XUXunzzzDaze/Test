using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDir;
    public Tile mTile;
    public Box mBox;
    public LayerMask targetLayer;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, moveDir,Color.red);

        GetInput();
    }

    public void Init()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out RaycastHit hit, 1, targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Tile"))
            {
                mTile = hit.collider.gameObject.GetComponent<Tile>();
                mTile.SetTopObj(gameObject);
            }
        }
    }

    public void GetInput()
    {
        //前进
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDir = Vector3.forward;
            //moveDir = transform.forward;
            FaceTo(moveDir);
            Move();
        }
        //后退
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDir = Vector3.back;
            //moveDir = -transform.forward;
            FaceTo(moveDir);
            Move();
        }
        //向左
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDir = Vector3.left;
            //moveDir = -transform.right;
            FaceTo(moveDir);
            Move();
        } 
        //向右
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDir = Vector3.right;
            //moveDir = transform.right;
            FaceTo(moveDir);
            Move();
        }
        //抓取
        else if (Input.GetKeyDown(KeyCode.E) && mTile != null)
        {
            Catch(moveDir);
        }
        
    }

    public void Move()
    {
        if(moveDir != Vector3.zero)
        {
            Tile nextTile = mTile.GetNextTile(moveDir);
            Tile tile = mTile;
            //同一格子
            if(nextTile == tile)
            {
                return;
            }
            //前一格为空
            if(nextTile.GetTopObj() == null)
            {
                mTile = nextTile;
                nextTile.SetTopObj(gameObject);
                transform.position = nextTile.transform.position + Vector3.up * 0.7f;
                if (mBox != null)
                {
                    Vector3 toBox = (mBox.transform.position - transform.position).normalized;
                    if ((toBox + moveDir.normalized).magnitude >1)
                    {
                        mBox.Move(moveDir);
                    }
                }
            }
            //前一格是箱子
            else if (nextTile.GetTopObj().CompareTag("Box"))
            {
                if (nextTile.GetTopObj().GetComponent<Box>().Move(moveDir))
                {
                    mTile = nextTile;
                    nextTile.SetTopObj(gameObject);
                    transform.position = nextTile.transform.position + Vector3.up * 0.7f;
                }
            }

            tile.SetTopObj(null);
        }
    }


    public void FaceTo(Vector3 dir)
    {
        // 将方向向量投影到水平平面
        dir.y = 0f;
        dir.Normalize();

        // 获取目标旋转角度
        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);

        // 应用旋转角度
        transform.rotation = targetRotation;
    }


    public void Catch(Vector3 dir)
    {
        if (mBox != null )
        {
            mBox.SetIsCatch(false);
            mBox = null;
        }
        else if(mTile.GetNextTile(dir).GetTopObj().CompareTag("Box"))
        {
            mBox = mTile.GetNextTile(dir).GetTopObj().GetComponent<Box>();
            mBox.SetIsCatch(true);
        }
        else
        {
            Debug.Log("获取不到");
        }
    }

}
