using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExidDoorScript : MonoBehaviour
{
    [HideInInspector] public bool exit = false;
    private int objectIN=0;
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Jhonny" || other.gameObject.tag == "Gangster"){
            objectIN--;
            if(objectIN==0){
                exit=true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Jhonny" || other.gameObject.tag == "Gangster"){
            objectIN++;
        }
    }
}
