using UnityEngine;

public class SodaPrefabMovement : MonoBehaviour
{
    public GameObject prefab;
    public float initialSpeed;
    public float velocityY=0;
    float initialYPos;
    float currentYPos;
    public float g = 0;//-9.81f;
    Vector3 direction;
    public float lifeTime = .75f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialYPos = transform.position.y;
        velocityY = initialSpeed * direction.y;
    }

    // Update is called once per frame
    void Update()
    {
        // if(initialYPos - 4.0f < transform.position.y)
        // {
        // Negacja maski warstwy "Player"

        // Sprawdzamy kolizjê z wszystkimi warstwami oprócz "Player"
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y),
            0.1f,
             LayerMask.GetMask("Default")
        );

        // Jeœli wykryto kolizje
        foreach (var hitCollider in hitColliders)
        {
            // Usuwamy obecny obiekt
            Destroy(gameObject);

            // Obliczamy k¹t
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

            // Tworzymy now¹ instancjê prefabrykatu
            GameObject instance = Instantiate(
                prefab,
                new Vector3(transform.position.x, transform.position.y - 0.1f, 0),
                Quaternion.Euler(0, 0, angle)
            );

            // Zakoñcz funkcjê, poniewa¿ obiekt zosta³ zniszczony
            return;
        }
        float posY = transform.position.y +  ((velocityY * Time.deltaTime + g*(Time.deltaTime*Time.deltaTime)/2.0f));
            transform.position =(new Vector3(transform.position.x + direction.x*(Time.deltaTime * initialSpeed),posY,transform.position.z));

            lifeTime -= Time.deltaTime;
        if(lifeTime < 0 ) { Destroy(gameObject); }

            //velocityY += g * Time.deltaTime;
       // }
     //   else { Destroy(gameObject); }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }
}
