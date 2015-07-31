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
			lMissile.Init();
			lMissile.GoShot(pAttackUnit, pStartPos, pMissileDirect);
		}

	}
	private static MissileManager sInstance;
}
