using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class MissileManager 
{
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
        mMissilePool = new Dictionary<Missile.Type, Queue<Missile>>();
	}
	public void AddMissile(Unit pAttackUnit, Missile.Type pType, Vector3 pStartPos, Vector3 pMissileDirect)
	{
		GameObject lMissilePrefab = null;
		GameObject lMissileObject = null;
		if (pType == Missile.Type.Direct)
		{
			lMissilePrefab = (GameObject)Resources.Load("Prefabs/Missile/Direct Missile");
			lMissileObject = (GameObject)GameObject.Instantiate(lMissilePrefab, pStartPos, Quaternion.identity);

			DirectMissile lMissile = lMissileObject.GetComponent<DirectMissile>();
            lMissile.Init(_CreateMissileGuid());
			lMissile.GoShot(pAttackUnit, pStartPos, pMissileDirect);
		}
	}
    public void RemoveMissile(Missile pMissile)
    {
        if(!mMissilePool.ContainsKey(pMissile.vType))
        {
            mMissilePool.Add(pMissile.vType, new Queue<Missile>());
        }
        if(mMissilePool[pMissile.vType].Count <= 0)
        {
            mMissilePool[pMissile.vType].Enqueue(pMissile);
        }
    }
    private Missile _CreateMissile(Missile.Type pType)
    {
		return null;
		//if (pType == Missile.Type.Direct)
		//{

		//}
		//return null;
    }
    private Missile _FindMissileFromPool(Missile.Type pType)
    {
        if (!mMissilePool.ContainsKey(pType))
            return null;

        return mMissilePool[pType].Dequeue();
    }
    private int _CreateMissileGuid()
    {
        mCurrentCreateGuid++;
        return mCurrentCreateGuid;
    }
	private static MissileManager sInstance;
    private int mCurrentCreateGuid = 0;

    private Dictionary<Missile.Type, Queue<Missile>> mMissilePool;
}
