using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

    public static bool walk = false;
    private Animator animator;
    bool turn = true;
    bool right = true;
    bool walking = false;
    bool jumping = false;
    bool aiming = false;
    private bool border = false;
    int pressed = 0;
    public Transform aim;
    public Rigidbody2D arrow;
    public static bool Fire = false;
    Vector2 forward = new Vector2(5, 0);
    Vector2 back = new Vector2(-5, 0);
    Vector2 airforward = new Vector2(3, 0);
    Vector2 airback = new Vector2(-3, 0);
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumping==false)
        {
            GetComponent<Animator>().SetBool("Jump", true); // анимация прыжка
            GetComponent<Rigidbody2D>().AddForce(transform.up*3500f); // толчек вверх
            jumping = true;
        }
        if (GetComponent<Animator>().GetBool("Jump") == true)
        {
            YMove();
            GetComponent<Animator>().SetBool("Walk", false);
        }

        if (GetComponent<Animator>().GetBool("Jump") == false) XMove();
    }
    void OnCollisionEnter2D(Collision2D obj)// приземление
    {
        if (obj.gameObject.tag == "Ground")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            jumping = false;
        }
    }
    private void YMove()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (right == false) transform.Rotate(Vector2.up, 180);// поворот в сторону ходьбы
            pressed += 1; // количество нажатых кнопок
            turn = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) pressed -= 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (right == true) transform.Rotate(Vector2.up, 180);
            pressed += 1;
            turn = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) pressed -= 1;
        if (!turn == right) // переменная показывает куда лучник смотрит
        {
            right = !right;
        }
        if (pressed > 0) // запуск анимации и движения
        {
            GetComponent<Transform>().Translate(airforward * Time.deltaTime);
        }
    }
    private void Shoot()
    {
        if (aiming==true)
        {
            Rigidbody2D Arrow = Instantiate(arrow, aim.position, aim.rotation) as Rigidbody2D;
            if (right == true) Arrow.velocity = 3f * forward;
            else Arrow.velocity = 3f * back;
            Invoke("Deaim",0.25f);
        }
    }
    private void Deaim()
    {
        aiming = false;
    }
    private void XMove() // ходьба и выстрел
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (aiming == false && jumping==false)
            {
                GetComponent<Animator>().SetBool("Walk", false);
                GetComponent<Animator>().SetTrigger("Aim");
                aiming = true;
                if (jumping != true && walking != true) Invoke("Shoot", 1.15f);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (right == false)
            {
                transform.Rotate(Vector2.up, 180);// поворот в сторону ходьбы
                aim.position = aim.position + new Vector3(0, 0, -4); // перевдижение прицела на передний план
            }
            pressed += 1; // количество нажатых кнопок
            turn = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) pressed -= 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (right == true)
            {
                transform.Rotate(Vector2.up, 180);// поворот в сторону ходьбы
                aim.position = aim.position + new Vector3(0, 0, -4); // перевдижение прицела на передний план
            }
            pressed += 1;
            turn = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) pressed -= 1;
        if (!turn == right) // переменная показывает куда лучник смотрит
        {
            right = !right;
        }
        if (pressed > 0 && aiming == false) // запуск анимации и движения
        {
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Transform>().Translate(forward * Time.deltaTime);
        }
        if (pressed == 0) // остановка
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
    }
    private int Moved()
    {
        if ( Input.touchCount <= 0) return 0;
        Vector2 delta =Input.GetTouch(0).deltaPosition;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                return 1; // Right
            }
            else
            {
                return 2; // Left
            }
        }
        else
        {
            if
            (delta.y > 0)
            {
                return 3; // Up
            }
            else
            {
                return 0; // Down
            }
        }
    }
}
