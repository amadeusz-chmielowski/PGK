using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movmentSpeed = 4.0f;
    public float jumpForce = 6.0f;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    private bool firstJump = false;
    public Animator animationOfPlayer;
    private bool isWalking = false;
    private bool isFacingRight;
    private int money = 0;
    private int eur = 0;
    private int gbp = 0;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = false;
        animationOfPlayer.SetBool("isGrounded", IsGrounded());
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!isFacingRight)
            {
                Flip();
            }
            transform.Translate(movmentSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (isFacingRight)
            {
                Flip();
            }
            transform.Translate(-movmentSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking = true;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Jump();
        }
        animationOfPlayer.SetBool("isGrounded", IsGrounded());
        animationOfPlayer.SetBool("isWalking", isWalking);

        if(transform.position.y < -10)
        {
            transform.position = new Vector3(-13.3f, -1.88f, 0f);
        }
        
        if(transform.rotation.z < -0.01f || transform.rotation.z > 0.01f)
        {
            //Debug.LogWarning(transform.rotation.z);
            transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        }
        animationOfPlayer.SetBool("isGrounded", IsGrounded());
        animationOfPlayer.SetBool("isWalking", isWalking);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 0.8f, groundLayer.value);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            firstJump = true;
            isWalking = false;
            animationOfPlayer.SetBool("isJumping", true);
        }
        if (firstJump && !IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce * 0.7f, ForceMode2D.Impulse);
            firstJump = false;
            isWalking = false;
            animationOfPlayer.SetBool("isJumping", true);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("GameObject1 collided with " + other.name);
        if (other.CompareTag("CoinEUR"))
        {
            money += 1;
            eur += 1;
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("CoinGBP"))
        {
            money += 1;
            gbp += 1;
            other.gameObject.SetActive(false);
        }
    }
}
