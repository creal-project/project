using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : SIngleTon<Spawner>
{
    public List<GameObject> enemyList;
    public List<GameObject> itemList;
    public List<Vector2> spawnPos;
    public bool itemSpawned=false;
    public void RandSpawn()
    {
        spawnPos.RemoveAt(0);
        if (spawnPos == null || spawnPos.Count == 0)
        {
            Debug.LogError("Spawn positions are empty or not set!");
            return;
        }

        if (enemyList == null || enemyList.Count == 0)
        {
            Debug.LogError("Enemy list is empty or not set!");
            return;
        }

        for (int i = 0; i < RoomManager.Instance.roomCount-1; i++)
        {
            // Make sure the spawnPos list has enough elements for the room count
            if (i >= spawnPos.Count)
            {
                Debug.LogWarning("Not enough spawn positions for room count.");
                continue;
            }

            for (int j = 0; j < 3; j++)
            {
                if (Random.Range(0, 100) < (100 - 20 * j))
                {
                    Vector2 randPos = new Vector2(
                        spawnPos[i].x + Random.Range(-5.5f, 5.5f),
                        spawnPos[i].y + Random.Range(-2.5f, 2.5f)
                    );
                    GameObject enemyPrefab = enemyList[Random.Range(0, enemyList.Count)];

                    if (enemyPrefab != null)
                    {
                        Instantiate(enemyPrefab, randPos, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogError("Enemy prefab is null!");
                    }
                }
            }
        }
    }
    public void itemRandSpawn(){
        itemSpawned = true;
        GameObject itemPrefab = itemList[Random.Range(0,itemList.Count)];
        Vector2 itemSpawn = GameManager.Instance.activatedRoomLocation;
        //if(GameManager.Instance.activatedRoomLocation == )
        if(Random.Range(0,2)>=0){
            Instantiate(itemPrefab,new Vector3(itemSpawn.x+Random.Range(-1f,1f),itemSpawn.y+Random.Range(-1f,1f),0),Quaternion.identity);
        }
    }
}
