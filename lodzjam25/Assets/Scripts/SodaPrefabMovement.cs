using UnityEngine;

public class SodaPrefabMovement : MonoBehaviour
{
    public float initialSpeed;
    float velocityY=0;
    float initialYPos;
    float currentYPos;
    public float g=9.81f;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialYPos - 1.0f < currentYPos)
        {
            float posY = transform.position.y - (velocityY * Time.deltaTime + g*(Time.deltaTime*Time.deltaTime)/2.0f);
            transform.position =(new Vector3(transform.position.x + direction.x*(Time.deltaTime * initialSpeed),posY,transform.position.z));
            velocityY += g * Time.deltaTime;
        }
        else {  Object.Destroy(this); }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }
}
