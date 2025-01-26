using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyMelee : MonoBehaviour, EnemyBase
{

    bool sawPlayer = false;

    Behaviour currentBehaviour;
    bool takeDamage = false;
    public float basicLifeTime;
    public float lifeTimeToDeath;
    public float currentLifeTimeToDeath;
    public float attackRange;
    public float attackSpeed;
    public float speed;
    public float damage;
    public float attackRangeOffset;
    public float attentionDistace;
    public Player player;
    public Vector2 direction;
    float backAttentionMultiply = 0.2f;
    public bool isTarczaSzmato = true;
    Vector3 positionInit;
    public float patrolDistance;
    Vector2 patrolRange;
    Vector3 initialPositionDebil;
    //
    public float timeToRotate;
    float currentTimeToRotate;
    //
    public LayerMask layerMask; // Zmienna, do kt�rej przypiszesz warstwy w inspektorze
    bool attackTrigger = false;
    float currentAttackTrigger;

    public UnityEvent onDeath = new UnityEvent();
    float damageTime = 0.0f;
    public void attack()
    {
        //Attack
        if (!attackTrigger)
        {
           
            attackTrigger = true;
            currentAttackTrigger = attackSpeed;
        }

    }

    public void attackAndMoveBackward()
    {

    }

    public void attackAndMoveForward()
    {
        transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
        if (player.transform.position.x < transform.position.x)
        {
            if (!attackTrigger)
            {
                bool attackTrigger = true;
                currentAttackTrigger = attackSpeed;
            }
            direction.x = -1.0f;
        }
        else
        {
            if (!attackTrigger)
            {
                bool attackTrigger = true;
                currentAttackTrigger = attackSpeed;
            }
            direction.x = 1.0f;
        }
    }

    public void moveBackward()
    {
        transform.position = new Vector3(transform.position.x + -direction.x * speed, transform.position.y, transform.position.z);
    }

    public void moveForward()
    {
		Vector3 newPos = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
		Collider2D hitCollider = Physics2D.OverlapCircle(new Vector2(newPos.x + direction.x * 0.5f, newPos.y), attackRange, LayerMask.GetMask("Scana"));
        
        if(hitCollider == null){
		transform.position =newPos;
        if (player.transform.position.x < transform.position.x)
        {
            direction.x = -1.0f;
        }
        else
        {
            direction.x = 1.0f;
        }}
    }

    public void movePatrol()
    {
        if (direction.x > 0)
        {
            if (transform.position.x < patrolRange.y)
            {
                transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
            }
            else
            {
                currentTimeToRotate -= Time.deltaTime;
                if (currentTimeToRotate < 0) { direction.x *= -1; currentTimeToRotate = timeToRotate; }
            }
        }
        else
        {
            if (transform.position.x > patrolRange.x)
            {
                transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
            }
            else
            {
                currentTimeToRotate -= Time.deltaTime;
                if (currentTimeToRotate < 0) { direction.x *= -1; currentTimeToRotate = timeToRotate; }
            }
        }

        float distance = Vector3.Magnitude(player.transform.position - transform.position);

        if (direction.x * attentionDistace > distance || -direction.x * attentionDistace * backAttentionMultiply > distance)
        {

            sawPlayer = true;
            if (transform.position.x > player.transform.position.x)
            {
                direction.x = -1.0f;
            }
            else
            {
                direction.x = 1.0f;
            }

        }
    }



    public void stay()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionInit = transform.position;
        patrolRange = new Vector2(positionInit.x - patrolDistance, positionInit.x + patrolDistance);
        direction = new Vector2(1.0f, 0.0f);
        currentTimeToRotate = timeToRotate;
        currentLifeTimeToDeath = lifeTimeToDeath;
        this.currentBehaviour = new Stay();
        initialPositionDebil = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        damageTime-=Time.deltaTime;
        currentAttackTrigger -= Time.deltaTime;
        
        if (currentAttackTrigger < 0 && attackTrigger)
        {

            attackTrigger = false;
            Collider2D hitCollider = Physics2D.OverlapCircle(new Vector2(this.transform.position.x + direction.x * 0.5f, this.transform.position.y), attackRange, LayerMask.GetMask("Player"));
            if (hitCollider != null) player.DecreaseHealth(damage);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 rotation = transform.eulerAngles;
        rotation.z = 0.0f;
        rotation.y = 0.0f;
        rotation.x = 0.0f;
        transform.eulerAngles = rotation;
        if (takeDamage)
        {
            takeDamage = false;
            currentLifeTimeToDeath -= Time.deltaTime;
            if(damageTime < 0 ){
                damageTime = 2.0f;
              //  GetComponent<Punche>().SpawnBillboard(transform.position);
            }
        }
        //if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position = new Vector3(transform.position.x, initialPositionDebil.y, transform.position.z);
        
        setCurrentState();
        currentBehaviour.execute(this);
    }

    public void distanceAttack()
    {
        //create ammo
    }

    public void death()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }

    public void setCurrentState()
    {
        if (currentLifeTimeToDeath < 0)
        {

            currentBehaviour = new Death();
            return;
        }
        if (sawPlayer)
        {

            Vector3 bufor = new Vector3(this.transform.position.x + direction.x * attackRange, this.transform.position.y, this.transform.position.z);

            float distance = Vector3.Magnitude(player.transform.position - bufor);
            if (attackRange > distance)
            {

                currentBehaviour = new Attack();
            }
           // else if (attackRange + attackRangeOffset > distance)
           // {
           //     currentBehaviour = new AttackInForwardMovement();
           // }
            else
            {
                currentBehaviour = new MoveToPlayer();
            }

        }
        else
        {
            currentBehaviour = new Patrol();

        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {

            takeDamage = true;
        }

    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector2(this.transform.position.x + direction.x * 0.8f, this.transform.position.y), attackRange);
        /*
         Gizmos.color = Color.blue;
         Gizmos.DrawSphere(this.transform.position + translateSphereOnHight, 2.0f);

         Gizmos.color = Color.magenta;
         Gizmos.DrawSphere(this.transform.position + new Vector3(0, -0.85f, 0), 0.1f);

         Gizmos.color = Color.gray;
         Gizmos.DrawSphere(this.transform.position + new Vector3(translateAttackCircle.x, translateAttackCircle.y, 0), 0.3f);*/
    }
}




//Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y + (-0.80f)), 0.1f, groundLayerMask);