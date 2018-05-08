using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Не успел доделать сам выстрел - инициализацию стрелы
    public Rigidbody2D arrow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Hunter.Fire == true)
        {
            //Instantiate(Arrow);
            Hunter.Fire = false;
        }
    }
}
