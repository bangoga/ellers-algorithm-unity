using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    public string wall;
    public int health = 2;
    public bool breakable = true;
    public Renderer rend;
    void Start()
    {
        rend = this.GetComponent<Renderer>();
    }

    void Update()
    {
        
     }

    public string getName()
    {
        return wall;
    }

    // Wall breaks when its hit more that 2 times 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BaseBall")
        {
            if (health > 0)
            {
                health--;
                // Change color for damamged item 
                //rend.material.shader = Shader.Find("brick");
                //rend.material.SetColor("brick", Color.green);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }


}
