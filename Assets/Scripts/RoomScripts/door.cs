using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject bottomDoor;
    GameObject player;
    public float a = 9.5f;
    public bool top, left, right, bottom;
    
    float detectionRange = 4f;
    GameObject Camera;
    private void Start()
    {
       
        Camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        CheckProximity();
        Move();
    }

    void CheckProximity()
    {
        if (player == null) return;

        float distanceToTopDoor = Vector2.Distance(player.transform.position, topDoor.transform.position);
        float distanceToLeftDoor = Vector2.Distance(player.transform.position, leftDoor.transform.position);
        float distanceToRightDoor = Vector2.Distance(player.transform.position, rightDoor.transform.position);
        float distanceToBottomDoor = Vector2.Distance(player.transform.position, bottomDoor.transform.position);

        top = distanceToTopDoor <= detectionRange && topDoor.activeSelf;
        left = distanceToLeftDoor <= detectionRange && leftDoor.activeSelf;
        right = distanceToRightDoor <= detectionRange && rightDoor.activeSelf;
        bottom = distanceToBottomDoor <= detectionRange && bottomDoor.activeSelf;
    }

    void Move()
    {
        if (GameManager.Instance.isAllowToMove)
        {
            if (top && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.up + new Vector2(0, a));
            }
            if (bottom && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.down + new Vector2(0, -a));
            }
            if (left && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.left + new Vector2(-a, 0));
            }
            if (right && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.right + new Vector2(a, 0));
            }

        }
    }
}
