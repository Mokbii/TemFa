using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager
{
	public static GameDataManager aInstance
	{
		get
		{
			if (sInstance == null)
				sInstance = new GameDataManager();
			return sInstance;
		}
	}
	public void Init()
	{
		mUnitEnemyList = new List<UnitEnemy>();
	}
	public void OnUpdate()
	{
		if (mUnitHero.vTargetObject == null)
		{

		}
		// mUnitHero.SetTarget();
	}
	public void SetInfo(UnitHero pHero, Map pMap)
	{
		mUnitHero = pHero;
		mMap = pMap;
	}
	public void AddEnemy(UnitEnemy pUnitEnemy)
	{
		pUnitEnemy.Init();
		mUnitEnemyList.Add(pUnitEnemy);
	}
	public void RemoveEnemy(UnitEnemy pUnitEnemy)
	{
		mUnitEnemyList.Remove(pUnitEnemy);
		if (mUnitHero.vTargetObject == pUnitEnemy.aTransform)
		{
			mUnitHero.vTargetObject = null;
		}
	}
	private static GameDataManager sInstance;
	private UnitHero mUnitHero;
	private Map mMap;
	private List<UnitEnemy> mUnitEnemyList;
}
