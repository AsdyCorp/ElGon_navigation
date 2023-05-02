using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Time.deltaTime * Vector3.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Time.deltaTime * Vector3.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(Time.deltaTime * Vector3.up * -rotateSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Time.deltaTime * Vector3.up * rotateSpeed, Space.World);
        }
    }
}
