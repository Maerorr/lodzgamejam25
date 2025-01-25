using UnityEngine;

public class Enemyrange : MonoBehaviour, EnemyBase
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

    //
    public float timeToRotate;
    float currentTimeToRotate;
    //
    public LayerMask layerMask; // Zmienna, do której przypiszesz warstwy w inspektorze
    bool attackTrigger;
    float currentAttackTrigger;

    public void attack()
    {
       
    }

    public void attackAndMoveBackward()
    {

    }

    public void attackAndMoveForward()
    {
        transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
        if (player.transform.position.x < transform.position.x)
        {
            direction.x = 1.0f;
        }
        else
        {
            direction.x = -1.0f;
        }
    }

    public void moveBackward()
    {
        transform.position = new Vector3(transform.position.x + -direction.x * speed, transform.position.y, transform.position.z);
    }

    public void moveForward()
    {
        transform.position = new Vector3(transform.position.x + direction.x * speed, transform.position.y, transform.position.z);
        if (player.transform.position.x < transform.position.x)
        {
            direction.x = -1.0f;
        }
        else
        {
            direction.x = 1.0f;
        }
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
    }

    // Update is called once per frame
    void Update()
    {

        if (takeDamage)
        {
            takeDamage = false;
            currentLifeTimeToDeath -= Time.deltaTime;
        }
        if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        setCurrentState();
        currentBehaviour.execute(this);
    }

    public void distanceAttack()
    {
        //create ammo
    }

    public void death()
    {

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
                currentBehaviour = new DistanceAttack();
            }
            else if (attackRange < distance)
            {
                currentBehaviour = new AttackInEscape();
            }
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
    }
}
