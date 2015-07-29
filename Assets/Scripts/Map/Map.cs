using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour 
{
	public int vMapWidth = 500;
	public int vMapHeight = 250;
	// Use this for initialization
	void Start () 
	{
		mMapTiles = new MapTile[vMapWidth, vMapHeight];

		int lMapWidthStart = -1 * (vMapWidth / 2);
		int lMapWidthEnd = vMapWidth / 2;
		int lMapHeightStart = -1 * (vMapHeight / 2);
		int lMapHeightEnd = vMapHeight / 2;

		int lRowCount = 0;
		int lColCount = 0;

		for (int iCol = lMapHeightStart; iCol < lMapHeightEnd; iCol++)
		{
			for (int iRow = lMapWidthStart; iRow < lMapWidthEnd; iRow++)
			{
				MapTile lMapTile = new MapTile(iCol * vMapWidth + iRow, new Vector2(iRow, iCol));
				mMapTiles[lRowCount, lColCount] = lMapTile;
				lRowCount++;
			}
			lColCount++;
		}
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	private MapTile[,] mMapTiles;
}
