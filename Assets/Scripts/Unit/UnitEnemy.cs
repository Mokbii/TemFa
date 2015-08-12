using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit
{
	public Animator vAnim;
	void Start()
	{
		GameDataManager.aInstance.AddEnemy(this);
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
	public override void OnMissileCollider(Missile pMissile)
	{
		vHp -= pMissile.vPower;
		if (vHp <= 0)
			_OnDestory();
	}
	private void _OnDestory()
	{
		GameDataManager.aInstance.RemoveEnemy(this);
		Destroy(gameObject);
	}
}
