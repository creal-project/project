using UnityEngine.SceneManagement;
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
        if (IsIn && Input.GetKeyDown(KeyCode.E) && GameManager.Instance.IsThereEnemy)
        {
            RoomManager.Instance.RegenerateRooms(); // �� �����
            Player.transform.position = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }
}
