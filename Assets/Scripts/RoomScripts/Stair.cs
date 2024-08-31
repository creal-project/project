using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Stair : MonoBehaviour
{
    GameObject Player;
    float Range = 3f;
    bool IsIn = false;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    private void Update()
    {
        InRange();
        InterAct();
    }
    void InRange()
    {
        float dis = Vector2.Distance(Player.transform.position,transform.position);
        if(dis < Range)
        {
            IsIn = true;
        }
        else {  IsIn = false; }
    }
    void InterAct()
    {
        if (IsIn && Input.GetKeyDown(KeyCode.E) && GameManager.Instance.IsThereEnemy == true)
        {
            //ï¿½ï¿½ï¿½Îµï¿½
            Debug.Log("¾À ÀÌµ¿");
            //RoomManager.Instance.RegenerateRooms();
        }
    }
}
