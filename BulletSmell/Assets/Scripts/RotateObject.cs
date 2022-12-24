using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //THIS IS A TEST SCRIPT TO VIEW THE EFFETS OF THE BULLET TIME MECHANIC

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, 500f * Time.deltaTime));
    }
}
