using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeftDoorScript : MonoBehaviour
{
    [SerializeField] RightDoorScript rightdoorscript;
    [HideInInspector] public bool atLeftSide = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (rightdoorscript.atRightSide == false)
        {
            if (other.gameObject.tag == "Jhonny" || other.gameObject.tag == "Gangster")
            {
                atLeftSide = true;
            }
        }
    }
}
