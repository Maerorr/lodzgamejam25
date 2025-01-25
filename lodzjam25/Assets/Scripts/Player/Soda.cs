using UnityEngine;

public class Soda : MonoBehaviour
{
	Vector2 previous;
	[SerializeField] TrzepoMeter trzepoMeter;
	[SerializeField] float pressure; //0-1
	[SerializeField] float losingPressureInterval = 0.005f;
	[SerializeField] float timeBetweenLoss = 0.1f;
	[SerializeField] float timeBeforeFirstLoss = 2.0f;
	[SerializeField] float addedPressureMultiplier = 0.01f;
	float minDist = 150.0f;
	float maxDist = 300.0f;

	float timeAtLastShake;
	float timeSinceLastShake;

	float timeAtPreviousLoss;
	float timeSincePreviousLoss;

	bool isPressureFalling = false;

    float currentSpawnTimer;
    public float spawnTimer;
    public GameObject prefab;

    Vector2 ReadMouse()
	{
		// this is the ONE place you can change if you wish to get your mouse from somewhere else
		Vector3 rawPosition = Input.mousePosition;

		return new Vector2( rawPosition.x, rawPosition.y);
	}

	void Start()
	{
        currentSpawnTimer = spawnTimer;
        previous = ReadMouse();
		timeAtPreviousLoss = Time.time;
	}

    public void Emit()
    {
        if (currentSpawnTimer < 0)
        {
            // Pozycja myszy w uk�adzie ekranu
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Zamiana pozycji myszy na wsp�rz�dne �wiata (tylko dla X i Y)
            Camera mainCamera = Camera.main;
			Vector3 mouseWorldPosition = new Vector2(mouseScreenPosition.x, mouseScreenPosition.y);
			mouseWorldPosition.z = 0;
            // Pozycja obiektu w �wiecie (tylko dla X i Y)
            Vector3 objectPosition = mainCamera.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, 0)); 

            // Obliczenie kierunku (z obiektu do myszy, tylko w p�aszczy�nie X i Y)
            Vector3 direction = (mouseWorldPosition - objectPosition);
            direction.z = 0; // Upewniamy si�, �e ignorujemy wsp�rz�dn� Z
            direction.Normalize();

            // Obliczenie k�ta obrotu wzgl�dem osi Z
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Instancjonowanie prefaba z rotacj� zgodn� z k�tem
            GameObject instance = Instantiate(
                prefab,
                new Vector3(transform.position.x, transform.position.y - 0.1f, 0), // Pozycja startowa w p�aszczy�nie X, Y
                Quaternion.Euler(0, 0, angle) // Rotacja tylko wok� osi Z
            );

            // Przekazanie kierunku do prefab movement (tylko X i Y)
            instance.GetComponent<SodaPrefabMovement>().setDirection(new Vector3(direction.x, direction.y, 0));

            // Resetowanie timera
            currentSpawnTimer = spawnTimer;
            
        }

        // Odliczanie czasu do nast�pnego emitowania
        currentSpawnTimer -= Time.deltaTime;
    }



    void Explode()
	{
		Debug.Log("Exploded! Too much pressure!");
	}

	void Update ()
	{
		// read and age position (differentiate with respect to frame)
		Vector2 current = ReadMouse();
		Vector2 delta = current - previous;
		previous = current;

		// how far was that in pixels?
		float distance = delta.magnitude;


		if(distance > maxDist)
		{
			distance = maxDist;
		}

		if(distance < minDist)
		{
			distance = 0.0f;
			timeSinceLastShake = Time.time - timeAtLastShake;
		}
		else
		{
			timeAtLastShake = Time.time;
			float value = Mathf.InverseLerp(minDist, maxDist, distance);
			pressure += addedPressureMultiplier * value;
			if(pressure > 1.0f)
			{
				pressure = 1.0f;
			}
		}

		//check if some time since last shake passed
		if(timeSinceLastShake >= timeBeforeFirstLoss)
		{
			isPressureFalling = true;
		}
		else
		{
			isPressureFalling = false;
		}


		timeSincePreviousLoss = Time.time - timeAtPreviousLoss;
		if(timeSincePreviousLoss >= timeBetweenLoss && isPressureFalling == true)
		{

			if(pressure >= losingPressureInterval)
			{
				pressure -= losingPressureInterval;
			}
			if(pressure < 0.0f)
			{
				pressure = 0.0f;
			}

			//zerujemy zeby znowu minely 2 sekundy przed kolejnym spadkiem pressure
			timeSincePreviousLoss = 0.0f;
			timeAtPreviousLoss = Time.time;
		}

		// how long in seconds was the last frame?
		float timeDelta = Time.deltaTime;

		// compute!
		float pixelsPerSecond = distance / timeDelta;

		if(trzepoMeter)
		{
			trzepoMeter.SetPressure(pressure);
		}
		// and report!
		//Debug.Log( System.String.Format( "Speed is {0:000.0} pixels / second.", pixelsPerSecond));

		if(pressure > 1.0f)
		{
			Explode();
		}

		//Debug.Log(pressure);
	}
}
