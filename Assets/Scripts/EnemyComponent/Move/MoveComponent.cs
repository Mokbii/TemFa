using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class MoveComponent : MonoBehaviour
{
	public UnitEnemy vUnitEnemy;
	public void Start()
	{
		vUnitEnemy = gameObject.GetComponent<UnitEnemy>();
		if (vUnitEnemy == null)
			Debug.LogError("This object is not exist UnitEnemy component. please check your component");
	}
	public abstract void OnPlay();
}