using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHealth = 100f;
    public int lifeCharges = 3;
    private Vector3 latestCheckpoint = new Vector3(0.0f,0.0f,0.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCheckpoint();
    }

    void DecreaseHealth(float damage)
    {
        playerHealth -= damage;
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
                Debug.Log("here");
                GetLifeCharges();
            }
        }

    }

    void OnDrawGizmos()
 {
      // Draw a yellow sphere at the transform's position
      //Gizmos.color = Color.yellow;
      //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.transform.position.y), new Vector3(1,1,1));
     /*
      Gizmos.color = Color.blue;
      Gizmos.DrawSphere(this.transform.position + translateSphereOnHight, 2.0f);

      Gizmos.color = Color.magenta;
      Gizmos.DrawSphere(this.transform.position + new Vector3(0, -0.85f, 0), 0.1f);

      Gizmos.color = Color.gray;
      Gizmos.DrawSphere(this.transform.position + new Vector3(translateAttackCircle.x, translateAttackCircle.y, 0), 0.3f);*/
 }

    void SetLatestCheckpoint(Vector3 position)
    {
        latestCheckpoint = position;
    }

    void GetLifeCharges()
    {
        lifeCharges = 3;
    }

    void UseCharge()
    {
        if(lifeCharges > 0)
        {
            playerHealth = 100.0f;
            lifeCharges -= 1;
        }
    }
}
