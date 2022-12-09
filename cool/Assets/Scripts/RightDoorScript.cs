using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RightDoorScript : MonoBehaviour
{
    [SerializeField] LeftDoorScript leftdoorscript;
    [HideInInspector] public bool atRightSide = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (leftdoorscript.atLeftSide == false)
        {
            if (other.gameObject.tag == "Jhonny" || other.gameObject.tag == "Gangster")
            {
                atRightSide = true;
            }
        }
    }
}
