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
		mColliderPools = new Stack<int>();
	}

	public void AddCollider(ColliderInfo pColliderInfo)
	{
		if (mColliderPools.Count > 0)
			pColliderInfo.vGuid = mColliderPools.Pop();
		else
			pColliderInfo.vGuid = mColliderInfos.Count;
		mColliderInfos.Add(pColliderInfo);
	}
	public void RemoveCollider(ColliderInfo pColliderInfo)
	{
		mColliderPools.Push(pColliderInfo.vGuid);
		mColliderInfos.Remove(pColliderInfo);
	}
	private static ColliderManager sInstance;
	private Stack<int> mColliderPools;
	private List<ColliderInfo> mColliderInfos;
}
