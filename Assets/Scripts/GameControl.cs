using UnityEngine;
using System.Collections;

public class GameControl
{
	public delegate void OnMovePosition(Vector2 pDeltaPos);
	public delegate void OnMoveStart();
	public delegate void OnMoveEnd();

	public OnMovePosition aOnMovePosition
	{
		set { mOnMovePosition = value;}
		get { return mOnMovePosition; }
	}
	public OnMoveStart aOnMoveStart
	{
		set { mOnMoveStart = value; }
		get { return mOnMoveStart; }
	}
	public OnMoveEnd aOnMoveEnd
	{
		set { mOnMoveEnd = value; }
		get { return mOnMoveEnd; }
	}

	public static GameControl aInstance
	{
		get
		{
			if (sInstance == null)
				sInstance = new GameControl();
			return sInstance;
		}
	}
	public void Init ()
	{
		mIsMouseDown = false;
	}
	public void OnUpdate () 
	{
		_UpdateMouse();
//#if UNITY_EDITOR
//		_UpdateMouse();
//#else
//		_UpdateTouch();
//#endif
	}
	private void _UpdateMouse()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mLastMousePosition = Input.mousePosition;
			mIsMouseDown = true;
			if (aOnMoveStart != null)
				aOnMoveStart();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			mIsMouseDown = false;
			if (aOnMoveEnd != null)
				aOnMoveEnd();
		}
		if (mIsMouseDown)
		{
			Vector3 lMousePosition = Input.mousePosition;
			Vector3 lDeltaPosition = lMousePosition - mLastMousePosition;

			if (aOnMovePosition != null)
				aOnMovePosition(new Vector2(lDeltaPosition.x, lDeltaPosition.y));
		}
	}
	private void _UpdateTouch()
	{
		int nbTouches = Input.touchCount;
		if (nbTouches > 0)
		{
			for (int i = 0; i < nbTouches; i++)
			{
				Touch touch = Input.GetTouch(i);

				TouchPhase phase = touch.phase;

				switch (phase)
				{
					case TouchPhase.Moved:
						Vector2 lMovePosition = touch.position - new Vector2(mLastMousePosition.x, mLastMousePosition.y);
						if (aOnMovePosition != null)
							aOnMovePosition(lMovePosition);
						break;
					case TouchPhase.Began:
						mLastMousePosition = touch.position;
						if (aOnMoveStart != null)
							aOnMoveStart();
						break;
					case TouchPhase.Ended:
						if (aOnMoveEnd != null)
							aOnMoveEnd();
						break;
				}
			}
		}
	}
	private static GameControl sInstance;
	private OnMovePosition mOnMovePosition;
	private OnMoveStart mOnMoveStart;
	private OnMoveEnd mOnMoveEnd;

	// Mouse 처리
	private bool mIsMouseDown;
	private Vector3 mLastMousePosition;
}
