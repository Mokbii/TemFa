/// <summary>
/// 게임 시동 관련 처리  [7/30/2015 khsong33]
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameEngine : MonoBehaviour 
{
	public UnitHero vHeroUnit;
	public Map vMapBase;
	// 게임 엔진에서만 Awake 사용 다른곳에서 초기화시엔 Start에서만 처리하도록 합니다.
	void Awake () 
	{
		if (vHeroUnit == null)
			Debug.LogError("vHeroUnit is Empty - " + this);
		if(vMapBase == null)
			Debug.LogError("vMapBase is Empty - " + this);

		// 게임에서 사용할 싱글턴 객체들 처리(관리자)
		GameControl.aInstance.Init();
		ColliderManager.aInstance.Init();
		MissileManager.aInstance.Init();

		// 게임 필수 오브젝트 처리
		vHeroUnit.Init();
		vMapBase.Init();

		// 로드는 차후 다른곳으로 이동 - 랜덤맵 사용
		_LoadMap();
	}
	void Update()
	{
		GameControl.aInstance.OnUpdate();
	}
	void OnDestory()
	{
		vHeroUnit.Destroy();
	}
	// 맵 정보를 불러오고, 주인공 유닛에 맵정보를 입력합니다.
	private void _LoadMap()
	{
		vMapBase.LoadMapInfo();
		vHeroUnit.SetMap(vMapBase);
	}
}
