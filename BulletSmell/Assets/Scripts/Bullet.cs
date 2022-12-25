using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // movement
    private Vector2 moveDirection;
    private float speed = 5f, acceleration, angularSpeed, angularAcceleration;
    private bool isBoomerang;

    // game mechanics
    private int attackPower = 1;
    private bool fromPlayer = false;

    private void OnEnable()
    {
        Invoke("Destroy", 10f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // move and accelerate
        if (isBoomerang)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(moveDirection * Mathf.Abs(speed) * Time.deltaTime);
        }
        speed += acceleration * Time.deltaTime;

        // angular move and accelerate
        moveDirection = Quaternion.AngleAxis(angularSpeed * Time.deltaTime, Vector3.forward) * moveDirection;
        angularSpeed += angularAcceleration * Time.deltaTime;
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

    // ### ON BULLET INSTANTIATION SET VARIABLES
    public void SetMovementVariables(Vector2 initialDirection, float speed, float acceleration = default, float angularSpeed = default, float angularAcceleration = default, bool isBoomerang = default)
    {
        moveDirection = initialDirection;
        this.speed = speed;
        this.acceleration = acceleration;
        this.angularSpeed = angularSpeed;
        this.angularAcceleration = angularAcceleration;
        this.isBoomerang = isBoomerang;
    }
}