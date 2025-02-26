using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        //카메라가 바라보는 방향대로 내가 바라보는 방향 설정 ==이거 넣어놓으면 이 넣어놓은 오브젝트가 카메라를 바라봄 ㅇㅇ
    }
}
