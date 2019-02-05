using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

// After hitting the wall, destroy yourself 
 void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cell_A") 
        {
            Destroy(gameObject);
        }
    }

}
