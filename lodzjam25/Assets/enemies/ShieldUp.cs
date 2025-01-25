using UnityEngine;

public class ShieldUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((GetComponentInParent<EnemyMelee>() == null))
        {

        }
        if (!GetComponentInParent<EnemyMelee>().isTarczaSzmato)
        {
                // Zniszczenie obiektu, z którym kolidujemy
                Destroy(other.gameObject);

            
        }
    }
}
