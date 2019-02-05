using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    // Cells are also tagged as either a or b 
    // a are all normal cells that can break
    // b are the final cells in the row that can't break

    public string cellno;      // The current cells no
    // if the number coming in is n and the set number is represented by m and n = m then they are the same sets. 
    public GameObject RightWall;
    public GameObject BottomWall;
    public GameObject LeftWall;

    public GameObject BottomWall_Prefab;
    public string type;
    public bool created = false;
    public bool _passed = false;

    // Use this for initialization

    // Cell responsibilities to check its walls and destroy its walls 
    public bool CheckBottom()
    {
        if (this.BottomWall == null)
        {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    public bool Checkright() {
        if (RightWall == null)
        {
            return false;
        }
        return true;
    }

    public void destroyBottom()
    {
        Destroy(BottomWall.gameObject);
    }

    public void destroyRight()
    {
        Destroy(RightWall.gameObject);
    }

 

    
    public  void createBottomWall()
    {
        GameObject buttom;

        Vector3 v = this.transform.position;
        Vector3 Position = new Vector3(v.x, v.y, v.z + 30);
        buttom = Instantiate(this.gameObject, Position, Quaternion.identity);
        buttom.tag = "Cell_B"; // too unsure they can't be destroyed 
    }
    // Add wall top 
}
