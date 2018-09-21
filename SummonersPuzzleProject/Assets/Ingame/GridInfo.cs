using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * 各マスを二次元配列で表現して、各マスの中心座標を計算して保持しておく。
 * </summary>
*/

public class GridInfo : MonoBehaviour {

	public GameObject[,] monsterPos;
	public Vector2[,] centerCoordinate;
	public int gridX = 5;
	public int gridY = 6;

	// Use this for initialization
	void Start ()
	{
		Vector2 topLeftPos = GetSpriteTopLeftPosition();

		float tempx = topLeftPos.x + (transform.position.x - topLeftPos.x) * 2;
		float tempy = topLeftPos.y + (transform.position.y - topLeftPos.y) * 2;
		Vector2 bottomRightPos = new Vector2(tempx, tempy);
		print("grid topLeftPos : " + topLeftPos + "\tbottomRightPos : " + bottomRightPos);

		float width = bottomRightPos.x - topLeftPos.x;
		float height = topLeftPos.y - bottomRightPos.y;
		print("grid width = " + width);
		print("grid height = " + height);

		// gridX列gridY行の配列として初期化する
		centerCoordinate = new Vector2[gridX, gridY];
		monsterPos = new GameObject[gridX, gridY];

		// centerCoordinate の座標計算。
		for (int x = 0; x < gridX; x++)
		{
			for (int y = 0; y < gridY; y++)
			{
				float centerX = topLeftPos.x + (2 * x + 1) * (width / gridX / 2);
				float centerY = topLeftPos.y - (2 * y + 1) * (height / gridY / 2);
				centerCoordinate[x, y] = new Vector2(centerX, centerY);
				//print(x + ", " + y + " : " + centerCoordinate[x, y]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// デバッグ用で、シーンビューにのみ描画される。
	// centerCoordinateの座標（各マスの中心座標）を描画する。
	/*
	void OnDrawGizmos()
	{
		for (int x = 0; x < gridX; x++)
		{
			for (int y = 0; y < gridY; y++)
			{
				Gizmos.DrawSphere(centerCoordinate[x, y], 0.2f);
			}
		}
	}
	*/

	// グリッドの左上の座標を取得する
	Vector2 GetSpriteTopLeftPosition()
	{
		var _spriteRenderer = GetComponent<SpriteRenderer>();
		var _sprite = _spriteRenderer.sprite;
		var _halfX = _sprite.bounds.extents.x;
		var _halfY = _sprite.bounds.extents.y;
		var _vec = new Vector2(-_halfX, _halfY);
		var _pos = _spriteRenderer.transform.TransformPoint(_vec);
		return _pos;
	}

}
