using UnityEngine;

public class level : MonoBehaviour
{
    public Vector3 translationVector = new Vector3(1f, 1f, 0f); // Wektor przesuni�cia
    public float zoomOutDistance = 5f; // O ile oddali� kamer� (dla kamery ortogonalnej zmieniasz orthoSize)
    public float animationDuration = 2f; // Czas trwania animacji w sekundach

    private Vector3 initialPosition;
    private float initialZoom;
    private Camera cam;
    private float elapsedTime = 0f;
    private bool animating = false;

    void Start()
    {
        // Zapisz pocz�tkowe ustawienia kamery
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Ten skrypt musi by� przypisany do obiektu z kamer�!");
            enabled = false;
            return;
        }
        initialPosition = cam.transform.position;
        initialZoom = cam.orthographic ? cam.orthographicSize : cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Rozpocznij animacj� po naci�ni�ciu Spacji
        {
            if (!animating)
            {
                animating = true;
                elapsedTime = 0f;
            }
        }

        if (animating)
        {
            AnimateCamera();
        }
    }

    void AnimateCamera()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / animationDuration;

        if (t <= 0.5f) // Pierwsza po�owa animacji: oddalanie i przesuwanie
        {
            float progress = t * 2f; // Skaluje czas do zakresu [0, 1] w tej fazie
            cam.transform.position = Vector3.Lerp(initialPosition, initialPosition + translationVector, progress);

            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(initialZoom, initialZoom + zoomOutDistance, progress);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(initialZoom, initialZoom + zoomOutDistance, progress);
            }
        }
        else if (t <= 1f) // Druga po�owa animacji: powr�t
        {
            float progress = (t - 0.5f) * 2f; // Skaluje czas do zakresu [0, 1] w tej fazie
            cam.transform.position = Vector3.Lerp(initialPosition + translationVector, initialPosition, progress);

            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(initialZoom + zoomOutDistance, initialZoom, progress);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(initialZoom + zoomOutDistance, initialZoom, progress);
            }
        }
        else
        {
            // Zako�cz animacj�
            animating = false;
            cam.transform.position = initialPosition;
            if (cam.orthographic)
            {
                cam.orthographicSize = initialZoom;
            }
            else
            {
                cam.fieldOfView = initialZoom;
            }
        }
    }
}
