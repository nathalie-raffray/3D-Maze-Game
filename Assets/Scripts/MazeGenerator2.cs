using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator2 : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject treeWallPrefab;
    public GameObject pathTilePrefab;

    public float xPos;
    public float yPos;
    public float zPos;

    // private bool[8][7] horizWall;
    //private bool[7][8] vertWall;

    private MazeNode[,] graph = new MazeNode[8, 8];
    private List<MazeNode> frontierNeighbours = new List<MazeNode>();
    private MazeNode current;
    private MazeNode exit;

    private Stack<MazeNode> myStack = new Stack<MazeNode>();
    private List<MazeNode> shortestPath = new List<MazeNode>();
    private bool exitFound = false;

   // private MazeNode[,,] fifteenGraphs = new MazeNode[8, 8, 15];
    private MazeNode[][,] fifteenGraphs = new MazeNode[15][, ];
    //double[][] x = new double[5][];

    private GameObject player;


    public class MazeNode
    {

        public List<MazeNode> inNeighbours = new List<MazeNode>();

        //the nodes that this mazenode is connected to in the MST
        public List<MazeNode> neighbourNodes = new List<MazeNode>();

        public int indexX;
        public int indexY;

        public bool visited;

        //Is it inside the MST already?
        public bool mstIn;

        public MazeNode(int x, int y)
        {
            visited = false;
            mstIn = false;

            this.indexX = x;
            this.indexY = y;
        }

        public string toString()
        {
            string s = "IndexX: " + indexX + ", IndexY: " + indexY;
            return s;
        }

    }

    private void unVisit(MazeNode[,] g)
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                g[i, j].visited = false;
            }
        }
    }

    private MazeNode getRandomExit()
    {
        //choosing random indices to get the exit mazenode. 
        int randomX = 7;
        int randomY = 4;
        while (randomX == 7 && randomY == 4)
        { //we don't want the exit to be the same as the root. 
            randomX = Random.Range(0, 8);
            randomY = Random.Range(0, 8);
        }
        return graph[randomX, randomY];
    }


    private void Prim(MazeNode[,] g, int step)
    {

        //initialize the nodes
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                g[i, j] = new MazeNode(i, j);

            }
        }

        //the two nodes that are connected from the start
        MazeNode n1;
        MazeNode n2;

        if (step == 0) //haven't actually created our path yet. 
        {
            //this is our root
            current = g[7, 4];
            current.mstIn = true;
            //current.inNeighbours.Clear();
        }
        //else if(step == 15) //last exit
        //{
        //    current = shortestPath[step];
        //    current = g[current.indexX, current.indexY];
        //    current.mstIn = true;
        //}
        else
        {
            if( (step+1) > shortestPath.Count-1) //this is for if the path from start to exit is of length < 16 (say it is of x length),
                                                    //but the player is taking more than x time to complete the maze.
                                                    //in this case, we still want the path from the (exit-1) tile to the exit tile to be traversable.
            {
                n1 = shortestPath[shortestPath.Count-2];
                n2 = shortestPath[shortestPath.Count-1];
            }
            else
            {
                n1 = shortestPath[step];
                n2 = shortestPath[step + 1];
            }



            n1 = g[n1.indexX, n1.indexY];
            n2 = g[n2.indexX, n2.indexY];

            n1.neighbourNodes.Add(n2);
            n2.neighbourNodes.Add(n1);

            n1.mstIn = true;
            //n2.mstIn = true;

            addFrontierAndInNeighbours(n1);
            current = n2;

        }

        var x = 0;
        var y = 0;
        MazeNode connectingVertex;

        do
        {
            addFrontierAndInNeighbours(current);

            int random = Random.Range(0, frontierNeighbours.Count); //will get a random result from 0 to frontierNeighbours.Count
            current = frontierNeighbours[random];
            frontierNeighbours.Remove(current); //remove current from frontierNeighbours


            random = Random.Range(0, current.inNeighbours.Count);
            connectingVertex = current.inNeighbours[random];
            current.neighbourNodes.Add(connectingVertex);   //connect the new node to one of its random inNeighbors
            connectingVertex.neighbourNodes.Add(current);   //connect its in neighbor to itself

            current.mstIn = true;
            current.inNeighbours.Clear();

            //printFrontier();

        } while (frontierNeighbours.Count > 0);



        void addFrontierAndInNeighbours(MazeNode curr)
        {
            for (var i = 0; i < 2; i++) //add the current's neighbours not already in MST to frontier neighbours
            {
                for (var j = 0; j < 2; j++)
                {
                    if (i == 0 && j == 0 || i == 1 && j == 1) continue;
                    if (((x = curr.indexX + i) >= 0) && (curr.indexX + i < 8))
                    {
                        if (((y = curr.indexY + j) >= 0) && (curr.indexY + j < 8))
                        {
                            if (g[x, y].mstIn == false) //if not already in the MST
                            {
                                if (!(frontierNeighbours.Contains(g[x, y]))) //if not already part of frontierNeighbours
                                {
                                    frontierNeighbours.Add(g[x, y]);
                                }

                                g[x, y].inNeighbours.Add(curr);

                            }
                        }
                    }
                    if (((x = curr.indexX - i) >= 0) && (curr.indexX - i < 8))
                    {
                        if (((y = curr.indexY - j) >= 0) && (curr.indexY - j < 8))
                        {
                            if (g[x, y].mstIn == false) //if not already in the MST
                            {
                                if (!(frontierNeighbours.Contains(g[x, y]))) //if not already part of frontierNeighbours
                                {
                                    frontierNeighbours.Add(g[x, y]);
                                }

                                g[x, y].inNeighbours.Add(curr);


                            }
                        }
                    }
                }
            }

        }

    }

    private void PrintFrontier()
    {
        string s = "Frontier Neighbours: ";
        for (var i = 0; i < frontierNeighbours.Count; i++)
        {
            s += i + ": " + frontierNeighbours[i].toString() + ", ";
        }
        Debug.Log(s);
    }



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Capsule");
        Instantiate(pathTilePrefab, new Vector3(135, 0, 170), Quaternion.identity); //the first tile when you enter is colored. 

        for (var i = 0; i < 8; i++)
        { //make the 8x8 grid of tiles
            for (var j = 0; j < 8; j++)
            {
                //instantiate the tiles
                Instantiate(tilePrefab, new Vector3((xPos - 10 * i), yPos, (zPos + 10 * j)), Quaternion.identity);
            }
        }

        GameObject doorToMaze; //this is the tree wall which will disappear upon obtaining the key. 
        for (var i = 0; i < 8; i++) //make trees surround the whole maze
        {
            Instantiate(treeWallPrefab, new Vector3((xPos + 5), yPos, ((zPos - 5) + 10 * (i))), Quaternion.AngleAxis(90, Vector3.up)); //create a line of trees on left side of maze
            Instantiate(treeWallPrefab, new Vector3((xPos - 5 - 10 * 7), yPos, ((zPos - 5) + 10 * (i))), Quaternion.AngleAxis(90, Vector3.up)); //create a line of trees on right side of maze

            Instantiate(treeWallPrefab, new Vector3((xPos + 5 - 10 * (i)), yPos, (zPos - 5)), Quaternion.identity); //create a line of trees on north side of maze

            if (((xPos + 5 - 10 * i) == 140) && ((zPos - 5) + 10 * 8) == 175)
            {
                doorToMaze = Instantiate(treeWallPrefab, new Vector3((xPos + 5 - 10 * (i)), yPos, ((zPos - 5) + 10 * 8)), Quaternion.identity); //create a line of trees on south side of maze
                doorToMaze.gameObject.name = "entry";
            }
            else
            {
                Instantiate(treeWallPrefab, new Vector3((xPos + 5 - 10 * i), yPos, ((zPos - 5) + 10 * 8)), Quaternion.identity); //create a line of trees on south side of maze
            }
        }


        Prim(graph, 0);

        CreateMazeWalls(graph);

        myStack.Push(graph[4, 7]); //add the root
        var pathLength = 20; //arbitrary number. 

        while (pathLength > 17) //for up to 16 steps, then the path from entry to exit may consist of a maximum of 17 tiles. 
        {

            exit = getRandomExit();
            //exit = graph[6, 7];


            //FIND THE SHORTEST PATH TO THE EXIT USING DFS
            exitFound = false;
            myStack.Clear();
            shortestPath.Clear();
            unVisit(graph);
            myStack.Push(graph[4, 7]);
            graph[4, 7].visited = true;

            DFS(graph[4, 7]);

            Debug.Log("exit, indexX: " + exit.indexX + "indexY: " + exit.indexY);

            pathLength = shortestPath.Count;
        }

        //for (var i = 0; i < shortestPath.Count; i++)
        //{ //now draw tiles leading to the path
        //    Instantiate(pathTilePrefab, new Vector3((xPos - 10 * shortestPath[i].indexX), yPos, (zPos + 10 * shortestPath[i].indexY)), Quaternion.identity);
        //}

        for(var i = 0; i<15; i++)
        {
            //creates the remaining 15 pregenerated mazes 
            fifteenGraphs[i] = new MazeNode[8, 8];
            Prim(fifteenGraphs[i], i + 1);
        }

    }


    void DFS(MazeNode curr)
    {
        if (exitFound) return;
        for (var i = 0; i < curr.neighbourNodes.Count; i++)
        {

            if (exitFound) return;
            if (!(curr.neighbourNodes[i].visited)) //if not visited
            {
                curr.neighbourNodes[i].visited = true;

                if (curr.neighbourNodes[i] == exit) //base case
                {
                    myStack.Push(exit);
                    savePath();
                    exitFound = true;
                    return;
                }
                myStack.Push(curr.neighbourNodes[i]);
                DFS(curr.neighbourNodes[i]);
            }

        }
        if (exitFound) return;
        myStack.Pop();
    }

    void savePath()
    {
        Stack<MazeNode> myReverseStack = new Stack<MazeNode>();
        var length = myStack.Count;
        for (var i = 0; i < length; i++)
        {
            myReverseStack.Push(myStack.Pop());
        }
        for (var i = 0; i < length; i++)
        {
            shortestPath.Add(myReverseStack.Pop());
        }

    }
    //doorToMaze.gameObject.name = "entry";

    private void CreateMazeWalls(MazeNode[,] g)
    {
        GameObject mazeWall;
        //placing --HORIZONTAL MAZE WALLS-- putting walls on the TOP
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 7; j++)
            {
                if (!(g[i, j].neighbourNodes.Contains(g[i, j + 1])))
                {
                    mazeWall = Instantiate(treeWallPrefab, new Vector3(((xPos + 5) - 10 * (i)), yPos, ((zPos + 5) + 10 * (j))), Quaternion.identity);
                    mazeWall.gameObject.tag = "MazeWall";
                }
            }
        }



        //placing --VERTICAL MAZE WALLS---  putting walls on the LEFT (positive z pointing away from me)
        for (var i = 0; i < 7; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (!(g[i, j].neighbourNodes.Contains(g[i + 1, j])))
                {
                    mazeWall = Instantiate(treeWallPrefab, new Vector3(((xPos - 5) - 10 * (i)), yPos, ((zPos - 5) + 10 * (j))), Quaternion.AngleAxis(90, Vector3.up));
                    mazeWall.gameObject.tag = "MazeWall";
                }

            }
        }
    }

    private int[] playerTilePosition = { 4, 7 }; //start at root
    private float minLimitX = 130; //left
    private float maxLimitX = 140; //right
    private float minLimitZ = 95; //bottom
    private float maxLimitZ = 105; //top
    private int stepCount = 0;
    private bool stepTaken = false;
    private bool playerInsideMaze = false;
    private GameObject[] oldMazeWalls;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        playerInsideMaze = ((pos.x <= 174 && pos.x >= 96) && (pos.z <= 179 && pos.z >= 101));
        if (playerInsideMaze)
        {
            //checking to see when the player is traversing tiles
            if (pos.x <= minLimitX)
            {
                minLimitX -= 10;
                maxLimitX -= 10;
                stepCount++;
                stepTaken = true;
            }
            else if (pos.x >= maxLimitX)
            {
                minLimitX += 10;
                maxLimitX += 10;
                stepCount++;
                stepTaken = true;
            }
            else if (pos.x <= minLimitZ)
            {
                minLimitZ -= 10;
                maxLimitZ -= 10;
                stepCount++;
                stepTaken = true;
            }
            else if (pos.x >= maxLimitZ)
            {
                minLimitZ += 10;
                maxLimitZ += 10;
                stepCount++;
                stepTaken = true;
            }

            if (stepCount > 16)
            {
                //start maze over again
                //terminate maze
            }
            else if (stepTaken)
            {

                stepTaken = false;

                oldMazeWalls = GameObject.FindGameObjectsWithTag("MazeWall");

                int coloredTile; //this variable holds the index (inside shortest path) of the next tile in maze solution
                if (stepCount < shortestPath.Count)
                {
                    coloredTile = stepCount;
                }
                else
                {
                    coloredTile = shortestPath.Count - 1;
                }
                Instantiate(pathTilePrefab, new Vector3((xPos - 10 * shortestPath[coloredTile].indexX),
                yPos, (zPos + (10 * shortestPath[coloredTile].indexY))), Quaternion.identity); //create the next tile in maze solution (colored)

                for (var i = 0; i < oldMazeWalls.Length; i++)
                {
                    Destroy(oldMazeWalls[i]);
                }
                CreateMazeWalls(fifteenGraphs[stepCount - 1]);
            }



            //var gameObjects : GameObject[];
            //gameObjects = GameObject.FindGameObjectsWithTag("yourTag");

            //for (var i = 0; i < gameObjects.length; i++) { 
            //Destroy(gameObjects[i]);
            //}
        }

    }
}
