using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager
{
	public static EnemyManager aInstance
	{
		get
		{
			if (sInstance == null)
				sInstance = new EnemyManager();
			return sInstance;
		}
	}
	public int aEnemyCount
	{
		get { return mUnitEnemyList.Count; }
	}
	public void Init()
	{
		mUnitEnemyList = new List<UnitEnemy>();
	}

	public void AddEnemy(UnitEnemy pUnitEnemy)
	{
		pUnitEnemy.Init();
		mUnitEnemyList.Add(pUnitEnemy);
	}
	private static EnemyManager sInstance;
	private List<UnitEnemy> mUnitEnemyList;
}
