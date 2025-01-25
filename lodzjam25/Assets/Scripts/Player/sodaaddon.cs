using UnityEngine;

public class sodaaddon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float initialSpeed;
    public float velocityY = 0;
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
        float posY = transform.position.y + ((velocityY * Time.deltaTime + g * (Time.deltaTime * Time.deltaTime) / 2.0f));
        transform.position = (new Vector3(transform.position.x + direction.x * (Time.deltaTime * initialSpeed), posY, transform.position.z));

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) { Destroy(gameObject); }

        //velocityY += g * Time.deltaTime;
        // }
        //   else { Destroy(gameObject); }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }
}
