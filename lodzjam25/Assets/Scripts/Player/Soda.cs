using UnityEngine;

public class Soda : MonoBehaviour
{
	Vector2 previous;
	float pressure;
	float minDist = 150.0f;
	float maxDist = 300.0f;

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

	void Update ()
	{
		// read and age position (differentiate with respect to frame)
		Vector2 current = ReadMouse();
		Vector2 delta = current - previous;
		previous = current;

		// how far was that in pixels?
		float distance = delta.magnitude;

		//Debug.Log(distance);

		// how long in seconds was the last frame?
		float timeDelta = Time.deltaTime;

		// compute!
		float pixelsPerSecond = distance / timeDelta;

		// and report!
		//Debug.Log( System.String.Format( "Speed is {0:000.0} pixels / second.", pixelsPerSecond));
	}
}
