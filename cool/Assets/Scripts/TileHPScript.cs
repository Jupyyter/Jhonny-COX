using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileHPScript : MonoBehaviour//it is used for making copies of himself amd assigning them to damaged tiles to simulate HP
{
    private int HP=5;
    private Vector3Int cor;
    public int GetHP(){
        return HP;
    }
    public void SetHP(int HPP){
        HP=HPP;
    }
    public Vector3Int Getcor(){
        return cor;
    }
    public void Setcor(Vector3Int corr){
        cor=corr;
    }
}