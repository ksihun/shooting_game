using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.pool.Get(4);
            
        }
    }
}
