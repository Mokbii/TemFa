using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit
{
	public Animator vAnim;
	void Start()
	{
		GameDataManager.aInstance.AddEnemy(this);
		mIsAlive = true;
		if (vAnim != null)
		{
			vAnim.CrossFade("Running@loop", 0.1f, 0, 0.5f);
			//vAnim.Play("Running@loop");
		}
	}
	public override void Init()
	{
		base.Init();
	}
	private void Update()
	{
		if (!mIsAlive)
			return;

		Transform lTarget = GameDataManager.aInstance.aUnitHero.aTransform;

		Vector3 lToTargetPosition = lTarget.position - aTransform.position;
		lToTargetPosition = lToTargetPosition.normalized;

		Vector3 lNextMove = new Vector3(lToTargetPosition.x * vMaxMoveSpeed.x, lToTargetPosition.x, lToTargetPosition.z * vMaxMoveSpeed.y);
		aTransform.localPosition += (lNextMove * Time.deltaTime);

		float rotationDegree = Mathf.Atan2(lToTargetPosition.x, lToTargetPosition.z) * Mathf.Rad2Deg;
		aTransform.rotation = Quaternion.Euler(0f, rotationDegree, 0f);
	}
	public override void OnMissileCollider(Missile pMissile)
	{
		if (!mIsAlive)
			return;

		vHp -= pMissile.vPower;
		if (vHp <= 0)
		{
			GoDie();
		}
	}
	public override void GoDie()
	{
		GameDataManager.aInstance.RemoveEnemy(this);
		vAnim.CrossFade("GoDown", 0.1f, 0, 0.5f);
		mIsAlive = false;
	}
	private void _OnDestory()
	{
		Destroy(gameObject);
	}
	private void EndGoDown()
	{
		Debug.Log("GoDown");
		_OnDestory();
	}

	private bool mIsAlive;
}
