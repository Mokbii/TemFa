using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit
{
	void Start()
	{
		EnemyManager.aInstance.AddEnemy(this);
	}
	public override void Init()
	{
		base.Init();
	}
	public override void Destroy()
	{
	}
}
