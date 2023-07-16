using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Tile mTile;
    public bool isCatch;
    public Box AnotherOne;
    public bool moveAble;

    public LayerMask targetLayer;

    private void Start()
    {
        Init();
    }

    private void CheckMoveAble()
    {
        if (!AnotherOne.GetMoveAble())
        {
            moveAble = false;
        }

    }


    public void Init()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1,targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Tile"))
            {
                mTile = hit.collider.gameObject.GetComponent<Tile>();
                mTile.SetTopObj(gameObject);
            }
        }
    }

    public bool GetIsCatch()
    {
        return isCatch;
    }

    //在被玩家推动时才会调用
    public bool Move(Vector3 moveDir)
    {
        Tile nextTile = mTile.GetNextTile(moveDir);
        Tile tile = mTile;
        if (tile == nextTile)
        {
            return false;
        }
        if ((nextTile.GetTopObj() == null)||(nextTile.GetTopObj().CompareTag("Player")))
        {
            mTile = nextTile;
            nextTile.SetTopObj(gameObject);
            transform.position = nextTile.transform.position + Vector3.up * 0.7f;
            tile.SetTopObj(null);
            return true;
        }
        return false;
    }

    public bool GetMoveAble()
    {
        return moveAble;
    }


    public void SetIsCatch(bool able )
    {
        isCatch = able;
    }


}
