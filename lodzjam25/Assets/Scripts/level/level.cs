using UnityEngine;
using System.Collections.Generic;

public class level : MonoBehaviour
{
    public Vector3 translationVector = new Vector3(1f, 1f, 0f); // Wektor przesuni�cia
    public List<Vector3> cameraPositions = new List<Vector3>();
    public List<Vector3> playerPositions = new List<Vector3>();
    [SerializeField] Player player;
    public float zoomOutDistance = 5f; // O ile oddali� kamer� (dla kamery ortogonalnej zmieniasz orthoSize)
    public float animationDuration = 2f; // Czas trwania animacji w sekundach

    public int currentPositionNr = 0;
    private Vector3 initialPosition;
    private float initialZoom;
    private Camera cam;
    private float elapsedTime = 0f;
    private bool animating = false;

    public List<Transform> level1MeleeEnemies = new List<Transform>();
    public List<Transform> level1RangedEnemies = new List<Transform>();

    public List<Transform> level2MeleeEnemies = new List<Transform>();
    public List<Transform> level2RangedEnemies = new List<Transform>();

    public List<Transform> level3MeleeEnemies = new List<Transform>();
    public List<Transform> level3RangedEnemies = new List<Transform>();

    public List<Transform> level4MeleeEnemies = new List<Transform>();
    public List<Transform> level4RangedEnemies = new List<Transform>();

    public List<Transform> level5MeleeEnemies = new List<Transform>();
    public List<Transform> level5RangedEnemies = new List<Transform>();

    public List<Transform> level6MeleeEnemies = new List<Transform>();
    public List<Transform> level6RangedEnemies = new List<Transform>();

    public List<Transform> level7MeleeEnemies = new List<Transform>();
    public List<Transform> level7RangedEnemies = new List<Transform>();

    public List<Transform> level8MeleeEnemies = new List<Transform>();
    public List<Transform> level8RangedEnemies = new List<Transform>();

    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;

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
        player.respawnPosition = playerPositions[0];
        initialZoom = cam.orthographic ? cam.orthographicSize : cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Rozpocznij animacj� po naci�ni�ciu Spacji
        {
            if (!animating)
            {
                initialPosition = cam.transform.position;
                if (cameraPositions.Count > currentPositionNr + 1)
                {
                    currentPositionNr += 1;
                }
                translationVector = cameraPositions[currentPositionNr] - initialPosition;

                animating = true;
                elapsedTime = 0f;
            }
        }

        if (animating)
        {
            if (cameraPositions.Count > currentPositionNr)
            {
                AnimateCamera();
            }
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
            cam.transform.position = Vector3.Lerp(initialPosition + translationVector, initialPosition + translationVector, progress);

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
            player.transform.position = playerPositions[currentPositionNr];
            animating = false;
            cam.transform.position = initialPosition + translationVector;
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
