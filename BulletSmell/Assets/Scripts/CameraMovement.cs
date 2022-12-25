using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Awake() {
        Camera.main.orthographicSize = 8;
    }

    void FixedUpdate(){
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
    
}
