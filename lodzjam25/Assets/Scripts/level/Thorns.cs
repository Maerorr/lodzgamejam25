using UnityEngine;

public class Thorns : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("also");
            collision.gameObject.GetComponent<Player>().DecreaseHealth(100.0f);
        } 
    }
}
