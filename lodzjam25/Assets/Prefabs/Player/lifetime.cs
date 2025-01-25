using UnityEngine;

public class lifetime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float lifeTime = 0.4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime < 0) { Destroy(gameObject); }
        lifeTime -= Time.deltaTime;
    }
}
