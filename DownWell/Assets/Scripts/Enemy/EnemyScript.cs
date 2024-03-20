using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float hp = 100;
    public float hpMax = 100;
    public float damageAmount = 50; 
    
    void Start()
    {
        hp = hpMax;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}