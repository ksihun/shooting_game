using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//https://discussions.unity.com/t/a-guide-on-using-the-new-ai-navigation-package-in-unity-2022-lts-and-above/371872
//이거 AI navigation 사용 가이드인데 요거 참고해야될듯? 옛날책이랑 많이 달라요..   

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,       //기본
        Walk,       //이동
        Attack,     //공격
        Damaged,    //피격
        Dead        //죽음
    }
    public float hp = 100;
    public Slider hpBar;
    public EnemyState eState = EnemyState.Idle;
        //적의 상태를 담아둘 변수 (시작할때는 기본이겟죠?)
    public float waittime = 2;
    public float timer = 0;

    Transform player;
    private float distance; //유저와의 거리
    private NavMeshAgent agent;

    void Start()
    {
        //player = FindObjectOfType<Player>().transform << 책에는 이걸로 나와잇는데 사라졋나봄.. 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent = GetComponent<NavMeshAgent>(); //나의 NavMeshAgent 컴포넌트 가져오기 (여기선 적 안에 들어가는 코드니까 적의 navMeshAgent겟죠?
        hp = 100;

    }
    void Update()
    {
        //적과 플레이어 거리 계산 ㄱㄱ
        distance = Vector3.Distance(transform.position, player.position);
        switch (eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;
            case EnemyState.Damaged: timer += Time.deltaTime; break;
            case EnemyState.Dead: Dead(); break;
                
        }

        if (timer >= waittime) //맞았으면 waiting time만큼 대기해
        {
            timer = 0;
            Idle();
        }
        
    }
    
    private void OnEnable() // 오브젝트 풀링에의해 다시 활성화될시 정보 초기화
    {
            
            hp = 100;
            eState = EnemyState.Idle;
            hpBar.value = hp;
    }

    void Damaged(float damage)
    {
        hp -= damage;
        hpBar.value = hp;
        agent.isStopped = true; //이동 멈춰
        agent.ResetPath();      //경로 초기화

        if (hp > 0) //피가 있어? 그럼 피격 ㅇㅇ
        {
            eState = EnemyState.Damaged;
            
        }
        else //피가 없어? 그럼 죽어
        {
            eState = EnemyState.Dead;
        }
    }

    public void Idle()
    {
        if (distance <= 8)
        {   
            eState = EnemyState.Walk;
            //거리가 가까워지면 이동 상태로 바꿔라
            agent.isStopped = false; //움직여
            
        }
        
    }

    public void Walk()
    {
        if (distance > 8)
        {
            
            eState = EnemyState.Idle;
            //플레이어가 멀리있으면 기본 대기 상태로 전환해라
            agent.isStopped = true; //멈춰
            agent.ResetPath();      //경로 초기화
        }

        else if (distance <= 2)
        {
            eState = EnemyState.Attack;
            //더 가까워지면 공격상태로 바꾸기 
            agent.isStopped = true; //멈춰
            agent.ResetPath();      //경로 초기화
            
        }
        else
        {
            agent.SetDestination(player.position);
        }
        
    }

    public void Attack()
    {
        if (distance > 2)
        {
            eState = EnemyState.Walk;
            //다시 거리 벌리면 다시 이동 상태로 바꾸기
            agent.isStopped = false; //움직여
        }
        
    }

    public void Dead()
    {
        GameManager.instance.pool.ReturnToPool(gameObject);
        
            
    }
    
    
}
