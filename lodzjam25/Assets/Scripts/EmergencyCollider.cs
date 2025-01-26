using UnityEngine;

public class EmergencyCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out EnemyMelee en))
        {
            en.death();
        }
        else
        if (collider.gameObject.TryGetComponent(out Enemyrange en2))
        {
            en2.death();
        }
    }
}
