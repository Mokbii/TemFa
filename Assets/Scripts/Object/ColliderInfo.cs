using UnityEngine;
using System.Collections;

public enum ColliderType
{
	Wall,
	Unit,
	Missile,
}
public class ColliderInfo : MonoBehaviour 
{
	public delegate void OnColliderDelegate(ColliderInfo pColliderInfo);
	public OnColliderDelegate aOnColliderDelegate
	{
		get { return mOnColliderDelegate; }
		set { mOnColliderDelegate = value; }
	}
	public int vGuid; // 고정 아이디
	public ColliderType vType;
	public int vValue;
	public Collider vCollider;
	void Start()
	{
		vCollider = gameObject.GetComponent<Collider>();
		if(vCollider == null)
			Debug.LogError("vCollider Is Empty. please enter the collider in this object - " + this);
		ColliderManager.aInstance.AddCollider(this);
	}
	void OnTriggerEnter(Collider pOther)
	{
		//Debug.Log("OnTrigger Enter From " + this.name + " to " + pOther.name);
		ColliderInfo lOtherColliderInfo = pOther.GetComponent<ColliderInfo>();
		if (aOnColliderDelegate != null)
			aOnColliderDelegate(lOtherColliderInfo);
	}
	private OnColliderDelegate mOnColliderDelegate;
}
