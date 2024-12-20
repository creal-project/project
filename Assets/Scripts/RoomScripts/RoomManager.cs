using System.Collections.Generic;
using UnityEngine;

public class RoomManager : SIngleTon<RoomManager>
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] public int maxRooms = 15;
    [SerializeField] public int minRooms = 10;
    [SerializeField] private GameObject Stair;
    public int roomGenerateCount = 1;

    // Augmenter la taille des cases
    int roomWidth = 40;  // Anciennement 20
    int roomHeight = 24;  // Anciennement 12
    GameObject player;
    int gridSizeX = 10;
    int gridSizeY = 10;
    public GameObject CurrentRoom;
    public Vector2 stairPosition;
    public int stairIndex;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    public int roomCount;
    int roomChanged = 1;

    public int Rand;

    public bool generationComplete = false;
    public Vector2Int InitialRoomIndex;

    private void Start()
    {
        Start1();
    }
    public void Start1()
    {
        player = GameObject.Find("Player");
        Rand = Random.Range(minRooms,minRooms);
        Debug.Log(Rand);
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        InitialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(InitialRoomIndex);
    }

    public void Update()
    {

        HandleRoomGeneration();
        if(generationComplete){
            CurRoom();
        }
    }
    public void HandleRoomGeneration()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("roomCount was less than the minimum amount of rooms. trying again");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created");
            if(roomGenerateCount != roomChanged){
                GameManager.Instance.roomLocation.Clear();
                Spawner.Instance.spawnPos.Clear();
                for (int i = 0; i < roomCount; i++)
                {
                    GameManager.Instance.roomLocation.Add(new Vector2(roomObjects[i].transform.position.x, roomObjects[i].transform.position.y));
                    Spawner.Instance.spawnPos.Add(new Vector2(roomObjects[i].transform.position.x, roomObjects[i].transform.position.y));
                }
                Spawner.Instance.RandSpawn();
                roomChanged++;
            }
            else{
                for (int i = 0; i < roomCount; i++)
                {
                    GameManager.Instance.roomLocation.Add(new Vector2(roomObjects[i].transform.position.x, roomObjects[i].transform.position.y));
                    Spawner.Instance.spawnPos.Add(new Vector2(roomObjects[i].transform.position.x, roomObjects[i].transform.position.y));
                }
                Spawner.Instance.RandSpawn();
            }
            generationComplete = true;
        }
    }

    public void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.tag = $"{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;
        if (roomGrid[x, y] != 0)
            return false;
        if (roomCount >= maxRooms)
            return false;
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        newRoom.tag = $"{roomCount}";

        roomObjects.Add(newRoom);
        if (roomCount == Rand && IsThereStair())
        {
            GameObject myInstance = Instantiate(Stair, newRoom.transform.position, Quaternion.identity);
            //tag=$"{roomCount}"
            stairPosition = newRoom.transform.position;
            stairIndex = roomCount;
        }
        OpenDoors(newRoom, x, y);

        return true;
    }
    public void CurRoom()
    {
        if (CurrentRoom == null )
        {
            float minDistance = float.MaxValue;
            foreach (var room in roomObjects)
            {
                float distance = Vector2.Distance(player.transform.position, room.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    CurrentRoom = room;
                }
            }
        }

        
        foreach (var room in roomObjects)
        {
            float distanceToCurrentRoom = Vector2.Distance(CurrentRoom.transform.position, player.transform.position);
            float distanceToNewRoom = Vector2.Distance(room.transform.position, player.transform.position);

            if (distanceToNewRoom < distanceToCurrentRoom)
            {
                CurrentRoom = room;
            }
        }
    }


    public void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        GameObject.Find("Main Camera").transform.position= new Vector3(0, 0, 0);
        
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        Rand = Random.Range(minRooms, maxRooms);
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }
    private bool IsThereStair()
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Stair");
        if (arr.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
