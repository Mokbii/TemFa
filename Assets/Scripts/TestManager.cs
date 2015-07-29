using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickButton(TestObject a)
	{
		Debug.Log("On Click Button - " + a.ToString());
	}
}

public class TestObject
{
	public int a;
	public int b;
	public TestObject(int _a, int _b)
	{
		a = _a;
		b = _b;
	}
	public override string ToString()
	{
		return string.Format("{0}/{1}", a, b);
	}
}
