using UnityEngine;

public class Soda : MonoBehaviour
{
	Vector2 previous;
	[SerializeField] float pressure; //0-1
	[SerializeField] float losingPressureInterval = 0.1f;
	[SerializeField] float addedPressureMultiplier = 0.25f;
	float minDist = 150.0f;
	float maxDist = 300.0f;

	float timeAtLastShake;
	float timeSinceLastShake;
	[SerializeField] float timeBetweenLoss = 2.0f;

	Vector2 ReadMouse()
	{
		// this is the ONE place you can change if you wish to get your mouse from somewhere else
		Vector3 rawPosition = Input.mousePosition;

		return new Vector2( rawPosition.x, rawPosition.y);
	}

	void Start()
	{
		previous = ReadMouse();
	}

	public void Emit()
	{
		Debug.Log("Emit");
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

		if(timeSinceLastShake >= timeBetweenLoss)
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
			timeSinceLastShake = 0.0f;
			timeAtLastShake = Time.time;
		}

		// how long in seconds was the last frame?
		float timeDelta = Time.deltaTime;

		// compute!
		float pixelsPerSecond = distance / timeDelta;

		// and report!
		//Debug.Log( System.String.Format( "Speed is {0:000.0} pixels / second.", pixelsPerSecond));

		if(pressure > 1.0f)
		{
			Explode();
		}

		//Debug.Log(pressure);
	}
}
