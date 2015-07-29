using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapTile 
{
	public MapTile()
	{
		mIndex = 0;
		mPosition = new Vector2(0, 0);
	}
	public MapTile(int pIndex, Vector2 pPos)
	{
		mIndex = pIndex;
		mPosition = pPos;
	}

	private int mIndex;
	private Vector2 mPosition;
}
