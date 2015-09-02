using UnityEngine;
using System.Collections;

public class UnitHero : Unit
{
	public float vWeaponDelay = 1.0f;
	public MissileType vMissileType = MissileType.Direct;
	public Transform vModelTransform;
	public Transform vWeaponTransform;
	public Animator vAnim;
	public Rigidbody vRigidBody;

	public Transform vTargetObject;

	public override void Init()
	{
		base.Init();

		GameControl.aInstance.aOnMovePosition += _OnMovePosition;
		GameControl.aInstance.aOnMoveStart += _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd += _OnMoveEnd;
		GameControl.aInstance.aOnPickObject += _OnPickObject;
		vUnitColliderInfo.aOnColliderDelegate += _OnCollider;

		vRigidBody = gameObject.GetComponent<Rigidbody>();

		StartCoroutine(_ShotMissile());
		StartCoroutine(_FindTargetRot());
	}
	public void SetTarget(UnitEnemy pEnemy)
	{
		vTargetObject = pEnemy.aTransform;
	}
	public override void GoDie()
	{
		base.GoDie();
		vAnim.CrossFade("GoDown", 0.2f, 0, 0.5f);
		GameDataManager.aInstance.OnEndGame();
	}
	private IEnumerator _FindTargetRot()
	{
		while (true)
		{
			yield return null;

			if (vTargetObject == null)
				continue;

			Vector3 lDirectPos = vTargetObject.position - aTransform.position;

			mTargetDirectY = Mathf.Atan2(lDirectPos.x, lDirectPos.z) * Mathf.Rad2Deg;
			float mAttackDirectY = 0.0f;
			if (mTargetDirectY > -45.0f && mTargetDirectY <= 45.0f)
				mAttackDirectY = 0.0f;
			else if (mTargetDirectY > 45.0f && mTargetDirectY <= 135.0f)
				mAttackDirectY = 90.0f;
			else if ((mTargetDirectY > 135.0f && mTargetDirectY <= 180.0f) ||
					 (mTargetDirectY > -180.0f && mTargetDirectY <= -135.0f))
				mAttackDirectY = 180.0f;
			else
				mAttackDirectY = -90.0f;

			aTransform.rotation = Quaternion.Euler(new Vector3(0, mAttackDirectY, 0));
		}
	}
	private IEnumerator _ShotMissile()
	{
		while (true)
		{
			if (!IsAvailableShot())
			{
				yield return null;
				continue;
			}
			yield return new WaitForSeconds(vWeaponDelay);
			Vector3 lDirect = new Vector3(vWeaponTransform.position.x - vModelTransform.position.x,
											0,
											vWeaponTransform.position.z - vModelTransform.position.z);
			lDirect = lDirect.normalized;
			MissileManager.aInstance.AddMissile(this, vMissileType, vWeaponTransform.position, lDirect);
		}
	}
	private void _OnDestroy()
	{
		GameControl.aInstance.aOnMovePosition -= _OnMovePosition;
		GameControl.aInstance.aOnMoveStart -= _OnMoveStart;
		GameControl.aInstance.aOnMoveEnd -= _OnMoveEnd; 
	}
	private void _OnMovePosition(Vector2 pDeltaPos)
	{
		if (mIsStiff)
			return;

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

		if (vRigidBody != null)
		{
			vRigidBody.transform.position = new Vector3(lMoveX,
											  mTransform.position.y,
											  lMoveY);
		}
		else
		{
			mTransform.position = new Vector3(lMoveX,
											  mTransform.position.y,
											  lMoveY);
		}
	}

	private void _OnMoveStart()
	{
		if (mIsStiff)
			return;

		mMoveSpeed = new Vector2(0, 0);
		vAnim.CrossFade("Running@loop", 0.1f, 0, 0.5f);
	}
	private void _OnMoveEnd()
	{
		if (mIsStiff)
			return;

		mMoveSpeed = new Vector2(0, 0);
		vAnim.CrossFade("Standing@loop", 0.1f, 0, 0.5f);
	}
	private void _OnPickObject(ColliderInfo pColliderInfo)
	{
		if(pColliderInfo.vType == ColliderType.Unit &&
			pColliderInfo.vValue == 1)
		{
			vTargetObject = pColliderInfo.transform;
		}
	}
	private void _OnCollider(ColliderInfo pColliderInfo)
	{
		bool lIsDamaged = false;
		if (pColliderInfo.vType == ColliderType.Missile && pColliderInfo.vValue != vUnitTeamId)
		{
			Missile lMissile = pColliderInfo.GetComponent<Missile>();
			vHp -= lMissile.vPower;
			lIsDamaged = true;
		}
		else if (pColliderInfo.vType == ColliderType.Unit && pColliderInfo.vValue != vUnitTeamId)
		{
			vHp -= 1;
			lIsDamaged = true;
		}
		// HP 처리
		if (lIsDamaged)
		{
			if (vHp <= 0)
			{
				mIsStiff = true;
				GoDie();
			}
			else
			{
				mIsStiff = true;
				vAnim.CrossFade("GoDown", 0.1f, 0, 0.5f);
			}
		}
	}
	private void EndGoDown()
	{
		if (!GameDataManager.aInstance.aIsActiveGame)
		{
			vAnim.speed = 0.0f;
			Debug.Log("Game Ending");
		}
	}
	// 경직 종료
	private void OnDownToUp()
	{
		mIsStiff = false;
		vAnim.CrossFade("Standing@loop", 0.1f, 0, 0.5f);
	}
	/// <summary>
	/// 미사일 발사가 안되는 경우 확인
	/// </summary>
	private bool IsAvailableShot()
	{
		if (GameDataManager.aInstance.aUnitEnemyList.Count <= 0)
			return false;
		if (mIsStiff)
			return false;

		return true;
	}
	private bool mIsStiff;
	private float mTargetDirectY;
}
