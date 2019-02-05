using UnityEngine;
using System.Collections;

public class CharacterControl: MonoBehaviour {
	CharacterController characterController;

	    public float speed = 10.0f;
	    public float jumpSpeed = 40.0f;
	    public float gravity = 20.0f;
        public int ammo;
        public GameObject projectile; // prefab 
        public GameObject baseball ;
     

	    private Vector3 moveDirection = Vector3.zero;
        private Rigidbody rb;
	    void Start()
	    {
	        characterController = GetComponent<CharacterController>();
            rb = GetComponent < Rigidbody > ();
      
	    }

	    void Update()
	    {
        // Move only if the capsule is on the ground
        if (characterController.isGrounded)
        {

            // Get the Direction that if being moved and factor * by the speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal")*speed, 0.0f, Input.GetAxis("Vertical")*speed);
            

            // Jump button binded by space
            if (Input.GetButton("Jump"))
            {
                moveDirection.x = 0;
                moveDirection.z = 0;
                moveDirection.y = jumpSpeed;

            }

            // press f to shoot, adds force to after getting direction 
            if (Input.GetKeyDown("f") && ammo>0)
            {
                // destroy the previous baseball if its around 
                Destroy(baseball);
                Vector3 p = this.transform.position;
                Vector3 p1 = new Vector3(p.x, p.y, p.z + 10);
                baseball = Instantiate(projectile, p1, Quaternion.identity) as GameObject;
                baseball.tag = "bullet";
                //print(transform.forward);
                baseball.GetComponent<Rigidbody>().AddForce(transform.forward * 10000); // projectiles are being fired 
                ammo--;
            }
            else if (Input.GetKeyDown("f") && ammo == 0)
            {
                print("Out Of Ammo");
            }


        }
	        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller

        moveDirection = transform.rotation * moveDirection;
        characterController.Move(moveDirection * Time.deltaTime);

        //------------------------[ Finish Maze ] -------------------------// 
       /* if (baseball.transform.position.z > m.LastRowBound.z)
        {
            m.finishMaze(m.Row_Cells);
        }*/
    }

    // destroy on collision the projectile to indicate picking up 
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "BaseBall" )
            {
                ammo = ammo + 1;
                GameObject x = collision.gameObject;
              
                Destroy(x);
            }

    }

}



// Needs to be able to pick ammo
// shoot projectiles 

