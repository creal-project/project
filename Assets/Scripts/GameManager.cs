using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : SIngleTon<GameManager>
{
    public GameObject player;
    public List<Vector2> roomLocation;
    public Collider2D[] enemyInRoom;
    public Vector2 activatedRoomLocation = new Vector2(0,0);
    public int currentPlayerRoom;
    public bool isAllowToMove = false;
    public LayerMask whatIsLayer;
    public bool IsThereEnemy = false;
    public float enemyDetectDistence;
    public float enemyAttackDistence;
    
    void FixedUpdate(){
        enemyInRoom = Physics2D.OverlapBoxAll(activatedRoomLocation,new Vector2(15,7),0,whatIsLayer);
        if(enemyInRoom.Length==0){
            isAllowToMove = true;
        }
        else{
            isAllowToMove = false;
        }
        if(RoomManager.Instance.generationComplete){
            FindPlayerRoom();
        }
        FindAnyEnemy();
    }
    void FindPlayerRoom(){
        for(int i=0;i<RoomManager.Instance.roomCount;i++){
            if(player.transform.position.x>=((roomLocation[i].x)-15)&&player.transform.position.x<=((roomLocation[i].x)+15)){
                if(player.transform.position.y>=((roomLocation[i].y)-7)&&player.transform.position.y<=((roomLocation[i].y)+7)){
                    activatedRoomLocation=roomLocation[i];
                    currentPlayerRoom = i;
                    return;
                }
            }
        }
    }
  
    void FindAnyEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if(enemies.Length == 0)
        {
            IsThereEnemy = true;
        }
        else{
            IsThereEnemy = false;
        }
        
    }
}
