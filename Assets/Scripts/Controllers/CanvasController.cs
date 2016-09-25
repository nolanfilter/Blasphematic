using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanvasController : MonoBehaviour {

	public bool isTransitioning;

	private Vector2 oldMousePosition;
	private Vector2 oldFingerPosition;

	private float minX;
	private float maxX;

	void Awake()
	{
		float adjustedWidth = Mathf.RoundToInt( Screen.height / 3f * 4f );

		minX = ( Screen.width - adjustedWidth ) * 0.5f;
		maxX = Screen.width - minX;

		isTransitioning = false;
	}

	void Update()
	{
		if( isTransitioning )
			return;

		if( Application.isEditor )
		{
			Vector2 mousePosition = Input.mousePosition;

			if( Input.GetMouseButtonDown( 0 ) )
			{
				if( mousePosition.x < minX || mousePosition.x > maxX || mousePosition.y < 0f || mousePosition.y > Screen.height )
					return;

				OnTouchDown( mousePosition );
				oldMousePosition = mousePosition;
			}

			if( Input.GetMouseButton( 0 ) )
			{
				if( mousePosition.x < minX || mousePosition.x > maxX || mousePosition.y < 0f || mousePosition.y > Screen.height )
					return;

				OnTouchHeld( mousePosition, mousePosition - oldMousePosition );
				oldMousePosition = mousePosition;
			}

			if( Input.GetMouseButtonUp( 0 ) )
			{
				mousePosition = new Vector2( Mathf.Clamp( mousePosition.x, minX, maxX ), Mathf.Clamp( mousePosition.y, 0f, Screen.height ) );

				OnTouchUp( mousePosition );
				oldMousePosition = mousePosition;
			}
		}
		else
		{
			if( Input.touchCount > 0 )
			{
				Vector2 fingerPosition = Input.touches[0].position;

				if( Input.touches[0].phase == TouchPhase.Began )
				{
					if( fingerPosition.x < minX || fingerPosition.x > maxX || fingerPosition.y < 0f || fingerPosition.y > Screen.height )
						return;

					OnTouchDown( fingerPosition );
					oldFingerPosition = fingerPosition;
				}

				if( Input.touches[0].phase == TouchPhase.Stationary || Input.touches[0].phase == TouchPhase.Moved )
				{
					if( fingerPosition.x < minX || fingerPosition.x > maxX || fingerPosition.y < 0f || fingerPosition.y > Screen.height )
						return;

					OnTouchHeld( fingerPosition, fingerPosition - oldFingerPosition );
					oldFingerPosition = fingerPosition;
				}

				if( Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled )
				{
					fingerPosition = new Vector2( Mathf.Clamp( fingerPosition.x, minX, maxX ), Mathf.Clamp( fingerPosition.y, 0f, Screen.height ) );

					OnTouchUp( fingerPosition );
					oldFingerPosition = fingerPosition;
				}
			}
		}
	}
	
	public virtual void OnTouchDown( Vector2 position )
	{

	}

	public virtual void OnTouchHeld( Vector2 position, Vector2 delta )
	{

	}

	public virtual void OnTouchUp( Vector2 position )
	{

	}
}
