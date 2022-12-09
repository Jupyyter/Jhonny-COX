using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DeadTile : MonoBehaviour
{// ⋆｡˚ᎶᎾᎾⅅ ℕᏐᎶℍᎢ⋆｡˚✩
    private Tilemap Map;
    private MapManager mapManager;
    private Dictionary<Vector3Int, int> tilePile;

    private void Awake()
    {
        tilePile = new Dictionary<Vector3Int, int>();
        Map = GetComponent<Tilemap>();
        mapManager = FindObjectOfType<MapManager>();
    }
    private void OnCollisionEnter2D(Collision2D col)//called when collides
    {
        if (col.gameObject.CompareTag("Bullet"))//if a bullet strikes
        {
            Vector3 hitPosition = Vector3.zero;
            ContactPoint2D[] contacts = new ContactPoint2D[3];
            int cumContacts = col.GetContacts(contacts);
            for (int i = 0; i < 1; i++)
            {
                hitPosition.x = contacts[i].point.x + 0.05f * contacts[i].normal.x;
                hitPosition.y = contacts[i].point.y + 0.05f * contacts[i].normal.y;
                Vector3Int truLocation = Map.WorldToCell(hitPosition);
                if (Map.GetTile(truLocation) != null && mapManager.getTileHP(truLocation) != -1)
                {
                    if (!tilePile.ContainsKey(truLocation))
                    {
                        tilePile.Add(truLocation, mapManager.getTileHP(truLocation));
                    }
                    harasTile(truLocation);
                }
            }
        }
    }
    private void harasTile(Vector3Int location)
    {
        tilePile[location]--;
        if (tilePile[location] == 0)//if tile at location has hp==0
        {
            mapManager.spawnParticles(location,Map.GetCellCenterWorld(location));
            Map.SetTile(location, null);//dies
            Map.SetTile(new Vector3Int(location.x, location.y + 1, location.z), null);
            tilePile.Remove(location);
        }
    }
    public void giveTiles(Vector3 vec)
    {
        Vector3Int truLocation = Map.WorldToCell(vec);
        if (Map.GetTile(truLocation) != null && mapManager.getTileHP(truLocation) != -1)
        {
            if (!tilePile.ContainsKey(truLocation))
            {
                tilePile.Add(truLocation, mapManager.getTileHP(truLocation));
            }
            harasTile(truLocation);
        }
    }
    //:):):):):):):):):):):):):):):):):):):):):):):):):):):):)
}
/*
⠄⠄⠄⠄⠄⠄⠄⠄⣀⣤⡴⠶⠟⠛⠛⠛⠛⠻⠶⢦⣤⣀⠄⠄⠄⠄⠄⠄⠄⠄
⠄⠄⠄⠄⠄⣠⣴⡟⠋⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠙⢻⣦⣄⠄⠄⠄⠄⠄
⠄⠄⠄⣠⡾⠋⠈⣿⣶⣄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⣠⣶⣿⠁⠙⢷⣄⠄⠄⠄
⠄⠄⣴⠏⠄⠄⠄⠸⣇⠉⠻⣦⣀⠄⠄⠄⠄⣀⣴⠟⠉⣸⠇⠄⠄⠄⠹⣦⠄⠄
⠄⣼⠏⠄⠄⠄⠄⠄⢻⡆⠄⠄⠙⠷⣦⣴⠾⠋⠄⠄⢰⡟⠄⠄⠄⠄⠄⠹⣧⠄
⢰⡏⠄⠄⠄⠄⠄⠄⠈⣷⠄⢀⣤⡾⠋⠙⢷⣤⡀⠄⣾⠁⠄⠄⠄⠄⠄⠄⢹⡆
⣿⠁⠄⠄⠄⠄⠄⠄⠄⣸⣷⠛⠁⠄⠄⠄⠄⠈⠛⣾⣇⠄⠄⠄⠄⠄⠄⠄⠄⣿
⣿⠄⠄⠄⠄⠄⣠⣴⠟⠉⢻⡄⠄ AYAYA ⠄⣾⡟⠻⣦⣄⠄⠄⠄⠄⠄⣿
⣿⡀⠄⢀⣴⠞⠋⠄⠄⠄⠈⣷⠄⠄⠄⠄⠄⠄⣾⠁⠄⠄⠄⠙⠳⣦⡀⠄⠄⣿
⠸⣧⠾⠿⠷⠶⠶⠶⠶⠶⠶⢾⣷⠶⠶⠶⠶⣾⡷⠶⠶⠶⠶⠶⠶⠾⠿⠷⣼⠇
⠄⢻⣆⠄⠄⠄⠄⠄⠄⠄⠄⠄⢿⡄⠄⠄⢠⡿⠄⠄⠄⠄⠄⠄⠄⠄⠄⣰⡟⠄
⠄⠄⠻⣆⠄⠄⠄⠄⠄⠄⠄⠄⠘⣷⠄⠄⣾⠃⠄⠄⠄⠄⠄⠄⠄⠄⣰⠟⠄⠄
⠄⠄⠄⠙⢷⣄⠄⠄⠄⠄⠄⠄⠄⢹⣇⣸⡏⠄⠄⠄⠄⠄⠄⠄⣠⡾⠋⠄⠄⠄
⠄⠄⠄⠄⠄⠙⠳⣦⣄⡀⠄⠄⠄⠄⢿⡿⠄⠄⠄⠄⢀⣠⣴⠞⠋⠄⠄⠄⠄⠄
⠄⠄⠄⠄⠄⠄⠄⠄⠉⠛⠳⠶⣦⣤⣼⣧⣤⣴⠶⠞⠛⠉⠄⠄⠄⠄⠄⠄⠄⠄
*/