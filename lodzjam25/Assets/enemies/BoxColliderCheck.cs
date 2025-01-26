using UnityEngine;

public class BoxColliderCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	  void OnTriggerEnter(Collider collision)
    {
      
	
	   
    if  (collision.gameObject.layer == LayerMask.NameToLayer("Ground")|| (collision.gameObject.layer == LayerMask.NameToLayer("Scana")))

        {          
			Debug.Log("kurwachujjasny");
          Bullet prefabDestroyer = GetComponentInParent<Bullet>();
        if (prefabDestroyer != null)
			{
				 Debug.Log("zb");
            prefabDestroyer.DestroyMe(); // Niszczy cały prefab
			}
        }
        
  
       
    }
}
