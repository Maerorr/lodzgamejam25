using UnityEngine;

public class SodaDirection : MonoBehaviour
{
    public Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pozycja myszy w uk³adzie ekranu (2D)
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Zamiana pozycji myszy na wspó³rzêdne œwiata
        Camera mainCamera = Camera.main;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.nearClipPlane));

        // Zamiana wspó³rzêdnych na 2D (ignorujemy Z)
        Vector2 mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        Vector2 objectPosition2D = new Vector2(transform.position.x, transform.position.y);

        // Obliczenie wektora kierunkowego 2D
        Vector2 direction2D = (mouseWorldPosition2D - objectPosition2D).normalized;

    }
}
