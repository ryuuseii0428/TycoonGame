using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetting : Singleton<CharacterSetting>
{
    Vector3 resetPos = new Vector3(0, 0, 0);


    enum State
    {
        Idle,
        Walk,
        Clicker,
        Pick
    }

    State state = State.Idle;
    public SpriteRenderer spr;

    float speedX;
    float speedY = 0;


    Vector3 ranPoint;

    float pickTime = 0;

    //public Animator anim;

   
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();


        int ranSec = Random.Range(3, 5);

        Invoke("ranSecState", ranSec);

        ranSecState();
    }


   /* public void CheckBorder()
    {
        if(transform.position.x < topLeft.position.x 
        || transform.position.x > bottomRight.position.x 
        || transform.position.y > topLeft.position.y
        || transform.position.y < bottomRight.position.y)
        
        {

            SetPoint();
            Vector3 pos = ranPoint - transform.position;
            Vector3 posChang = pos.normalized * 0.3f;
            speedX = posChang.x;
            speedY = posChang.y;

        }

    } */

    public void SetPoint()
    {
        Vector3[]point = GameManager.Instance.pointList;
        ranPoint = point[Random.Range(0, 3)];

    }

    public void ranSecState()
    {
        int ranSec = Random.Range(3, 5);

        state = (State)Random.Range(0, 2);

        float ranSpd = Random.Range(-0.8f, 0.8f);
        speedX = ranSpd;

        Invoke("ranSecState", ranSec);

    }

    // Update is called once per frame
    void Update()
    {
        if(speedX > 0)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }

        switch(state)
        {
            case State.Idle:

                //anim.SetBool("isWalk", false);
                break;
            
            case State.Walk:
               //anim.SetBool("isWalk", true);
                transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
                //CheckBorder();
                break;
            case State.Clicker:
                Invoke("Timer", Random.Range(2, 4));
                break;
            case State.Pick:
                transform.position = GetMousePoint();
                break;


        }


    }


    Vector3 GetMousePoint()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 MousePos = new Vector3(mouse.x, mouse.y, mouse.y);
        return MousePos;
    }

    void OnMouseDrag()
    {
        pickTime += Time.deltaTime;

        if(pickTime > 0.5f)
        {
            state = State.Pick;
        }   
    }

    private void OnMouseUp()
    {
        if(state == State.Pick)
        {
            pickTime = 0;
            state = State.Idle; 
            //CheckBorder();
            transform.position = ranPoint;
              
        }
        else
        {
            pickTime = 0;
            state = State.Idle; 
        }
    }


    void Timer()
    {
        if(state != State.Pick)
            state = State.Walk;
    }

    private void OnMouseDown()
    {
        state = State.Clicker;
        //anim.SetTrigger("doTouch");
        
    }

    

    



}
