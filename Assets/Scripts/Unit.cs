using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	public Vector2 vMaxMoveSpeed = new Vector2(1.0f, 1.0f);
	void Start () 
	{
		GameControl.aInstance.aOnMovePosition += _OnMovePosition;
		GameControl.aInstance.aOnMoveStart += _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd += _OnMoveEnd;
		mTransform = transform;
	}
	void OnDestroy()
	{
		GameControl.aInstance.aOnMovePosition -= _OnMovePosition;
		GameControl.aInstance.aOnMoveStart -= _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd -= _OnMoveEnd;
	}

	private void _OnMovePosition(Vector2 pDeltaPos)
	{
		float lMoveSpeedX = pDeltaPos.x * Time.deltaTime;
		float lMoveSpeedY = pDeltaPos.y * Time.deltaTime;

		if (lMoveSpeedX > vMaxMoveSpeed.x)
			lMoveSpeedX = vMaxMoveSpeed.x;
		else if (lMoveSpeedX <= -1 * vMaxMoveSpeed.x)
			lMoveSpeedX = -1 * vMaxMoveSpeed.x;

		if(lMoveSpeedY > vMaxMoveSpeed.y)
			lMoveSpeedY = vMaxMoveSpeed.y;
		else if(lMoveSpeedY <= -1 * vMaxMoveSpeed.y)
			lMoveSpeedY = -1 * vMaxMoveSpeed.y;

		mMoveSpeed = new Vector2(lMoveSpeedX, lMoveSpeedY);
		Debug.Log("mMoveSpeed - " + mMoveSpeed);
		mTransform.localPosition = new Vector3(mTransform.localPosition.x + mMoveSpeed.x,
											   mTransform.localPosition.y,
											   mTransform.localPosition.z + mMoveSpeed.y);
	}

	private void _OnMoveStart()
	{
		mMoveSpeed = new Vector2(0, 0);
	}
	private void _OnMoveEnd()
	{
		mMoveSpeed = new Vector2(0, 0);
	}

	private Transform mTransform;
	private Vector2 mMoveSpeed;
}
