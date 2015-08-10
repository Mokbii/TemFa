using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit
{
	void Start()
	{
		GameDataManager.aInstance.AddEnemy(this);

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
