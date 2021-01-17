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
    void Start()
    {
        renderer = GetComponent<Renderer>();
        //Debug.LogError("***REMOVED***me");
        nodeRadius = 1f;
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
                bool walkable = !Physics2D.OverlapCircle(position, nodeRadius);        
                grid[x,y] = new Node();
                grid[x,y].WalkAble = walkable;
                grid[x,y].position = position;
                grid[x,y].x = x;
                grid[x,y].y = y;
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
        int NodesInY = Mathf.Min(Mathf.RoundToInt(yInGrid / nodeDiameter), numNodesX - 1);
        //Debug.LogError(NodesInX);
        return grid[NodesInX, NodesInY];
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
                if(Neighbours.Count == 0)
                {
                    Debug.LogError("yessirrr:" + currentNode.x);
                }
                foreach(Node Neighbour in Neighbours)
                {
                    if(!compareNodeToList(Neighbour, closedList) && Neighbour.WalkAble)
                    {
                        // ***REMOVED*** this ***REMOVED*** ***REMOVED***
                        // don't think something to do with neighbours function as never empty
                        // is it on comparison on closed and open list?, what else could it be the ***REMOVED***?
                        // but then how does it get through when g cost comp removed?
                        // when this is removed, it starts checking neighbours of neightbours, but here
                        // neighbours seem to dissapear even though all in openList
                        //Debug.LogError("big");
                        int gCost = currentNode.gCost + CalcHCost(currentNode, Neighbour);

                        if(gCost < Neighbour.gCost || !compareNodeToList(Neighbour, openList) )
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
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(currentNode);
        int i = 0;
        //Debug.LogError(currentNode.x);

        while(i < 50000)
        {
            currentNode = currentNode.Parent;
            path.Add(currentNode);
            if(currentNode.x == startNode.x && currentNode.y == startNode.y)
            {
                //Debug.Log(i);
                break;
            }
            i++;
            //Debug.LogError(i);
            //Debug.LogError(currentNode.x);
        }
        path.Reverse();
        return path;
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
        if(Input.GetMouseButtonDown(0))
        {
            firstNode = GetCurrentNode((Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            //Debug.LogError("x:" + firstNode.x);
            //Debug.LogError("y:" + firstNode.y);
        }
        if(Input.GetMouseButtonDown(1))
        {
            path = PathFind(firstNode, GetCurrentNode((Camera.main.ScreenToWorldPoint(Input.mousePosition))));
            //Debug.LogError(path.Count);
        }
    }
    void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, 1, gridSizeY))
        // {
            if(grid != null)
            {
        //     Node PlayerNode = GetCurrentNode(Player.position);
            foreach(Node n in grid)
             {
                 if(path != null)
        {
           foreach(Node node in path)
          {
               Gizmos.color = Color.cyan;
               Gizmos.DrawCube(node.position, Vector3.one * (nodeRadius * 2));
            }
        }
      //         if(n.x == PlayerNode.x && n.y == PlayerNode.y)
        //         {
        //             Gizmos.color = Color.cyan;
        //         }
                if(compareNodeToList(n, NeighbourLs))
                {
                    Gizmos.color = Color.yellow;
                }
                else if(n.WalkAble && !n.onPath )
                {
           Gizmos.color = Color.green;
         }
                 else if(!n.onPath)
           {
                     Gizmos.color = Color.red;
           }
        //        // Gizmos.DrawSphere(n.position, nodeRadius);
                 Gizmos.DrawCube(n.position, Vector3.one * (nodeRadius * 2));
             }
            }
        //         //Debug.LogError(n.WalkAble);
        //     }
        if(path != null)
       {
           foreach(Node n in path)
          {
               Gizmos.color = Color.cyan;
               Gizmos.DrawCube(n.position, Vector3.one * (nodeRadius * 2));
            }
        }
    }
}
public class Node : MonoBehaviour{
    public bool WalkAble;
    public Vector3 position;
    public int x;
    public int y;
    public int fCost;
    public int hCost;
    public int gCost;
    public Node Parent;
    public bool onPath;

    public void CalcFCost()
    {
        fCost = hCost + gCost;
    }
}
