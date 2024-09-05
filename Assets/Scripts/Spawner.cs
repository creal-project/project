using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : SIngleTon<Spawner>
{
    public List<GameObject> enemyList;
    public List<Vector2> spawnPos;
    int roomCount = 0;
    void Start()
    {
        //아이템 추가 코드
    }
    void getPosition()
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
        getPosition();
        for (int i = 0; i < RoomManager.Instance.roomCount; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Random.Range(0, 100) < (100 - 20 * j))
                {
                    Vector2 randPos = new Vector2(spawnPos[i].x+Random.Range(-5.5f, 5.5f),spawnPos[i].y+Random.Range(-2.5f, 2.5f));
                    Instantiate(enemyList[Random.Range(0,enemyList.Count)],randPos,Quaternion.identity);
                }
            }
        }
    }
    void Update(){
        bool isCalled=false;
        if(RoomManager.Instance.generationComplete){
            if(!isCalled){
                RandSpawn();
                isCalled = true;
            }
        }
    }
}
