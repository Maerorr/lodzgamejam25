using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] HealConsumables healConsumables;
    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] float healthDecreaseMultiplier = 1.0f;

    public float playerHealth = 100f;
    
    //public int lifeCharges = 3;
    //private Vector3 latestCheckpoint = new Vector3(0.0f,0.0f,0.0f);
    private Soda soda;
    private PlayerMovement pm;
    public Rigidbody2D rb;
    public float knockbackValue;
    [SerializeField] Vector3 respawnPosition;

    public bool canBeEmitting = false;
    public bool isFlying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soda = GetComponent<Soda>();
        pm = GetComponent<PlayerMovement>();

        /*
        if(healConsumables)
        {
            healConsumables.SetAvailableHeals(lifeCharges);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

        playerHealth -= healthDecreaseMultiplier * Time.deltaTime;
        if(playerHealth <= 0.0f)
        {
            Die();
        }

        if(healthBar)
        {   
            
            healthBar.SetCurrentHealth(Mathf.InverseLerp(0f,100f, playerHealth));
        }

        if(soda.pressure > 0.0f)
        {
            canBeEmitting = true;
        } 
        else 
        {
            canBeEmitting = false;
        }

        //CheckForCheckpoint();

        if (Input.GetMouseButton(0))
        {
            soda.isEmitting = true;
            soda.Emit();
        }

        if (Input.GetMouseButtonUp(0))
        {
            soda.isEmitting = false;
        }

        if(canBeEmitting && soda.isEmitting && !pm.isGrounded)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }

        /*
        if (Input.GetMouseButtonDown(1))
        {
           
            UseCharge();
        }
        */

        if(soda.isKnockback)
        {
            //Vector2 buffer = new Vector2(soda.direction.x, soda.direction.y);
            float value = (soda.direction.y * -1) + 1;
            rb.AddForce(new Vector2(-soda.direction.x * knockbackValue, value * knockbackValue), ForceMode2D.Force);
            soda.isKnockback = false;
        }

    }

    public void DecreaseHealth(float damage)
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
        transform.position = respawnPosition;
        playerHealth = 100.0f;
    }

    /*
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
    */
}
