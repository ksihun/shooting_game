using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerFire : MonoBehaviour
{
    //총알 프리펩을 담아둘 변수
    public GameObject bulletPrefab;
    public float firePower;
    public int gunmode;

    private Camera cam;
    //총 변경을 위한 변수

    
    //총 효과 프리펩을 담아둘 변수
    public GameObject shootEffectPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Confined;
        cam=Camera.main;


    }

    
    // Update is called once per frame
    void Update()
    
    {
        if (Input.GetKey(KeyCode. Alpha1))
        {
            gunmode = 1;
        }
        if (Input.GetKey(KeyCode. Alpha2))
        {
            gunmode = 2;
        }
        //마우스 좌클릭 누르면
        if (Input.GetMouseButtonDown(0))
        {
            //보는 화면의 좌표는 2차원이니까 2차원으로 정의
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            //ViewportPointToRay 와 ScreenPointToRay 차이: view는 좌표 범위가 (0,1) screen은 (0,해상도)
            RaycastHit hit;
            Vector3 point = ray.direction;
            //Ray에 부딪힌 물체 저장
            

            if (gunmode == 1)
            {
                //vector.forward ( player 위치에서 (0,0,1) z축 +1)
                //Quaternion.identity = 모든 축으로의 회전 각도가 0인 변수 ==근데 이거 왜씀?
                //bullet에 프리펩 복사본 생성해서 저장 
                GameObject bullet = Instantiate(bulletPrefab, cam.transform.position,
                    Quaternion.identity);
                
                //총알 생성
                bullet.GetComponent<Rigidbody>().AddForce(point* firePower, ForceMode.VelocityChange);
                //ForceMode 연속적인힘= .Force(무게O), Acceleration(무게x) // 순간적인 힘 = Impulse(무게O), VelocityChange(무게x)
                

            }
            else if (gunmode == 2)
            {


                if (Physics.Raycast(ray, out hit))
                {


                    //Raycast함수 = Ray에 맞은 오브젝트를 hit으로 보내는 역할
                    print(hit.transform.name);
                    /*GameObject shootEffect = Instantiate(shootEffectPrefab, hit.point + hit.normal * 0.01f,
                        Quaternion.LookRotation(hit.normal));*/
                    GameObject G_effect =GameManager.instance.pool.Get(0);
                    //shootEffect.transform.SetParent(hit.transform);
                    G_effect.transform.position = hit.point+hit.normal * 0.01f;
                    G_effect.transform.rotation = Quaternion.LookRotation(hit.normal);
                }

                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.SendMessage("Damaged",10);
                }

            }   


            
        }
        
    }
}
