using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit
{
	public Animator vAnim;
	public MoveComponent vMoveCoponent;
	public bool aIsAlive
	{
		get { return mIsAlive; }
	}
	void Start()
	{
		GameDataManager.aInstance.AddEnemy(this);
	}
	public override void Init()
	{
		base.Init();

		mIsAlive = true;
		if (vAnim != null)
		{
			vAnim.CrossFade("Running@loop", 0.1f, 0, 0.5f);
			//vAnim.Play("Running@loop");
		}

		vMoveCoponent = gameObject.GetComponent<MoveComponent>();
		vMoveCoponent.OnPlay();
	}
	public override void OnMissileCollider(Missile pMissile)
	{
		if (!aIsAlive)
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
