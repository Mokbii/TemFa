using UnityEngine;
using System.Collections;

public class UnitHero : Unit
{
	public Animator vAnim;

	public override void Init()
	{
		GameControl.aInstance.aOnMovePosition += _OnMovePosition;
		GameControl.aInstance.aOnMoveStart += _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd += _OnMoveEnd;
		base.Init();
	}
	public override void Destroy()
	{
		GameControl.aInstance.aOnMovePosition -= _OnMovePosition;
		GameControl.aInstance.aOnMoveStart -= _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd -= _OnMoveEnd;
		base.Destroy();
	}
	public void Update()
	{
		//Animation lCurrentAnimation = vAnim.GetComponent<Animation>();
		//if(lCurrentAnimation != null)
		//	Debug.Log(lCurrentAnimation.clip.name);
	}
	private void _OnMovePosition(Vector2 pDeltaPos)
	{
		float lMoveSpeedX = pDeltaPos.x * Time.deltaTime;
		float lMoveSpeedY = pDeltaPos.y * Time.deltaTime;

		if (lMoveSpeedX > vMaxMoveSpeed.x)
			lMoveSpeedX = vMaxMoveSpeed.x;
		else if (lMoveSpeedX <= -1 * vMaxMoveSpeed.x)
			lMoveSpeedX = -1 * vMaxMoveSpeed.x;

		if (lMoveSpeedY > vMaxMoveSpeed.y)
			lMoveSpeedY = vMaxMoveSpeed.y;
		else if (lMoveSpeedY <= -1 * vMaxMoveSpeed.y)
			lMoveSpeedY = -1 * vMaxMoveSpeed.y;

		mMoveSpeed = new Vector2(lMoveSpeedX, lMoveSpeedY);

		float lMoveX = mTransform.position.x + mMoveSpeed.x;
		float lMoveY = mTransform.position.z + mMoveSpeed.y;

		// X타일 검사
		MapTile lCheckTileX = mMapInfo.GetMapTile(new Vector3(lMoveX, 0, mTransform.position.z));
		if (lCheckTileX == null || lCheckTileX.aTileType != Map.MapTileType.Way)
			lMoveX = mTransform.position.x;

		// Y타일 검사
		MapTile lCheckTileY = mMapInfo.GetMapTile(new Vector3(mTransform.position.x, 0, lMoveY));
		if (lCheckTileY == null || lCheckTileY.aTileType != Map.MapTileType.Way)
			lMoveY = mTransform.position.z;

		mTransform.position = new Vector3(lMoveX,
										  mTransform.position.y,
										  lMoveY);
	}

	private void _OnMoveStart()
	{
		mMoveSpeed = new Vector2(0, 0);
		vAnim.CrossFade("Running@loop", 0.1f, 0, 0.5f);
	}
	private void _OnMoveEnd()
	{
		mMoveSpeed = new Vector2(0, 0);
		vAnim.CrossFade("Standing@loop", 0.1f, 0, 0.5f);
	}
}
