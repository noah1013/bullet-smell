using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction dash;

    private bool canDash;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable()
    {
        playerControls.Enable();
        move.Disable();
        dash.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>(); //Player movement 
        ChangePlayerRotation();                    //Player rotation
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
    }

    void Dash(InputAction.CallbackContext context){
        if(canDash){
            StartCoroutine(DashWait());
        }
    }

    private void ChangePlayerRotation(){
        Vector3 mouse = new Vector3(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2), 0);
        //print(mouse);
        float hypotenuse = Vector3.Distance(mouse, new Vector3(0.0f, 0.0f, 0.0f));
        float angle = GetAngle(hypotenuse, mouse.x);

        if(mouse.y < 0)
            angle *= -1;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle -90);
    }

    private float GetAngle(float hypotenuse, float adjacent){
        float angle = (float) Math.Acos(adjacent/hypotenuse);   //angle in radians
        angle = RadiansToDegrees(angle);                        //angle in degrees
        return angle;
    }

    private float RadiansToDegrees(float angle){
        float degrees =  angle * (180f / (float) Math.PI);
        return degrees;
    }

    private IEnumerator DashWait(){
        float temp = moveSpeed;
        moveSpeed = 20f;
        canDash = false;
        yield return new WaitForSeconds(0.4f);
        moveSpeed = temp;
        canDash = true;
    }
}
