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
	void Start () 
	{
		if (vHeroUnit == null)
			Debug.LogError("vHeroUnit is Empty - " + this);
		if(vMapBase == null)
			Debug.LogError("vMapBase is Empty - " + this);

		GameControl.aInstance.Init();
		vHeroUnit.Init();
		vMapBase.Init();

		_LoadMap();
	}
	void Update ()
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
