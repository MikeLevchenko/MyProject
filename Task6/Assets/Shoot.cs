using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    // Выстрел, пока не уверен как заставить инициалироваться стрелу с нужной точки
    public Rigidbody2D Arrow;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Hunter.Fire == true)
        {
            Instantiate(Arrow) ;
            Hunter.Fire = false;
        }
    }
}
