using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class PlayerFollow : MonoBehaviour
{
    private GameObject player;
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public List<Vector2> movePath = new List<Vector2>();
    public bool allowDiagonal, dontCrossCorner;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    public Vector2 dest;
    public bool isMoving = false;
    public bool isPathFinding = false;

    public int i = 0;
    private int randPosX;
    private int randPosY;
    public float speed;
    public bool isArrived = false;
    public int attackDamage = 2;
    public int currentEnemyRoom = -1;
    bool isroomFound = false;
    bool isDestSet = false;

    float playerDistence;
    float time;
    private Color enemyColor;
    private float cooltime =2f;
    private float currenttime =0;
    private void Start(){
        player = GameObject.Find("Player");
        //enemyColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        currenttime += Time.deltaTime;
        if(!isroomFound&&RoomManager.Instance.generationComplete){
            FindEnemyRoom();
            topRight = new Vector2Int((int)(GameManager.Instance.roomLocation[currentEnemyRoom].x+16),(int)(GameManager.Instance.roomLocation[currentEnemyRoom].y+8));
            bottomLeft = new Vector2Int((int)(GameManager.Instance.roomLocation[currentEnemyRoom].x-16),(int)(GameManager.Instance.roomLocation[currentEnemyRoom].y-8));
            isroomFound = true;
            if(isDestSet == false){
                dest = new Vector2(GameManager.Instance.roomLocation[currentEnemyRoom].x+Random.Range(-15,16),GameManager.Instance.roomLocation[currentEnemyRoom].y+Random.Range(-7,8));
                isDestSet = true;
            }
        }
        if(GameManager.Instance.currentPlayerRoom == currentEnemyRoom){
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
            enemyColor.a = 1f;
            startPos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
            targetPos = new Vector2Int(Mathf.RoundToInt(dest.x), Mathf.RoundToInt(dest.y));
            playerDistence = Vector2.Distance(GameObject.FindWithTag("Player").transform.position,this.transform.position);
            if(playerDistence <= GameManager.Instance.enemyDetectDistence){
                dest = player.transform.position;
            }
            else{
                if(isArrived){
                    time+=Time.deltaTime;
                    if(time>=Random.Range(0.5f,2f)){
                        RandPosition();
                        time = 0;
                    }
                }
            }
            if(isDestSet){
                Follow();
            }
            if(playerDistence <= GameManager.Instance.enemyAttackDistence){
                
                if(cooltime<currenttime){
                    player.GetComponent<Player>().TakeDamage(attackDamage);
                    currenttime = 0f;
                }
            }
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(56,56,56);
            //enemyColor.a = 150f;
        }
        
    }
    private void Follow()
    {
        StopAllCoroutines();
        isPathFinding = false;
        i = 0;
        FinalNodeList = null;
        PathFinding();
        StartCoroutine(PlayerMove(FinalNodeList));
    }
    List<Vector2> GetRouteList(List<Node> oldList)
    {
        List<Vector2> newList = new List<Vector2>();

        foreach (var node in oldList)
        {
            newList.Add(new Vector2(node.x, node.y));
        }
        newList.RemoveAt(0);
        return newList;
    }
    void FindEnemyRoom(){
        for(int i=0;i<RoomManager.Instance.roomCount;i++){
            if(this.transform.position.x>=((GameManager.Instance.roomLocation[i].x)-15)&&this.transform.position.x<=((GameManager.Instance.roomLocation[i].x)+15)){
                if(this.transform.position.y>=((GameManager.Instance.roomLocation[i].y)-7)&&this.transform.position.y<=((GameManager.Instance.roomLocation[i].y)+7)){
                    currentEnemyRoom = i;
                    return;
                }
            }
        }
    }

    IEnumerator PlayerMove(List<Node> optimizedPath)
    {
        if (optimizedPath.Count <= 1){
            isArrived = true;
            yield break;
        }
        else{
            isArrived = false;
        }
        var routeList = GetRouteList(optimizedPath);
        int currentRoute = 0;
        Vector2 enemyPosition = transform.position;
        while (currentRoute < routeList.Count)
        {
            Vector2 moveVector = enemyPosition - routeList[currentRoute];
            if (((Vector2)transform.position - routeList[currentRoute]).magnitude <= 0.1f)
            {
                currentRoute++;
                enemyPosition = transform.position;
            }
            else{
                transform.position -= (Vector3)(moveVector * speed * Time.deltaTime);
            }
            yield return null;
        }
    }
    private void RandPosition(){
        randPosX = Random.Range(-15,16);
        randPosY = Random.Range(-7,8);
        dest = new Vector2(GameManager.Instance.roomLocation[currentEnemyRoom].x+randPosX,GameManager.Instance.roomLocation[currentEnemyRoom].y+randPosY);
    }

    public void PathFinding()
    {
        isPathFinding = true;
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;
                }
                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
        StartNode = NodeArray[Mathf.Abs(startPos.x - bottomLeft.x), Mathf.Abs(startPos.y - bottomLeft.y)];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0 && isPathFinding)
        {
            CurNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++)
                if (OpenList[i].F < CurNode.F || (OpenList[i].F == CurNode.F && OpenList[i].H < CurNode.H))
                {
                    CurNode = OpenList[i];
                }
            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();
            }

            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        i = 0;
    }

    void OpenListAdd(int checkX, int checkY)
    {
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }
    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}