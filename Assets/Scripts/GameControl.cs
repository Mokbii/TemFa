/// <summary>
/// 조작과 관련된 내용을 관리하는 클래스
/// 모든 조작은 이곳에서 처리됨. [7/30/2015 khsong33]
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}

	// 조작과 관련된 처리
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

	private static GameControl sInstance;
	private OnMovePosition mOnMovePosition;
	private OnMoveStart mOnMoveStart;
	private OnMoveEnd mOnMoveEnd;

	// Mouse 처리
	private bool mIsMouseDown;
	private Vector3 mLastMousePosition;
}
