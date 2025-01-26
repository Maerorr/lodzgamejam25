using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] public Vector3 respawnPosition;

    public bool canBeEmitting = false;
    public bool isFlying = false;

    public UnityEvent onDamage = new UnityEvent();

    public EventReference damageSound;

    bool wasEmittingLastFrame = false;
    public EventReference spraySound;
    EventInstance sprayInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soda = GetComponent<Soda>();
        pm = GetComponent<PlayerMovement>();
        sprayInstance = RuntimeManager.CreateInstance(spraySound);
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
        if (playerHealth <= 0.0f)
        {
            Die();
        }

        if (healthBar)
        {

            healthBar.SetCurrentHealth(Mathf.InverseLerp(0f, 100f, playerHealth));
        }

        if (soda.pressure > 0.0f)
        {
            canBeEmitting = true;
        }
        else
        {
            canBeEmitting = false;
        }

        //CheckForCheckpoint();

        if (soda.pressure == 0.0f)
        {
            soda.isEmitting = false;
            if (wasEmittingLastFrame)
            {
                sprayInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                wasEmittingLastFrame = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!wasEmittingLastFrame)
            {
                if (soda.pressure > 0.0f)
                {
                    sprayInstance.start();
                }
                wasEmittingLastFrame = true;
            }
            soda.isEmitting = true;
            soda.Emit();
        }



        if (Input.GetMouseButtonUp(0))
        {
            soda.isEmitting = false;
            if (wasEmittingLastFrame)
            {
                sprayInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                wasEmittingLastFrame = false;
            }
        }

        if (canBeEmitting && soda.isEmitting && !pm.isGrounded)
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

        if (soda.isKnockback)
        {
            //Vector2 buffer = new Vector2(soda.direction.x, soda.direction.y);
            Vector3 direction = soda.direction.normalized;
            float value = (direction.y * -1) + 1;
            //Debug.Log(soda.direction);
            if (pm.isGrounded)
            {
                rb.AddForce(new Vector2(-soda.direction.x * (knockbackValue * 0.1f), value * knockbackValue), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(-soda.direction.x * knockbackValue, value * knockbackValue), ForceMode2D.Force);
            }

            soda.isKnockback = false;
        }

    }

    public void Reset()
    {
        playerHealth = 100.0f;
        transform.position = respawnPosition;
    }

    public void DecreaseHealth(float damage)
    {
        playerHealth -= damage;
        RuntimeManager.PlayOneShot(damageSound);
        onDamage.Invoke();

        if (healthBar)
        {
            healthBar.SetCurrentHealth(playerHealth);
        }

        if (playerHealth <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        transform.position = respawnPosition;
        //Camera.main.transform.position = Camera.main.GetComponent<level>().cameraPositions[0];
        //Camera.main.GetComponent<level>().currentPositionNr = 0;
        Camera.main.GetComponent<level>().DestroyEnemies(true);
        soda.pressure = 1.0f;
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
