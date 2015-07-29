using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour 
{
	void Start () 
	{
		GameControl.aInstance.Init();
	}
	void Update ()
	{
		GameControl.aInstance.OnUpdate();
	}
}
