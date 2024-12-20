using UnityEngine;

public class Stair : MonoBehaviour
{
    GameObject Player;
    float Range = 3f;
    bool IsIn = false;
    bool SceneLoaded = false;

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
        if (Player != null)
        {
            float dis = Vector2.Distance(Player.transform.position, transform.position);
            IsIn = dis < Range;
        }
    }

    void InterAct()
    {
        if (IsIn && Input.GetKeyDown(KeyCode.E))
        {

            RoomManager.Instance.RegenerateRooms();
            if(RoomManager.Instance.maxRooms < 20)
            {
                RoomManager.Instance.maxRooms += 2;
                RoomManager.Instance.minRooms += 1;
            }
            GameObject[] items = GameObject.FindGameObjectsWithTag("item");
            for(int i = 0;i <items.Length;i++)
            {
                Destroy(items[i]);
            }
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
            for(int i =0;i<enemy.Length;i++)
            {
                Destroy(enemy[i]);
            }
            RoomManager.Instance.roomGenerateCount++;
            Player.transform.position = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }
}
