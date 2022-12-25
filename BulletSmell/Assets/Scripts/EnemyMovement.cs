using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed;
    private bool moveRight;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        moveSpeed = 2f;
        moveRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > originalPos.x + 3)
        {
            moveRight = false;
        }

        else if (transform.position.x < originalPos.x - 3)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}



