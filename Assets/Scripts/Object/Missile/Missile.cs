using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
	public enum Type
	{
		Direct,
	}
    public int vGuid; // 고유Id
	public Type vType; // 미사일 타입(풀링을 위해 존재)
	public int vUnitTeamId; // 발사한 유닛의 구분자
	public float vDistance; // 사정거리
	public int vPower; // 데미지
	public float vSpeed; // 속도

	public Rigidbody vRigidBody;
	public ColliderInfo vColliderInfo;

	public Transform aTransform
	{
		get { return mTransform; }
	}
	public virtual void Init(int pGuid)
	{
        vGuid = pGuid;
		mTransform = transform;
		vRigidBody = gameObject.GetComponent<Rigidbody>();
		vColliderInfo = gameObject.GetComponent<ColliderInfo>();
	}
	// 미사일의 시작위치와 나갈 방향을 결정
	public void GoShot(Unit pAttackUnit, Vector3 pStart, Vector3 pDirect)
	{
        // TODO :: 유닛에 있는 능력치를 이용해 Missile의 능력 setup을 진행합니다.
		mTransform.position = pStart;
		mDirect = pDirect;
		vColliderInfo.vValue = vUnitTeamId = pAttackUnit.vUnitTeamId;
		_OnShot();
	}
    protected virtual void _OnDestroy()
    {
        gameObject.SetActive(false);
        MissileManager.aInstance.RemoveMissile(this);
    }
	// 미사일 발사 처리는 각 클래스에서 처리
	protected virtual void _OnShot() {}

	protected Transform mTransform;
	protected Vector3 mDirect;
}
