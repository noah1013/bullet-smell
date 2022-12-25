using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // movement
    private Vector2 moveDirection;
    private float moveSpeed;
    // game mechanics
    private int attackPower = 1;
    private bool fromPlayer = false;

    private void OnEnable()
    {
        Invoke("Destroy", 5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" ^ fromPlayer)
        {

            if (other.gameObject.TryGetComponent<EntityHealth>(out EntityHealth entityHealthComponent))
            {
                entityHealthComponent.TakeDamage(attackPower);
            }
            Debug.Log("You got hit!");
            Destroy(gameObject);
        }
    }
}