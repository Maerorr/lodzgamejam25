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
            
            // Sprawdzenie, czy obiekt, z kt�rym mamy kolizj�, ma odpowiedni� warstw� (np. "Player")
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                
                // Zniszczenie obiektu, z kt�rym kolidujemy
                Destroy(other.gameObject);

            }
        }

    }
}
