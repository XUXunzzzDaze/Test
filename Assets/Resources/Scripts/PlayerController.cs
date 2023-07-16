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
        //ǰ��
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDir = Vector3.forward;
            //moveDir = transform.forward;
            FaceTo(moveDir);
            Move();
        }
        //����
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDir = Vector3.back;
            //moveDir = -transform.forward;
            FaceTo(moveDir);
            Move();
        }
        //����
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDir = Vector3.left;
            //moveDir = -transform.right;
            FaceTo(moveDir);
            Move();
        } 
        //����
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDir = Vector3.right;
            //moveDir = transform.right;
            FaceTo(moveDir);
            Move();
        }
        //ץȡ
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
            //ͬһ����
            if(nextTile == tile)
            {
                return;
            }
            //ǰһ��Ϊ��
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
            //ǰһ��������
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
        // ����������ͶӰ��ˮƽƽ��
        dir.y = 0f;
        dir.Normalize();

        // ��ȡĿ����ת�Ƕ�
        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);

        // Ӧ����ת�Ƕ�
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
            Debug.Log("��ȡ����");
        }
    }

}
