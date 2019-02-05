using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject WallRight; // for extending to right
    public GameObject WallBottom;// for extending to bottom
    public GameObject Cell_prefab;
    public GameObject Cell_LeftMost;
    public GameObject Cell_RightMost;

    public GameObject Row;
    public Cell[] Row_Cells; // can be of size 8 afterwards 
    public GameObject Row_prefab;
    public ArrayList allSets = new ArrayList();
    public Canvas canvas;   // ending screen
    bool entered = false; // If player entered the maze

    // Projectiles in each cell 
    public GameObject baseball; //items
    public GameObject PlayerBaseBall; // current projectile
    // Make a array of sets total 8 
    public int rownumber;

    private GameObject player; // player model
    private Cell[] newcells; // not used 


    public Vector3 LastRowBound = new Vector3(0,0,0);
    // Use this for initialization
    //private GameObject Cell_Block;
    void Start()
    {
        Vector3 FirstRowPosition = new Vector3(309.7f, -258.3f, 231.5f);

        Vector3 position = new Vector3(45, 1, 480);
        player = Instantiate(playerObject, position, Quaternion.identity);
        // Row    = Instantiate(Row_prefab, FirstRowPosition, Quaternion.identity);
        Row.SetActive(false);
        Row_Cells = Row.GetComponentsInChildren<Cell>();


        print(" TIME PASSED = " + Time.realtimeSinceStartup);



    }


    void startRow(Cell[] current_row)
    {
        ArrayList destroyingset = new ArrayList();

        System.Random r = new System.Random();
        int toUnion;
        Cell c;
        Cell c1;
        for (int i = 0; i < current_row.Length - 1; i++)
        {
            c = current_row[i];
            c1 = current_row[i + 1];

            // randomly decide to union or not
            toUnion = r.Next(0, 2);
            if (toUnion == 1 && c.cellno != c1.cellno) // adjacenet sets are different, then union them
            {
                c1.cellno = c.cellno;


                c.destroyRight();   // after uinion destroy 
                                    //allSets.Add(c.cellno);

                // from the newly unionized cells, randomly decide one of the bottom walls to be destroyed 
                toUnion = r.Next(0, 2);

                if (toUnion == 1) {
                    c.destroyBottom();
                    c.created = true;
                }
                else
                {
                    c1.destroyBottom();
                    c1.created = true;
                }
            }
        }
        // if marked to create a bottom hole, create bottom hole fir ending point and starting point 
        if (current_row[0].created == false)
        {
            current_row[0].destroyBottom();
            current_row[0].created = true;
        }

        if (current_row[7].created == false)
        {
            current_row[7].destroyBottom();
            current_row[7].created = true;
        }

        Row_Cells = current_row;
    }

    // Function is used to close, open bottom of the maze as a way to end the infinite maze
    // adds the final row that is prefabed or add walls as such 
    void finishMaze(Cell[] current_row)
    {
        Cell c;
        for (int i = 0; i < 8; i++)
        {
         
            c = current_row[i];
            if (c.CheckBottom()== true)
            {
                c.createBottomWall();
            }
        }
    }

    
    Cell[] createMaze(Cell[] current_row)
    {
    
        ArrayList UsedNumbers = new ArrayList(); // save all the numbers that have been used so only to reassign numbers that are not used 

        System.Random r = new System.Random();
        int toUnion;



        GameObject[] newCells = new GameObject[8]; // start making a new row
        // instatiate a new row, if the number is 0, then we are left most hence spawn leftmost cell

        //------------[ Spawn Next Cell ] ----------//
        for (int i = 0; i < 8; i++)
        {
            Vector3 v = current_row[i].transform.position;
            if (i == 0)
            {
                newCells[i] = Instantiate(Cell_LeftMost, new Vector3(v.x, v.y, v.z + 30), new Quaternion());
                Instantiate(baseball, new Vector3(v.x, v.y, v.z + 30), new Quaternion());

            }
            else
            {
                newCells[i] = Instantiate(Cell_prefab, new Vector3(v.x, v.y, v.z + 30 ), new Quaternion());
                Instantiate(baseball, new Vector3(v.x, v.y, v.z + 30), new Quaternion());
            }

     //--------------------------[ Assigning numbers extending to eller ] -------------------------// 

        // if last rows cell has the tag created indicated, a cell below should be created 
            if (current_row[i].GetComponent<Cell>().created == true)
            {
                UsedNumbers.Add(current_row[i].cellno); // as number has been used up, mark it to avoid duplicate
                newCells[i].GetComponent<Cell>().cellno = current_row[i].cellno;
            }
        }

        // generate here a number exluding those numbers in UsedNumbers
        for (int i = 0; i < 8; i++)
        {
            int rand;
            if (newCells[i].GetComponent<Cell>().cellno == "null")
            {
                rand = r.Next(0, 8);
                while (UsedNumbers.Contains(rand)) { 
                  newCells[i].GetComponent<Cell>().cellno = "" + rand;
                }
            }
        }
        
        // rerun eller first step 
        for (int i = 0; i < 7; i++)
        {
            if (r.Next(0, 2) ==1)
            {
               
                newCells[i].GetComponent<Cell>().cellno = newCells[i + 1].GetComponent<Cell>().cellno;
            }
        }


        for (int i = 0; i < 7; i++)
        {
            if (newCells[i].GetComponent<Cell>().cellno == newCells[i + 1].GetComponent<Cell>().cellno)
            {
            
             newCells[i].GetComponent<Cell>().destroyRight();



                    toUnion = r.Next(0, 2);
                    if (toUnion == 1)
                    {
                    newCells[i].GetComponent<Cell>().destroyBottom();
                    newCells[i].GetComponent<Cell>().created = true;
                    }
                    else
                    {
                    newCells[i+1].GetComponent<Cell>().destroyBottom();
                    newCells[i+1].GetComponent<Cell>().created = true;
                    }
                
            }
        }
        
        /*
        if (newCells[0].GetComponent <Cell>().created == false)
        {
            newCells[0].GetComponent<Cell>().destroyBottom();
            newCells[0].GetComponent<Cell>().created = true;
        }

        if (current_row[7].GetComponent<Cell>().created == false)
        {
            current_row[7].GetComponent<Cell>().destroyBottom();
            current_row[7].GetComponent<Cell>().created = true;
        }
        */

        for (int i = 0; i < 8; i++)
        {
            Row_Cells[i] = newCells[i].GetComponent<Cell>();
        }
        return Row_Cells;
    }


    // Update is called once per frame
    void Update()
    {
        
        // Keeps track if we have entered the maze 
        
        // If at the edge of starting terrain, activate the first row 
        // update a entered saying if not entered Then entering will start the row as maze 
        if (entered == false && player.transform.position.x > 51 && player.transform.position.x < 90 && player.transform.position.z > 500 && player.transform.position.z < 502)
        {
            entered = true;
            Row.SetActive(true); // Activate the prefab set before hand in position 
            if (entered == true)
            {
                startRow(Row_Cells);
            }
            rownumber = 1; // using this to calculate the position of next row dynamically 
            LastRowBound = Row_Cells[7].transform.position;
        }

     //------------------------[ Expand the maze ] -------------------------// 

        // If I have not passed the point of maze, then only expand the maze, else not so. 
        // When player passes a trigger point p, next part of the maze is create 
            Vector3 v = Row_Cells[7].transform.position;
            v.z = v.z + 15;
           
        
            if (player.transform.position.z > (500 + 30 * rownumber) && player.transform.position.z < (502.0 + 30 * rownumber))
            {
                foreach (Cell c in Row_Cells)
                {
                    c._passed = true; 
                }
                Row_Cells = createMaze(Row_Cells);
        
                rownumber++;
                LastRowBound = Row_Cells[7].transform.position;
        }
        //------------------------[ Finish Maze ] -------------------------// 

        /*
        if (GameObject.FindGameObjectsWithTag("bullet") != null)
        {
            
            PlayerBaseBall = GameObject.FindGameObjectWithTag("bullet");
            print("hello");
            if (PlayerBaseBall.transform.position.z > LastRowBound.z && LastRowBound.z != 0)
            {
                finishMaze(Row_Cells);
            }
        }

        */
        //------------------------[ End The Game ] -------------------------// 

        if (player.transform.position.x>408 && player.transform.position.x<429 & player.transform.position.z > 466.6 && player.transform.position.z < 476.6)
        {
            endGame();
        }
    }

    void endGame()
    {
        // Disable movement from the character
        CharacterControl cc = player.GetComponent<CharacterControl>();
        cc.speed = 0;
        cc.jumpSpeed = 0;

        // Show end screen
        Instantiate(canvas, new Vector3(0, 0, 0), Quaternion.identity);

        // Get script and disable it by multiplying sensitivity by 0 
        canMouseLook mouse = player.GetComponentInChildren<canMouseLook>();
        mouse.sensititiy = 0 ;
     
    }

    //---------------[ Helper Methods ]---------------//
    void mazecount()
    {
        print(allSets);
    }

    // respawn if do fall off by accident, is for convience only 
    void respawn(GameObject player)
    {
        player.transform.position = new Vector3(35, 50, 30);
    }

    ArrayList destructionCell(ArrayList a)
    {

        return a;
    }


   
}
   

