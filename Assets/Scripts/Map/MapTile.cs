using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapTile
{
	public Vector2 aMapPosition { get { return mPosition; } }
	public Map.MapTileType aTileType { get { return mTileType; } }

	public MapTile()
	{
		mIndex = 0;
		mPosition = new Vector2(0, 0);
		mTileType = Map.MapTileType.None;
	}
	public MapTile(int pIndex, Vector2 pPos, float pSize)
	{
		mIndex = pIndex;
		mPosition = new Vector2(pPos.x, pPos.y);
		mRect = new Rect(pPos.x, pPos.y, pPos.x + pSize, pPos.y + pSize);
	}
	public void SetTileType(Map.MapTileType pTileType)
	{
		mTileType = pTileType;
	}
	public void DrawDebugTile()
	{
		Debug.DrawLine(new Vector3(mRect.x, 1, mRect.y), new Vector3(mRect.width, 1, mRect.y));
		Debug.DrawLine(new Vector3(mRect.width, 1, mRect.y), new Vector3(mRect.width, 1, mRect.height));
		Debug.DrawLine(new Vector3(mRect.width, 1, mRect.height), new Vector3(mRect.x, 1, mRect.height));
		Debug.DrawLine(new Vector3(mRect.x, 1, mRect.height), new Vector3(mRect.x, 1, mRect.y));
	}
	private int mIndex;
	private Vector2 mPosition;
	private Rect mRect;
	private Rect mDrawRact;
	private Map.MapTileType mTileType;
}
