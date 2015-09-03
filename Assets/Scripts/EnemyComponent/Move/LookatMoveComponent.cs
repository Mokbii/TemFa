using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class LookatMoveComponent : MoveComponent
{
	public override void OnPlay()
	{
		Debug.Log("Look at move component play");
		StartCoroutine("_OnPlay");
	}
	private IEnumerator _OnPlay()
	{
		while (vUnitEnemy.aIsAlive)
		{
			yield return null;
			Transform lTarget = GameDataManager.aInstance.aUnitHero.aTransform;

			Vector3 lToTargetPosition = lTarget.position - vUnitEnemy.aTransform.position;
			lToTargetPosition = lToTargetPosition.normalized;

			Vector3 lNextMove = new Vector3(lToTargetPosition.x * vUnitEnemy.vMaxMoveSpeed.x, lToTargetPosition.x, lToTargetPosition.z * vUnitEnemy.vMaxMoveSpeed.y);
			Vector3 lNextMovePosition = vUnitEnemy.aTransform.position + (lNextMove * Time.deltaTime);

			float lMoveX = lNextMovePosition.x;
			float lMoveY = lNextMovePosition.z;

			float rotationDegree = Mathf.Atan2(lToTargetPosition.x, lToTargetPosition.z) * Mathf.Rad2Deg;
			vUnitEnemy.aTransform.rotation = Quaternion.Euler(0f, rotationDegree, 0f);

			// X타일 검사
			MapTile lCheckTileX = GameDataManager.aInstance.aMap.GetMapTile(new Vector3(lMoveX, 0, vUnitEnemy.aTransform.position.z));
			if (lCheckTileX == null || lCheckTileX.aTileType != Map.MapTileType.Way)
				lMoveX = vUnitEnemy.aTransform.position.x;

			// Y타일 검사
			MapTile lCheckTileY = GameDataManager.aInstance.aMap.GetMapTile(new Vector3(vUnitEnemy.aTransform.position.x, 0, lMoveY));
			if (lCheckTileY == null || lCheckTileY.aTileType != Map.MapTileType.Way)
				lMoveY = vUnitEnemy.aTransform.position.z;

			vUnitEnemy.aTransform.position = new Vector3(lMoveX,
												  vUnitEnemy.aTransform.position.y,
												  lMoveY);
		}
	}
}