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
    public float enemyDetectDistence;
    public float enemyAttackDistence;
    void FixedUpdate(){
        enemyInRoom = Physics2D.OverlapBoxAll(RoomManager.Instance.CurrentRoom.transform.position,new Vector2(15,7),0,whatIsLayer);
        if(enemyInRoom.Length==0){
            isAllowToMove = true;
        }
        else{
            isAllowToMove = false;
        }
    }
}
