using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        Camera.main.orthographicSize = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
    
}
