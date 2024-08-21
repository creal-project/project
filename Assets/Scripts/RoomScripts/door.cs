using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject bottomDoor;
    GameObject player;

    public bool top, left, right, bottom;

    float detectionRange = 2.5f;

    private void Start()
    {
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

                player.transform.Translate(Vector2.up * 7f);
            }
            if (bottom && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.down * 7f);
            }
            if (left && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.left * 7f);
            }
            if (right && Input.GetKeyDown(KeyCode.E))
            {
                player.transform.Translate(Vector2.right * 7f);
            }
        }
    }
}
