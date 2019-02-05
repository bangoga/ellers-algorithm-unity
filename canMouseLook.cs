using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canMouseLook : MonoBehaviour {

    Vector2 mouselook;
    Vector2 smoothV;
    public float sensititiy = 1.0f;
    public float smoothing = 2.0f;

    GameObject player;
	// Use this for initialization
	void Start () {
        player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.None;
        var md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensititiy * smoothing, sensititiy * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouselook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);

    }

}
