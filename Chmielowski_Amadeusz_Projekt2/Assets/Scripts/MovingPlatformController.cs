using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    public float XMin = 0;
    public float XMax = 119.0f;
    public float YMin = 0;
    public float YMax = 174.0f;
    private float startingPoxitionX;
    private float startingPoxitionY;
    private bool isMovingRight = true;
    private bool isMovingUp = false;
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
        float inputX = 0;
        //Debug.LogWarning("Right: " + isMovingRight + " | Left: " + !isMovingRight);

        if (isMovingRight && Move)
        {
            if (this.transform.position.x < startingPoxitionX + XMax)
                inputX = 1f; //move right
            else
            {
                isMovingRight = false;
                inputX = -1f;
            }
        }
        else if(Move)
        {
            if (this.transform.position.x > startingPoxitionX - XMin)
                inputX = -1f; // move left
            else
            {
                isMovingRight = true;
                inputX = 1f; //move right
            }
        }
       // Debug.LogWarning("X:" + Convert.ToString(inputX) );
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Move = true;
    }
}
