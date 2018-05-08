using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    Vector2 move = new Vector2(10, 0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter2D( Collider2D obj)
    {
        if( obj.gameObject.tag == "Border") Destroy(this.gameObject);
    }
}
