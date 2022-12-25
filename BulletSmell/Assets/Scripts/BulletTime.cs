using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletTime : MonoBehaviour
{
    
    public PlayerInputActions playerControls;

    private InputAction bulletTime;
    private float waitTimeMultiplier;   
    private float slowDownRatio = 5f;
    private bool canBulletTime;

    private void Awake()
    {
        playerControls = new PlayerInputActions();   
    }

    private void OnEnable()
    {
        bulletTime = playerControls.Player.BulletTime;
        bulletTime.Enable();
        bulletTime.performed += Bullettime;
    }

    private void OnDisable()
    {
        playerControls.Enable();
        bulletTime.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canBulletTime = true;
        waitTimeMultiplier = 1f;
    }

    private void Bullettime(InputAction.CallbackContext context){
        if(canBulletTime){
            StartCoroutine(BulletTimeWait());
        }
    }
    
    private void SetTimeScale(float timeScale, float deltaTime, float newBaseSpeed){
        Time.timeScale = timeScale;
        Time.fixedDeltaTime *= deltaTime;
        gameObject.GetComponent<PlayerMovement>().baseSpeed *= newBaseSpeed;
        gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().baseSpeed;
        waitTimeMultiplier *= deltaTime;
    }

    private IEnumerator BulletTimeWait(){
        canBulletTime = false;
        float tempBaseSpeed = gameObject.GetComponent<PlayerMovement>().baseSpeed;   
        SetTimeScale((1/slowDownRatio), (1/slowDownRatio), slowDownRatio);
        print("BULLET TIME ACTIVE (PLAYER NOT AFFECTED)");
        yield return new WaitForSeconds(1); //Replace with meter but for now have a timer coroutine
        SetTimeScale(1f, slowDownRatio, 1/slowDownRatio);    
        print("BULLET TIME COMPLETE");
        StartCoroutine(RegenerateBulletTime());
    }

    private IEnumerator RegenerateBulletTime(){
        yield return new WaitForSeconds(4); 
        print("You can now use bullet time again");
        canBulletTime = true;
    }
}
