using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealConsumables healConsumables;
    [SerializeField] PlayerHealthBar healthBar;


    public float playerHealth = 100f;
    public int lifeCharges = 3;
    private Vector3 latestCheckpoint = new Vector3(0.0f,0.0f,0.0f);
    private Soda soda;
    public Rigidbody2D rb;
    public float knockbackValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soda = GetComponent<Soda>();

        if(healConsumables)
        {
            healConsumables.SetAvailableHeals(lifeCharges);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar)
        {
            healthBar.SetCurrentHealth(playerHealth);
        }
        CheckForCheckpoint();
        if (Input.GetMouseButton(0))
        {
            soda.isEmitting = true;
            soda.Emit();
        }

        if (Input.GetMouseButtonUp(0))
        {
            soda.isEmitting = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
           
            UseCharge();
        }

        if(soda.isKnockback)
        {
            //Vector2 buffer = new Vector2(soda.direction.x, soda.direction.y);
            float value = (soda.direction.y * -1) + 1;
            rb.AddForce(new Vector2(-soda.direction.x * knockbackValue, value * knockbackValue), ForceMode2D.Force);
            soda.isKnockback = false;
        }

    }

    void DecreaseHealth(float damage)
    {
        playerHealth -= damage;

        if(healthBar)
        {
            healthBar.SetCurrentHealth(playerHealth);
        }

        if(playerHealth <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        transform.position = latestCheckpoint;
        Debug.Log(transform.position);
    }

    void CheckForCheckpoint()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(10.0f, 10.0f), 0.0f, transform.forward, 100.0f, LayerMask.GetMask("Checkpoint"));
        if(hit)
        {
            SetLatestCheckpoint(hit.transform.position);
            if(Vector3.Distance(hit.transform.position, transform.position) <= 10.0f)
            {
                GetLifeCharges();
            }
        }

    }

    void SetLatestCheckpoint(Vector3 position)
    {
        latestCheckpoint = position;
    }

    void GetLifeCharges()
    {
        lifeCharges = 3;
        if(healConsumables)
        {
            healConsumables.SetAvailableHeals(lifeCharges);
        }
    }

    void UseCharge()
    {
        if(lifeCharges > 0)
        {
            playerHealth = 100.0f;
            if(healthBar)
            {
                healthBar.SetCurrentHealth(playerHealth);
            }

            lifeCharges -= 1;
            if(healConsumables)
            {
                healConsumables.SetAvailableHeals(lifeCharges);
            }
            
        }
    }
}
