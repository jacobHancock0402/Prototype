using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
    public float gridHeight;
    public float gridWidth;
    public int numNodesX;
    public int numNodesY;
    public float nodeRadius;
    public float nodeDiameter;
    public Renderer renderer;
    public Node[,] grid;
    public Transform Player;
    public const int diag_Cost = 14;
    public const int straight_Cost = 10;
    public List<Node> path;
    public Node firstNode;
    public List<Node> NeighbourLs;
    public int numWalkables = 0;
    public Node pathNode;
    public int pathCounter;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        //Debug.LogError("***REMOVED***me");
        nodeRadius = 0.2f;
        nodeDiameter = nodeRadius * 2;
        gridHeight = renderer.bounds.size.y; 
        gridWidth = renderer.bounds.size.x;
        numNodesX = Mathf.RoundToInt(gridWidth / nodeDiameter);
        numNodesY = Mathf.RoundToInt(gridHeight / nodeDiameter);
        grid = new Node[numNodesX, numNodesY];
        for(int x=0;x<numNodesX;x++)
        {
            for(int y=0;y<numNodesY;y++)
            {
                // shouldn't this be node diameter?this why have to *2 later
                Vector3 position = new Vector3(renderer.bounds.min.x + (nodeDiameter * x),renderer.bounds.min.y + (nodeDiameter * y),0);
                Collider2D walkable = Physics2D.OverlapCircle(position, nodeRadius);
                bool walkable2 = Physics2D.OverlapCircle(position, nodeRadius);      
                grid[x,y] = new Node();
                grid[x,y].Solid = walkable2;
                grid[x,y].position = position;
                grid[x,y].x = x;
                grid[x,y].y = y;
                if(walkable)
                {
                    grid[x,y].Object = walkable.gameObject;
                }
                //Debug.LogError("die");
                // should prob increase node size
            }
        }
        for(int x1=1;x1<numNodesX - 1;x1++)
        {
            for(int y1=1;y1<numNodesY - 1;y1++)
            {
                // shouldn't this be node diameter?this why have to *2 later
                int yminus1 = y1-1;
                int yplus1 = y1+1;
                int xplus1 = x1+1;
                int xminus1 = x1-1;
                // this ***REMOVED*** weird man, no work for - 1
                if(!grid[x1,y1].Solid)
                {
                    if(grid[x1,yplus1].Solid || grid[x1,yminus1].Solid || grid[xplus1,y1].Solid || grid[xminus1,y1].Solid)
                    {
                        if(grid[xplus1,y1].Solid || grid[xminus1,y1].Solid || grid[x1,yplus1].Solid)
                        {
                            grid[x1,y1].ClimbAble = true;
                        }
                        else
                        {
                            grid[x1,y1].ClimbAble = false;
                        }
                        if (grid[xplus1,y1].Solid)
                        {
                            if(grid[xplus1,y1].Object.layer == 11)
                            {
                                grid[x1,y1].Incline = true;
                            }
                        }
                        if(grid[x1,yminus1].Solid)
                        {
                            if(grid[x1,yminus1].Object.layer == 11)
                            {
                                grid[x1,y1].Incline = true;
                            }
                        }
                        if(grid[xminus1,y1].Solid)
                        {
                            if(grid[xminus1,y1].Object.layer == 11)
                            {
                                grid[x1,y1].Incline = true;
                            }
                        }
                        GameObject initObject = null;
                        LayerMask mask = LayerMask.GetMask("World");
                        Vector2 origin = new Vector2(grid[x1,y1].position.x, grid[x1,y1].position.y);
                        Vector2 di = ((new Vector2(grid[x1,y1].position.x, grid[x1,y1].position.y + 10)) - origin).normalized;
                        RaycastHit2D hit = Physics2D.Raycast(origin,di,1f,mask);

                        if(hit.collider == null)
                        {
                            grid[x1,y1].WalkAble = true;
                            numWalkables++;
                        }
                        else
                        {
                            grid[x1,y1].WalkAble = false;
                        }
                    }
                }
                else
                {
                    grid[x1,y1].WalkAble = false;
                    grid[x1,y1].ClimbAble = false;
                }
                //Debug.LogError("die");
                // should prob increase node size
            }
        }
        
        firstNode = grid[0,0];

    }
    public Node GetCurrentNode(Vector3 ObjectPos) {
        float xInGrid = ObjectPos.x - renderer.bounds.min.x;
        float yInGrid = ObjectPos.y - renderer.bounds.min.y;
        int NodesInX = Mathf.Min(Mathf.RoundToInt(xInGrid / nodeDiameter), numNodesX - 1);
        int NodesInY = Mathf.Min(Mathf.RoundToInt(yInGrid / nodeDiameter), numNodesY - 1);
        if(!grid[NodesInX, NodesInY].WalkAble)
        {
            if(grid[NodesInX, Mathf.Min(NodesInY + 1, numNodesY - 1)].WalkAble)
            {
                return grid[NodesInX, Mathf.Min(NodesInY + 1, numNodesY)];
            }
            if(grid[Mathf.Min(NodesInX + 1, numNodesX), NodesInY - 1].WalkAble)
            {
                return grid[Mathf.Min(NodesInY + 1, numNodesY - 1), NodesInY];
            }
            if(grid[Mathf.Max(NodesInX - 1, 0), NodesInY].WalkAble)
            {
                return grid[Mathf.Max(NodesInX - 1, 0), NodesInY];
            }
        }
        //Debug.LogError(NodesInX);
        return grid[NodesInX, NodesInY];
    }
    public List<Node> RandomPath(Node startNode)
    {
        if(numWalkables > 0)
        {   
            int x = UnityEngine.Random.Range(0, numNodesX - 1);
            int y = startNode.y;
            return PathFind(startNode, grid[x,y]);
        }
        return null;
    }
    public List<Node> PathFind(Node startNode , Node endNode) {
        List<Node> openList = new List<Node> {startNode};
        List<Node> closedList = new List<Node>();
        NeighbourLs = new List<Node>();
        if (endNode.WalkAble)
        {

           foreach(Node node in grid)
           {
                node.gCost = int.MaxValue;
                node.hCost = 0;
                node.CalcFCost();
                node.Parent = null;
                node.onPath = false;
            }

            startNode.gCost = 0;
            startNode.hCost = CalcHCost(startNode, endNode);
            startNode.CalcFCost();
            //Node currentNode = startNode;
            while(openList.Count > 0)
            {
                Debug.LogError("hi");
                //Debug.LogError(endNode.x);
                //Node lowestFCostNode = openList[0];
                //for(int i=1;i<openList.Count;i++)
               // {
                    //if(openList[i].fCost < lowestFCostNode.fCost || openList[i].fCost == lowestFCostNode.fCost && openList[i].hCost == lowestFCostNode.hCost)
                   // {
                        //lowestFCostNode = openList[i];
                    //}
                //}
                Node currentNode = openList[0];
                // currentNode.fCost = int.MaxValue;
                // for(int i=0;i<openList.Count;i++)
                // {
                //     if(openList[i].fCost < currentNode.fCost)
                //     {
                //         currentNode = openList[i];
                //     }
                // }
                //Debug.LogError(currentNode.x);
                if(currentNode.x == endNode.x && currentNode.y == endNode.y)
                {
                    Debug.LogError("madeit");
                    //Debug.LogError(currentNode.Parent.x);
                    return GetPath(startNode, currentNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                //Debug.LogError(closedList[1].x);
                List<Node> Neighbours = GetNeighbours(currentNode);
                foreach(Node Neighbour in Neighbours)
                {
                    if(!compareNodeToList(Neighbour, closedList) && ((Neighbour.WalkAble && currentNode.WalkAble && !Neighbour.ClimbAble) || (currentNode.ClimbAble && Neighbour.ClimbAble && currentNode.y <= Neighbour.y) ||(currentNode.ClimbAble && (Neighbour.WalkAble && !Neighbour.ClimbAble)) || ((currentNode.WalkAble && !currentNode.ClimbAble) && Neighbour.ClimbAble)  || (!Neighbour.Solid && Neighbour.y < currentNode.y && Neighbour.x == currentNode.x)))
                    {
                        // ***REMOVED*** this ***REMOVED*** ***REMOVED***
                        // don't think something to do with neighbours function as never empty
                        // is it on comparison on closed and open list?, what else could it be the ***REMOVED***?
                        // but then how does it get through when g cost comp removed?
                        // when this is removed, it starts checking neighbours of neightbours, but here
                        // neighbours seem to dissapear even though all in openList
                        int gCost = currentNode.gCost + CalcHCost(currentNode, Neighbour);

                        if(gCost < Neighbour.gCost || !compareNodeToList(Neighbour, openList))
                        {   
                            Neighbour.Parent = currentNode;
                            //Debug.LogError(Neighbour.)
                            Neighbour.gCost = gCost;
                            Neighbour.hCost = CalcHCost(Neighbour, endNode);
                            Neighbour.CalcFCost();
                        
                            if(!compareNodeToList(Neighbour, openList))
                            {
                                openList.Add(Neighbour);
                            //Debug.LogError(Neighbour.x);
                            }
                        }
                        
                    }
                    NeighbourLs.Add(Neighbour);
                }
                if(openList.Count == 0)
                {
                    Debug.LogError(currentNode.x);
                    Debug.LogError(currentNode.y);
                    Debug.LogError("didnt");
                    //return null;
                }

            }
        }
        else
        {
            Debug.LogError("can't walk");
            return null;
        }
        return null;
        //return null;




    }
    public bool compareNodeToList(Node node, List<Node> nodes)
    {
        foreach(Node n in nodes)
        {
            if(node.x == n.x && node.y == n.y )
            {
                return true;
            }
        }
        return false;
    }
    public int CalcHCost(Node a, Node b)
    {
        int xDistanceDiag = Mathf.Abs(a.x - b.x);
        int yDistanceDiag = Mathf.Abs(a.y - b.y);
        int remainingStraight = Mathf.Abs(xDistanceDiag - yDistanceDiag);
        return ((diag_Cost * Mathf.Min(xDistanceDiag, yDistanceDiag)) + (straight_Cost * remainingStraight));

    }

    public List<Node> GetPath(Node startNode, Node endNode) {
        List<Node> pat = new List<Node>();
        Node currentNode = endNode;
        pat.Add(currentNode);
        int i = 0;
        while(i < 50000)
        {
            currentNode = currentNode.Parent;
            pat.Add(currentNode);
            //Debug.LogError("cur" + currentNode.x);
            //Debug.LogError("start" + startNode.x);

            if(currentNode.x == startNode.x && currentNode.y == startNode.y)
            {
                //Debug.Log(i);
                break;
            }
            i++;
            //Debug.LogError(i);
            //Debug.LogError(currentNode.x);
        }
        pat.Reverse();
        path = pat;
        return pat;
    }

    public List<Node> GetNeighbours(Node currentNode) {
        List<Node> NeighbourList = new List<Node>();
        int x = currentNode.x;
        int y = currentNode.y;
        if((x - 1) >= 0)
        {
           // if(grid[x - 1,y].WalkAble)
           // {
            NeighbourList.Add(grid[x - 1,y]);
            //}
            if((y - 1) >= 0)
            {
               // if(grid[x - 1, y - 1].WalkAble)
               // {
                    NeighbourList.Add(grid[x - 1,y - 1]); 
                //}
            }
            if((y + 1) <= (numNodesY - 1))
            {
               // if(grid[x - 1, y + 1].WalkAble)
                //{
                    NeighbourList.Add(grid[x - 1,y + 1]) ;
                //}
            }
        }
        else
        {
            Debug.LogError("x-1:" + x +"," + y);
        }
        if((x + 1) <= (numNodesX - 1))
        {
            //if(grid[x + 1,y].WalkAble)
            //{
                NeighbourList.Add(grid[x + 1,y]);
           // }
            if((y - 1) >= 0)
            {
               // if(grid[x + 1,y - 1].WalkAble)
               // {
                    NeighbourList.Add(grid[x + 1,y - 1]); 
               // }
            }
            if((y + 1) <= (numNodesY - 1))
            {
               // if(grid[x + 1,y + 1].WalkAble)
               // {
                    NeighbourList.Add(grid[x + 1,y + 1]) ;
               // }
            }
        }
        else
        {
            Debug.LogError("x+1:" + x +"," + y);
        }
        if((y - 1) >= 0)
        {
           // if(grid[x,y - 1].WalkAble)
           // {
                NeighbourList.Add(grid[x,y - 1]) ;
           // }
        }
        else
        {
            Debug.LogError("y-1:" + x +"," + y);
        }
        if((y + 1) <= (numNodesY - 1))
        {
            //if(grid[x,y + 1].WalkAble)
            //{
                NeighbourList.Add(grid[x,y + 1]) ;
            //}
        }
        else
        {
            Debug.LogError("y+1:" + x +"," + y);
        }
        return NeighbourList;

    }
    void Update()
    {
    }
//    
void OnDrawGizmos()
     {
//         //Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, 1, gridSizeY))
//         {
//             if(grid != null)
//             {
//         Node PlayerNode = GetCurrentNode(Player.position);
//             foreach(Node n in grid)
//              {
                  if(path != null)
        {
           foreach(Node node in path)
          {
              Gizmos.color = Color.cyan;
               Gizmos.DrawCube(node.position, Vector3.one * (nodeRadius * 2));
            }
        }
//                if(n.x == PlayerNode.x && n.y == PlayerNode.y)
//                 {
//                     //Gizmos.color = Color.cyan;
//                 }
//                //  if(//compareNodeToList(n, NeighbourLs))
//                // {
//                //      Gizmos.color = Color.yellow;
//                //  }
//                else if(n.WalkAble && !n.onPath && n.Incline )
//                {
//                     Gizmos.color = Color.green;
//                }
//                 else if(!n.onPath)
//                {
//                     Gizmos.color = Color.red;
//                }
//                Gizmos.DrawSphere(n.position, nodeRadius);
//                  Gizmos.DrawCube(n.position, Vector3.one * (nodeRadius * 2));
//             }
//            }
//                 //Debug.LogError(n.WalkAble);
//             }
//        //  if(path != null)
//        // {
//        //    // foreach(Node n in path)
//        //   // {
//        //         //Gizmos.color = Color.cyan;
//        //         //Gizmos.DrawCube(n.position, Vector3.one * (nodeRadius * 2));
//        //      //}
//        //  }
}
}
public class Node : MonoBehaviour{
    public bool WalkAble;
    public bool ClimbAble;
    public bool Solid;
    public Vector3 position;
    public int x;
    public int y;
    public int fCost;
    public int hCost;
    public int gCost;
    public Node Parent;
    public bool onPath;
    public bool Incline;
    public GameObject Object;

    public void CalcFCost()
    {
        fCost = hCost + gCost;
    }
}
