using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<FKTILES> tileDatas;
    private Dictionary<TileBase, FKTILES> dataFromTiles;//first is the key
    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, FKTILES>();
        foreach (FKTILES tileData in tileDatas)
        {
            try{
                foreach (Tile tile in tileData.tiles)
                {

                    dataFromTiles.Add(tile, tileData);
                }
            }
            catch{
                foreach (RuleTile tile in tileData.tiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }
        }
    }
    public int getTileHP(Vector3Int location)
    {
        if (dataFromTiles.ContainsKey(map.GetTile(location)))
        {
            TileBase tilee = map.GetTile(location);
            return dataFromTiles[tilee].hp;
        }
        else return -1;
    }
    public void spawnParticles(Vector3Int location,Vector3 midLocation)
    {
        if (dataFromTiles.ContainsKey(map.GetTile(location)))
        {
            TileBase tilee = map.GetTile(location);
            Instantiate(dataFromTiles[tilee].particles,midLocation,new Quaternion());
        }
        //Instantiate(tileBreakPrefab, cell + cellOffset, Quaternion.identity);
    }
    /*public int damageTile(Vector2 worldPosition){
        Vector3Int gridPosition=map.WorldToCell(worldPosition);
        TileBase tile=map.GetTile(gridPosition);
        if(tile!=null){
            dataFromTiles[tile].hp--;
            return dataFromTiles[tile].hp;
        }
        else return -1;
    }*/

}
