using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// public class PoolManager : MonoBehaviour
// {
//     //프리펩을 보관할 변수
//     public GameObject[] prefabs;
//     
//     //풀 담당을 할 리스트
//     private List<GameObject>[] pools; //배열 안에 리스트가 들어있는 형태임,, 리스트 안에 배열 넣을거면 List<T[]> 이렇게 선언해야된다고 하네요
//     
//
//     private void Awake()
//     {
//         pools = new List<GameObject>[prefabs.Length];
//
//         for (int i = 0; i < prefabs.Length; i++)
//         {
//             pools[i] = new List<GameObject>(); //()는 기본생성자 ->초기화 담당, 크기 지정 가능 ..etc
//             
//         }
//     }
//
//     public GameObject Get(int index, Vector3 position = default, Vector3 forward = default)
//     {ㅌ   
//         GameObject obj = null; //어떤 오브젝트 가져올지 정해주는 지역변수
//         //선택한 풀의 (비활성화된) 게임오브젝트에 접근
//         //if 발견하면 obj에 할당
//         //else 못찾았어? 새로 생성해서 할당
//
//         foreach (GameObject item in pools[index]) //pool에 잇는 인덱스 하나하나 끝까지 돌면서 검사하는거임
//         {
//             if (!item.activeSelf) //비활성화 된 게 있어? 
//             {
//                 obj = item;
//                 obj.SetActive(true);
//                 //활성화 ㄱㄱ
//                 break;
//             }
//             
//         }
//
//         if (!obj)
//         {
//             obj = Instantiate(prefabs[index], position, Quaternion.LookRotation(forward),transform);
//         }
//
//         return obj;//리턴
//
//     }
//     
//     

// }



public class PoolManager : MonoBehaviour
{
    //프리펩을 보관할 변수
    public GameObject[] prefabs;
    
    //풀 담당을 할 리스트
    private List<GameObject>[] pools; //배열 안에 리스트가 들어있는 형태임,, 리스트 안에 배열 넣을거면 List<T[]> 이렇게 선언해야된다고 하네요


    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>(); //()는 기본생성자 ->초기화 담당, 크기 지정 가능 ..etc
            
        }
    }

    public GameObject Get(int index)
    {
        GameObject obj = null; //어떤 오브젝트 가져올지 정해주는 지역변수
        //선택한 풀의 (비활성화된) 게임오브젝트에 접근
        //if 발견하면 obj에 할당
        //else 못찾았어? 새로 생성해서 할당

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                obj = item;
                obj.SetActive(true);
                break;
            }
            
        }

        if (!obj)
        {
            obj = Instantiate(prefabs[index], transform);
            pools[index].Add(obj);
        }

        return obj;//리턴

    }     
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // 오브젝트를 비활성화하여 재사용 준비
        //게임 매니저 통해서 건너서 불러오면 될듯
    }
    
}





