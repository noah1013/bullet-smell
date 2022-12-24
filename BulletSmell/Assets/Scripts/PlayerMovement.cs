using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 500f;
    private float baseSpeed;
    private float slowDownRatio = 5f;
    private float waitTimeMultiplier;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction sprintPress;
    private InputAction sprintRelease;
    private InputAction dash;
    private InputAction bulletTime;
    

    private bool canDash;
    private bool isSprinting;
    private bool canBulletTime;

    private void Awake()
    {
        playerControls = new PlayerInputActions();   
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        sprintPress = playerControls.Player.SprintPress;
        sprintPress.Enable();
        sprintPress.performed += SprintPress;

        sprintRelease = playerControls.Player.SprintRelease;
        sprintRelease.Enable();
        sprintRelease.performed += SprintRelease;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        bulletTime = playerControls.Player.BulletTime;
        bulletTime.Enable();
        bulletTime.performed += BulletTime;
    }

    private void OnDisable()
    {
        playerControls.Enable();
        move.Disable();
        sprintPress.Disable();
        sprintRelease.Disable();
        dash.Disable();
        bulletTime.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        canBulletTime = true;
        isSprinting = false;
        waitTimeMultiplier = 1f;
        baseSpeed = moveSpeed;
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
        rb.velocity = new Vector2(moveDirection.x * (moveSpeed * Time.deltaTime / Time.timeScale), moveDirection.y * (moveSpeed * Time.deltaTime / Time.timeScale) );
        //print(rb.velocity);
    }

    void Dash(InputAction.CallbackContext context){
        if(canDash){
            StartCoroutine(DashWait(0.4f));
        }
    }

    private void SprintPress(InputAction.CallbackContext context){
        if(!isSprinting){
            isSprinting = true;
            moveSpeed = baseSpeed * 1.5f;
        }
    }

    private void SprintRelease(InputAction.CallbackContext context){
        if(isSprinting){
            moveSpeed = baseSpeed;
            isSprinting = false;
        }
    }

    private void BulletTime(InputAction.CallbackContext context){
        if(canBulletTime){
            StartCoroutine(BulletTimeWait());
        }
    }

    private void ChangePlayerRotation(){
        Vector3 mouse = new Vector3(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2), 0);
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
    
    private void SetTimeScale(float timeScale, float deltaTime, float newBaseSpeed){
        Time.timeScale = timeScale;
        Time.fixedDeltaTime *= deltaTime;
        baseSpeed *= newBaseSpeed;
        moveSpeed = baseSpeed;
        waitTimeMultiplier *= deltaTime;
    }

    private IEnumerator BulletTimeWait(){
        canBulletTime = false;
        float tempBaseSpeed = baseSpeed;   
        SetTimeScale((1/slowDownRatio), (1/slowDownRatio), slowDownRatio);
        print("BULLET TIME ACTIVE (PLAYER NOT AFFECTED)");
        yield return new WaitForSeconds(1); //Replace with meter but for now have a timer coroutine
        SetTimeScale(1f, slowDownRatio, 1/slowDownRatio);    
        print("BULLET TIME COMPLETE");
        canBulletTime = true;
    }

    private IEnumerator DashWait(float waitTime){
        canDash = false;
        moveSpeed = baseSpeed * 2.5f;
        yield return new WaitForSeconds(waitTime * waitTimeMultiplier);
        moveSpeed = baseSpeed;
        canDash = true;
    }
}
