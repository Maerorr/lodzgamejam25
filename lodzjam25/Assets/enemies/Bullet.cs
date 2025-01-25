using UnityEngine;

public class Bullet : MonoBehaviour
{

    Vector2 target;
    float speed;
    Vector2 direction;
    public int damage = 10; // IloúÊ zadawanych obraøeÒ
    public float lifetime; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void setTarget(Player player) { target = new Vector2(player.transform.position.x,player.transform.position.y); direction = (target - (Vector2)transform.position).normalized; }

    // Update is called once per frame
    void Update()
    {
        // Aktualizacja czasu øycia
        lifetime -= Time.deltaTime;

        // Jeúli czas øycia minπ≥, zniszcz pocisk
        if (0 > lifetime)
        {
            Destroy(gameObject);
            return;
        }

        // Sprawdü, czy pocisk dotar≥ do celu
        Vector2 currentPosition = transform.position;
        if (Vector2.Distance(currentPosition, target) <= speed * Time.deltaTime)
        {
            // Ustaw nowy kierunek po dotarciu do celu
            transform.position = target; // Zapewnia, øe cel zosta≥ osiπgniÍty
            target += direction; // Kontynuuj lot po tym samym kierunku
        }

        // Ruch pocisku w kierunku celu
        transform.position = Vector2.MoveTowards(currentPosition, target, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        // Sprawdü, czy pocisk trafi≥ w gracza
        if (collision.CompareTag("Player"))
        {
            // Pobierz komponent zdrowia gracza
            //collision.GetComponent<Player>().decreaseHealth;
           

            // Zniszcz pocisk
            Destroy(gameObject);
        }
    }
}
