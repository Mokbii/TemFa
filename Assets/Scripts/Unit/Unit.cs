using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	public int vUnitTeamId;
	public ColliderInfo vUnitColliderInfo;
	public Vector2 vMaxMoveSpeed = new Vector2(1.0f, 1.0f);
	public Transform aTransform
	{
		get { return mTransform; }
	}
	public virtual void Init()
	{
		vUnitColliderInfo = gameObject.GetComponent<ColliderInfo>();
		mTransform = transform;
	}
	public virtual void Destroy()
	{
	}
	public virtual void SetMap(Map pMap)
	{
		mMapInfo = pMap;
	}
	
	protected Transform mTransform;
	protected Vector2 mMoveSpeed;
	protected Map mMapInfo;
}
