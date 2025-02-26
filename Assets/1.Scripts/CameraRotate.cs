using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class CameraRotate : MonoBehaviour
{

    public float rotationspeed = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float temp_angle_x;

    // Update is called once per frame
    void Update()
    {
        float mouseMoveY = Input.GetAxis("Mouse Y");
        transform.Rotate(-mouseMoveY * rotationspeed*Time.deltaTime, 0, 0);
        //print(transform.eulerAngles.x);


        if (transform.eulerAngles.x > 180)
        {
            temp_angle_x = transform.eulerAngles.x - 360;
        }
        else
        {
            temp_angle_x = transform.eulerAngles.x;
        }
        
        temp_angle_x = Mathf.Clamp(temp_angle_x,-30,30);
        //Mathf.Clamp(A,Min,Max) ==> A값을 B와 C 사이 값으로 제한 
        transform.eulerAngles = new Vector3(temp_angle_x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
