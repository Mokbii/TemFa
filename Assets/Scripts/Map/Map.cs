using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour 
{
	public enum MapTileType
	{
		None,
		Way,
	}
	public int vMapWidth = 500;
	public int vMapHeight = 250;
	public int vTileSize = 10;

	void Update () 
	{
#if UNITY_EDITOR
		_DrawDebugInfo();
#endif
	}

	public void Init()
	{
		mRowCount = vMapWidth / vTileSize;
		mColCount = vMapHeight / vTileSize;

		mMapTiles = new MapTile[mRowCount, mColCount];

		for (int iCol = 0; iCol < mColCount; iCol++)
		{
			for (int iRow = 0; iRow < mRowCount; iRow++)
			{
				//MapTile lMapTile = new MapTile(iCol * vMapWidth + iRow, new Vector2((iRow * vTileSize), (iCol * vTileSize)), vTileSize);
				MapTile lMapTile = new MapTile(iCol * vMapWidth + iRow, iRow, iCol, vTileSize);
				mMapTiles[iRow, iCol] = lMapTile;
			}
		}
	}
	// 맵의 정보(바닥 정보)를 로딩합니다.
	public void LoadMapInfo()
	{
		for (int iCol = 0; iCol < mColCount; iCol++)
		{
			for (int iRow = 0; iRow < mRowCount; iRow++)
			{
				mMapTiles[iRow, iCol].SetTileType(MapTileType.Way);
			}
		}
	}
	// MapTile을 받아옴
	public MapTile GetMapTile(Vector3 pPosition)
	{
		if (pPosition.x < 0 || pPosition.z < 0)
			return null;

		int lRowIndex = (int)pPosition.x / vTileSize;
		int lColIndex = (int)pPosition.z / vTileSize;

		if (lRowIndex < 0 || lColIndex < 0)
			return null;
		if (lRowIndex >= mRowCount || lColIndex >= mColCount)
			return null;

		return mMapTiles[lRowIndex, lColIndex];
	}
	private void _DrawDebugInfo()
	{
		for (int iCol = 0; iCol < mColCount; iCol++)
		{
			for (int iRow = 0; iRow < mRowCount; iRow++)
			{
				mMapTiles[iRow, iCol].DrawDebugTile();
			}
		}
	}
	private MapTile[,] mMapTiles;
	private int mRowCount;
	private int mColCount;
}
