using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    EnemyMelee enemy;
    void Start()
    {
        enemy = GetComponent<EnemyMelee>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy.isTarczaSzmato)
        {
            if (enemy.direction.x < 0)
            {

            }
            else
            {

            }
        }
    }
}
