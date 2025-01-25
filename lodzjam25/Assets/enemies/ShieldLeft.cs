using UnityEngine;

public class ShieldLef : MonoBehaviour
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
        if (GetComponentInParent<EnemyMelee>().isTarczaSzmato && GetComponentInParent<EnemyMelee>().direction.x < 0)
        {
            
            // Sprawdzenie, czy obiekt, z którym mamy kolizjê, ma odpowiedni¹ warstwê (np. "Player")
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                
                // Zniszczenie obiektu, z którym kolidujemy
                Destroy(other.gameObject);

            }
        }

    }
}
