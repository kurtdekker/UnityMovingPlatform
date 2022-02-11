using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock2 : MonoBehaviour
{
	[Header ("Velocity Configuration")]
	public float Velocity = 10.0f;
	public float Acceleration = 30.0f;

	[Header ("Moving Points")]
	public Transform[] destination;

	private Rigidbody2D _rb2D;

	private Vector2 currentTargetPosition;
	private int currentIndex;

	void Awake ()
	{
		Debug.Log (destination.Length);

		_rb2D = GetComponent<Rigidbody2D> ();

		currentIndex = -1;
		SelectNextDestination();
	}

	void FixedUpdate ()
	{
		Vector2 position = _rb2D.position;

		Vector2 deltaToTarget = currentTargetPosition - position;

		Vector2 desiredVelocity = deltaToTarget.normalized * Velocity;

		Vector2 velocity = _rb2D.velocity;

		velocity = Vector2.MoveTowards( velocity, desiredVelocity, Acceleration * Time.deltaTime);

		if (desiredVelocity.magnitude * Time.deltaTime >= deltaToTarget.magnitude)
		{
			// we have arrived
			position = currentTargetPosition;
			_rb2D.MovePosition( position);

			velocity = Vector2.zero;

			SelectNextDestination();
		}

		_rb2D.velocity = velocity;
	}

	void SelectNextDestination ()
	{
		Debug.Log ("ISAMCN");
		if (currentIndex < destination.Length - 1)
		{
			currentIndex++;
		}
		else
		{
			currentIndex = 0;
		}
		currentTargetPosition = destination [currentIndex].position;
	}
}
