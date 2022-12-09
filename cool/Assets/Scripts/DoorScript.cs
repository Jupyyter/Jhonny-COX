using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    private bool DoorOpen = false;
    private BoxCollider2D right;
    private BoxCollider2D left;
    private SpriteRenderer sprt;
    private BoxCollider2D THEdoor;
    [SerializeField] private RightDoorScript rightdoorscript;
    [SerializeField] private LeftDoorScript leftdoorscript;
    [SerializeField] private ExidDoorScript exitdoorscript;
    [SerializeField] private Sprite doorOpenLeft;
    [SerializeField] private Sprite doorOpenRight;
    [SerializeField] private Sprite doorClosed;
    private int hp = 3;
    void Start()
    {
        THEdoor = GetComponent<BoxCollider2D>();
        sprt = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!DoorOpen)
        {
            if (rightdoorscript.atRightSide == true)
            {
                sprt.sprite = doorOpenLeft;
                DoorOpen = true;
                THEdoor.enabled = false;
            }
            else if (leftdoorscript.atLeftSide == true)
            {
                sprt.sprite = doorOpenRight;
                DoorOpen = true;
                THEdoor.enabled = false;
            }
        }
        else if (exitdoorscript.exit == true)
        {
            leftdoorscript.atLeftSide = false;
            rightdoorscript.atRightSide = false;
            exitdoorscript.exit = false;
            sprt.sprite = doorClosed;
            DoorOpen = false;
            THEdoor.enabled = true;
        }
    }
    public void DoorDamage()//called when collides
    {
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }
}