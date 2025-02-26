using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    public float moveSpeed;
    public float jumpForce;
    public float rotateSpeed;
    Rigidbody rb;
    int jumpCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize(); //대각 이동에서도 속도 동일하게 하기 위해 정규화 ( 근데 이거 세심한 이동이 안되지않나요)
        //ex) w키를 0.1초 눌렀을때 원래라면 getaxis에서 1보다 작은 무언가 값을 가져와 이동할텐데 Normalize하면 그냥 무조건 1입력되는거아닌가
        dir = transform.TransformDirection(dir); // 플레이어를 기준으로 방향 조절
        // transform.position += dir*(moveSpeed*Time.deltaTime); << 좌표로 이동 (rigidbody x)
        rb.MovePosition(rb.position + dir * (moveSpeed * Time.deltaTime));
        
        

        if (Input.GetButtonDown("Jump")&& jumpCount<2)
        {
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
        
        
        float mousemoveX = Input.GetAxis("Mouse X");
        //print(mousemoveX);
        transform.Rotate(0, mousemoveX * rotateSpeed *Time.deltaTime, 0);

        
        
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Ground")
        {
            jumpCount=0;
            //print("바닥 닿음!");
        }
            
    }
}
