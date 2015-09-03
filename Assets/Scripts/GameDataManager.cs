using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 맵이 바뀔때마다 GameDataManager에서의 셋팅도 변경됩니다
/// </summary>
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
	/// <summary>
	/// 속성
	/// </summary>
	public List<UnitEnemy> aUnitEnemyList
	{
		get { return mUnitEnemyList; }
	}
	public UnitHero aUnitHero
	{
		get { return mUnitHero; }
	}
	public Map aMap
	{
		get { return mMap; }
	}
	public bool aIsActiveGame
	{
		get { return mIsActiveGame; }
	}

	public void Init()
	{
		mUnitEnemyList = new List<UnitEnemy>();
		mIsActiveGame = true;
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
	public void OnEndGame()
	{
		mIsActiveGame = false;
	}
	private static GameDataManager sInstance;
	private UnitHero mUnitHero;
	private Map mMap;
	private List<UnitEnemy> mUnitEnemyList;
	private bool mIsActiveGame; 
}
