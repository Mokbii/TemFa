using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderManager
{
	public static ColliderManager aInstance
	{
		get
		{
			if (sInstance == null)
				sInstance = new ColliderManager();
			return sInstance;
		}
	}
	public void Init()
	{
		mColliderInfos = new List<ColliderInfo>();
	}

	public void AddCollider(ColliderInfo pColliderInfo)
	{
		pColliderInfo.vGuid = mColliderInfos.Count;
		mColliderInfos.Add(pColliderInfo);
	}
	private static ColliderManager sInstance;
	private List<ColliderInfo> mColliderInfos;
}
