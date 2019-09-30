using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
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


    public class MazeNode{

        public List<MazeNode> inNeighbours = new List<MazeNode>();

        //the nodes that this mazenode is connected to in the MST
        public List<MazeNode> neighbourNodes = new List<MazeNode>();

        // private MazeNode[] inNeighbours;
        //private MazeNode[] realNeighbours;
        public int indexX;
        public int indexY;
        //private Vector2 coordinates;
        public bool visited;

        //Is it inside the MST already?
        public bool mstIn;

        public MazeNode(int x, int y){
            visited = false;
            mstIn = false;
            //this.coordinates.x = coordinates.x;
            //this.coordinates.y = coordinates.y;
            this.indexX = x;
            this.indexY = y;
        }

        public string toString(){
            string s = "IndexX: " + indexX + ", IndexY: " + indexY;
            return s;
        }

    }

    private void Prim(){

        //initialize the nodes
        for (var i = 0; i < 8; i++){
            for (var j = 0; j < 8; j++){
                graph[i,j] = new MazeNode(i, j);
            }
        }

        //private MazeNode root = new MazeNode(new Vector2(0, 4));
        //graph[0, 4] = root;

        //choosing random indices to get the exit mazenode. 
        int randomX = 7;
        int randomY = 4;
        while(randomX == 7 && randomY == 4){ //we don't want the exit to be the same as the root. 
            randomX = Random.Range(0, 8);
            randomY = Random.Range(0, 8);
        }
        exit = graph[randomX, randomY];


        //this is our root
        current = graph[7, 4];
        var x = 0;
        var y = 0;
        MazeNode connectingVertex;
        current.mstIn = true;
        //current.inNeighbours.Clear();

        do
        {

            for (var i = 0; i < 2; i++) //add the current's neighbours not already in MST to frontier neighbours
            {
                for (var j = 0; j < 2; j++)
                {
                    if (i == 0 && j == 0 || i==1 && j==1) continue;
                    if (((x = current.indexX + i) >= 0) && (current.indexX + i < 8))
                    {
                        if (((y = current.indexY + j) >= 0) && (current.indexY + j < 8))
                        {
                            if (graph[x, y].mstIn == false) //if not already in the MST
                            {
                                if (!(frontierNeighbours.Contains(graph[x, y]))) //if not already part of frontierNeighbours
                                {
                                    frontierNeighbours.Add(graph[x, y]); 
                                }

                                graph[x, y].inNeighbours.Add(current);

                            }
                        }
                    }
                    if (((x = current.indexX - i) >= 0) && (current.indexX - i < 8))
                    {
                        if (((y = current.indexY - j) >= 0) && (current.indexY - j < 8))
                        {
                            if (graph[x, y].mstIn == false) //if not already in the MST
                            {
                                if (!(frontierNeighbours.Contains(graph[x, y]))) //if not already part of frontierNeighbours
                                {
                                    frontierNeighbours.Add(graph[x, y]);
                                }

                                graph[x, y].inNeighbours.Add(current);


                            }
                        }
                    }
                }
            }


            int random = Random.Range(0, frontierNeighbours.Count); //will get a random result from 0 to frontierNeighbours.Count
            current = frontierNeighbours[random];
            frontierNeighbours.Remove(current); //remove current from frontierNeighbours


            random = Random.Range(0, current.inNeighbours.Count);
            //Debug.log(random);
            connectingVertex = current.inNeighbours[random];
            current.neighbourNodes.Add(connectingVertex);   //connect the new node to one of its random inNeighbors
            connectingVertex.neighbourNodes.Add(current);   //connect its in neighbor to itself

            current.mstIn = true;
            current.inNeighbours.Clear();

            printFrontier();

           // Debug.Log("Frontier neighbours: " + frontierNeighbours);

        } while (frontierNeighbours.Count > 0);

        //frontierNeighbours.add(graph[current.coordinates.x + 1, current.coordi)


    }

    public void printFrontier(){
        string s = "Frontier Neighbours: ";
        for (var i = 0; i < frontierNeighbours.Count; i++){
            s += i + ": " + frontierNeighbours[i].toString() + ", ";
        }
        Debug.Log(s);
    }

    private void drawWalls(){

        //draw vertical then horizontal. 
    }


    // Start is called before the first frame update
    void Start()
    {
        

        for (var i = 0; i < 8; i++){
            for (var j = 0; j < 8; j++){
                //instantiate the tiles
                Instantiate(tilePrefab, new Vector3((xPos - 10 * i), yPos, (zPos + 10 * j)), Quaternion.identity); 
            }
        }


        //for (var i = 1; i <= 8; i++)
        //{
        //    for (var j = 1; j <= 7; j++)
        //    {
        //        if (!(graph[i-1, j-1].neighbourNodes.Contains(graph[i-1, j])))
        //        {
        //            Instantiate(treeWallPrefab, new Vector3((xPos - 10 * i), yPos, (zPos - 5 * j)), Quaternion.identity);
        //        }
        //    }
        //}
        Prim();

        //up, down (HORIZONTAL WALLS) putting walls on the top
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 7; j++)
            {
                if (!(graph[i, j].neighbourNodes.Contains(graph[i, j+1])))
                {
                    Instantiate(treeWallPrefab, new Vector3(((xPos + 5) - 10 * (i)), yPos, ((zPos + 5) + 10 * (j))), Quaternion.identity);
                }
            }
        }



        //left, right (VERTICAL WALLS)  putting walls on the right (left??)
        for (var i = 0; i < 7; i++){
            for (var j = 0; j < 8; j++){
                if( !(graph[i,j].neighbourNodes.Contains(graph[i+1, j])) ){
                    Instantiate(treeWallPrefab, new Vector3(( (xPos - 5) - 10 * (i) ), yPos, ( (zPos - 5) + 10 * (j) ) ), Quaternion.AngleAxis(90, Vector3.up));
                }

            }
        }
        GameObject doorToMaze;
        //make trees surround the whole maze
        for (var i = 0; i < 8; i++){ 
            Instantiate(treeWallPrefab, new Vector3((xPos + 5), yPos, ((zPos - 5) + 10 * (i))), Quaternion.AngleAxis(90, Vector3.up)); //create a line of trees on left side of maze
            Instantiate(treeWallPrefab, new Vector3((xPos -5 -10*7), yPos, ((zPos - 5) + 10 * (i))), Quaternion.AngleAxis(90, Vector3.up)); //create a line of trees on right side of maze

            Instantiate(treeWallPrefab, new Vector3((xPos + 5 -10*(i)), yPos, (zPos - 5)), Quaternion.identity); //create a line of trees on north side of maze

            if(((xPos + 5 - 10 * i) == 140) && ((zPos - 5) + 10 * 8) == 180){
                doorToMaze = Instantiate(treeWallPrefab, new Vector3((xPos + 5 - 10 * (i)), yPos, ((zPos - 5) + 10 * 8)), Quaternion.identity); //create a line of trees on south side of maze
                doorToMaze.gameObject.name = "entry";
            }else{
                Instantiate(treeWallPrefab, new Vector3((xPos + 5 - 10 * i), yPos, ((zPos - 5) + 10 * 8)), Quaternion.identity); //create a line of trees on south side of maze
             }
        }

    



        //FIND THE SHORTEST PATH TO THE EXIT USING DFS
        Stack<MazeNode> myStack = new Stack<MazeNode>();
        List<MazeNode> shortestPath = new List<MazeNode>();
        myStack.Push(graph[4, 7]); //add the root

        do
        {
            //current = graph[4, 0]
            current = myStack.Pop();
            if (!shortestPath.Contains(current))
            {
                shortestPath.Add(current);
            }


            if (current == exit)
            { //done
                Debug.Log("HEEYYY");
                Debug.Log("shortestPath.Count: " + shortestPath.Count);
                Debug.Log("exit: " + exit.indexX + ", " + exit.indexY);
                break;
            }

            bool remove = true;

            for (var i = 0; i < current.neighbourNodes.Count; i++)
            {
                if (current.neighbourNodes[i].visited == false)
                {
                    myStack.Push(current.neighbourNodes[i]);
                    current.neighbourNodes[i].visited = true;
                    remove = false;
                }
            }

            if (remove)
            {
                shortestPath.RemoveAt(shortestPath.Count-1);
            }

        } while (myStack.Count > 0);



        if(shortestPath.Count <= 16){
            for (var i = 0; i < shortestPath.Count; i++)
            { //now draw tiles leading to the path
                Instantiate(pathTilePrefab, new Vector3((xPos - 10 * shortestPath[i].indexX), yPos, (zPos + 10 * shortestPath[i].indexY)), Quaternion.identity);
            }
        }else{
            //prim again
        }






        //if(current.neighbourNodes.Count == 0){
        //    shortestPath.removeAt(shortestPath.Count);
        //}




        //rotate it

        //Prim's algorithm


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
