using UnityEngine;
using System.Collections;

public class DirectMissile : Missile
{
	public override void Init() 
	{
		base.Init();
		vColliderInfo.aOnColliderDelegate += _OnCollider;
		vType = Missile.Type.Direct;
	}
	protected override void _OnShot()
	{
		mStartPos = aTransform.position;
		mSqrDistance = vDistance * vDistance;
		StartCoroutine(_OnShotCoroutine());
	}
	private IEnumerator _OnShotCoroutine()
	{
		while (true)
		{
			float lSpeed = vSpeed * Time.deltaTime;
			Vector3 lMovePos = mDirect * lSpeed;
			aTransform.position = new Vector3(aTransform.position.x + lMovePos.x,
											  aTransform.position.y,
											  aTransform.position.z + lMovePos.z);

			if (mSqrDistance < Vector3.SqrMagnitude(aTransform.position - mStartPos))
			{
				Destroy(this.gameObject);
				break;
			}

			yield return null;
		}
	}
	private void _OnCollider(ColliderInfo pColliderInfo)
	{
		if (pColliderInfo.vType == ColliderType.Wall)
		{
			Destroy(this.gameObject);
		}
		if (pColliderInfo.vType == ColliderType.Unit)
		{
			Unit lUnit = pColliderInfo.GetComponent<Unit>();
			if (lUnit.vUnitTeamId != pColliderInfo.vValue)
			{
				Destroy(this.gameObject);
			}
		}
	}
	private Vector3 mStartPos;
	private float mSqrDistance;
}
