using UnityEngine;

public class ShootEffectspawn : MonoBehaviour
{

    public float waitTime;
    public float timer = 0;
    
    private void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            GameManager.instance.pool.ReturnToPool(gameObject);
        }
    }
}
