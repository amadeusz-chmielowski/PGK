using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{

    public float XMin = 0;
    public float XMax = 119.0f;
    public float YMin = 0;
    public float YMax = 174.0f;
    private float startingPoxitionX;
    private float startingPoxitionY;
    private bool isMovingRight = true;
    private bool isMovingUp = true;
    private bool isMovingDown = false;
    private bool Move = false;
    private Rigidbody2D m_body2d;
    [SerializeField] float m_speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        startingPoxitionX = this.transform.position.x;
        startingPoxitionY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float inputY = 0;
        //Debug.LogWarning(" | Up: " + isMovingUp + " | Down: " + isMovingDown);

        if (isMovingUp && Move)
        {
            if (this.transform.position.y < startingPoxitionY + YMax)
                inputY = 1f; // move up
            else
            {
                isMovingUp = false;
                isMovingDown = true;
                inputY = -1f; //move right
            }
        }
        else if(Move)
        {
            if (this.transform.position.y > startingPoxitionY - YMin)
                inputY = -1f; // move down
            else
            {
                isMovingDown = false;
                inputY = 1;
            }
        }
       // Debug.LogWarning(" | Y:" + Convert.ToString(inputY));
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, inputY * m_speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Move = true;
    }
}
