using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ButtonTest : MonoBehaviour
{
	public TestManager mManager;
	private Button mButton;
	void Start()
	{
		UnityEngine.Events.UnityAction action = () => { OnClick(); };
		mButton = gameObject.GetComponent<Button>();
		mButton.onClick.AddListener(action);
	}
	private void OnClick()
	{
		mManager.OnClickButton(new TestObject(1, 2));
	}
}