using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class FKTILES : ScriptableObject
{
    public TileBase[] tiles;
    public int hp = 20;
    public ParticleSystem particles;
}