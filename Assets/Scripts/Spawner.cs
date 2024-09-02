using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SIngleTon<Spawner>
{
    public List<GameObject> enemyList;
    public List<GameObject> itemList;
    public List<Vector2> spawnPos;
    int roomCount = 0;
    void Start()
    {
        //아이템 추가 코드
    }
    void Update()
    {
        if (RoomManager.Instance.generationComplete)
        {
            if (RoomManager.Instance.roomGenerateCount != roomCount)
            {
                spawnPos = GameManager.Instance.roomLocation;
                roomCount++;
                spawnPos.RemoveAt(0);
                //spawnPos.RemoveAt(stairPositionIndex);
            }
        }
    }
    public void RandSpawn()
    {

    }
}
