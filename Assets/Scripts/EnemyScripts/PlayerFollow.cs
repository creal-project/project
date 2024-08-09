using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class PlayerFollow : MonoBehaviour
{
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
    public float speed;
    private void Update()
    {
        dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
        targetPos = new Vector2Int(Mathf.RoundToInt(dest.x), Mathf.RoundToInt(dest.y));
        if (!isMoving
            && Mathf.Round(this.transform.position.x) == Mathf.RoundToInt(dest.x)
            && Mathf.Round(this.transform.position.y) == Mathf.RoundToInt(dest.y)
        )
        if (Input.GetMouseButtonDown(1));
        {
            StopAllCoroutines();
            isPathFinding = false;
            i = 0;
            FinalNodeList = null;
            PathFinding();
            StartCoroutine(PlayerMove(FinalNodeList));
        }
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

    IEnumerator PlayerMove(List<Node> optimizedPath)
    {

        if (optimizedPath.Count <= 1) yield break;
        var routeList = GetRouteList(optimizedPath);
        int currentRoute = 0;
        Vector2 enemyPosition = transform.position;
        while (currentRoute < routeList.Count)
        {
            print(currentRoute);
            Vector2 moveVector = enemyPosition - routeList[currentRoute];

            float rotation = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;

            if (rotation > 360) rotation -= 360;
            else if (rotation < 0) rotation += 360;

            switch (rotation / 45)
            {
                case 0: print("a"); break;
                case 1: print("b"); break;
                case 2: print("c"); break;
                case 3: print("d"); break;
                case 4: print("e"); break;
                case 5: print("f"); break;
                case 6: print("g"); break;
                case 7: print("h"); break;
            }

            if (((Vector2)transform.position - routeList[currentRoute]).magnitude <= 0.1f)
            {
                currentRoute++;
                enemyPosition = transform.position;
            }
            else transform.position -= (Vector3)(moveVector * speed * Time.deltaTime);

            yield return null;
        }
    }

    public void PathFinding()
    {
        isPathFinding = true;

        // NodeArray의 크기 정해주고, isWall, x, y 대입
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

        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[Mathf.Abs(startPos.x - bottomLeft.x), Mathf.Abs(startPos.y - bottomLeft.y)];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0 && isPathFinding)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++)
                if (OpenList[i].F < CurNode.F || (OpenList[i].F == CurNode.F && OpenList[i].H < CurNode.H))
                {
                    CurNode = OpenList[i];
                }
            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 마지막
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

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
            }


            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        i = 0;
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
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