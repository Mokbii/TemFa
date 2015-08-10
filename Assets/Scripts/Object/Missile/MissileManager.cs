using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum MissileType
{
	Direct,
	Slow,
	Count
}
public class MissileManager
{
	public readonly String[] MissileTypePath = {
		"Prefabs/Missile/Direct Missile", // Direct
		"Prefabs/Missile/Slow Missile", // Slow
	};

	public static MissileManager aInstance
	{
		get
		{
			if (sInstance == null)
				sInstance = new MissileManager();
			return sInstance;
		}
	}

	public void Init()
	{
		mMissilePool = new Dictionary<MissileType, Queue<Missile>>();
	}
	public void AddMissile(Unit pAttackUnit, MissileType pType, Vector3 pStartPos, Vector3 pMissileDirect)
	{
		Missile lMissile = _CreateMissile(pType, pStartPos);
		if (lMissile != null)
			lMissile.GoShot(pAttackUnit, pStartPos, pMissileDirect);
	}
	public void RemoveMissile(Missile pMissile)
	{
		if (!mMissilePool.ContainsKey(pMissile.vType))
		{
			mMissilePool.Add(pMissile.vType, new Queue<Missile>());
		}
		mMissilePool[pMissile.vType].Enqueue(pMissile);
	}
	private Missile _CreateMissile(MissileType pType, Vector3 pStartPos)
	{
		Missile lResultMissile = _FindMissileFromPool(pType);
		if (lResultMissile == null)
		{
			GameObject lMissilePrefab = null;
			GameObject lMissileObject = null;

			lMissilePrefab = (GameObject)Resources.Load(MissileTypePath[(int)pType]);
			lMissileObject = (GameObject)GameObject.Instantiate(lMissilePrefab, pStartPos, Quaternion.identity);

			lResultMissile = lMissileObject.GetComponent<Missile>();
			lResultMissile.Init(_CreateMissileGuid());
		}
		return lResultMissile;
	}
	private Missile _FindMissileFromPool(MissileType pType)
	{
		if (!mMissilePool.ContainsKey(pType))
			return null;
		if (mMissilePool[pType].Count <= 0)
			return null;

		Missile lResultMissile = mMissilePool[pType].Dequeue();
		lResultMissile.gameObject.SetActive(true);
		return lResultMissile;
	}
	private int _CreateMissileGuid()
	{
		mCurrentCreateGuid++;
		return mCurrentCreateGuid;
	}
	private static MissileManager sInstance;
	private int mCurrentCreateGuid = 0;

	private Dictionary<MissileType, Queue<Missile>> mMissilePool;
}
