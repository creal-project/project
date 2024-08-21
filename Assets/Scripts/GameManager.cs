using System.Collections.Generic;
using UnityEngine;

public class GameManager : SIngleTon<GameManager>
{
    public GameObject player;
    public List<Vector2> roomLocation;
    public Collider2D[] enemyInRoom;
    public Vector2 activatedRoomLocation;
    public int currentRoom;
    public bool isAllowToMove = false;
    public LayerMask whatIsLayer;
    void FixedUpdate(){
        enemyInRoom = Physics2D.OverlapBoxAll(activatedRoomLocation,new Vector2(15,7),0,whatIsLayer);
        if(enemyInRoom.Length==0){
            isAllowToMove = true;
        }
        else{
            isAllowToMove = false;
        }
        for(int i=0;i<RoomManager.Instance.roomCount;i++){
            if(player.transform.position.x>=((roomLocation[i].x)-7.5)&&player.transform.position.x<=((roomLocation[i].x)+7.5)){
                if(player.transform.position.y>=((roomLocation[i].y)-3.5)&&player.transform.position.y<=((roomLocation[i].y)+3.5)){
                    activatedRoomLocation=roomLocation[i];
                    currentRoom = i;
                    break;
                }
            }
        }
    }
}
