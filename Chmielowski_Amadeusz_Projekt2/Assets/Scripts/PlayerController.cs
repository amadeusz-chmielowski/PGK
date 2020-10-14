using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movmentSpeed = 4.0f;
    public float jumpForce = 6.0f;
    private Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(movmentSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-movmentSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(transform.position.y < -10)
        {
            transform.position = new Vector3(-13.3f, -1.88f, 0f);
        }
        
        if(transform.rotation.z < -0.01f || transform.rotation.z > 0.01f)
        {
            Debug.LogWarning(transform.rotation.z);
            transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        }
    }
}
