using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * タッチの入力があったときに、BlockGeneratorから次のブロックを受け取って、
 * タッチ座標にブロックを追従させて、離したポイントによってブロックの初期位置を決定する。
 * そして、ブロックの動きを始めるメソッドを呼び出す。
 * </summary>
*/

public class BlockShooter : MonoBehaviour
{

    [SerializeField] GameObject col1;
    [SerializeField] GameObject col2;
    [SerializeField] GameObject col3;
    [SerializeField] GameObject col4;
    [SerializeField] GameObject col5;

    private List<GameObject> cols;


    private BlockGenerator blockGenerator;
    void Start()
    {
        blockGenerator = GameObject.Find("BlockStarter").GetComponent<BlockGenerator>();
        cols = new List<GameObject>();
        cols.Add(col1);
		cols.Add(col2);
		cols.Add(col3);
		cols.Add(col4);
		cols.Add(col5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount <= 0) { return; }

        Touch touch = Input.GetTouch(0);
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit)
            {
                Bounds rect = hit.collider.bounds;

                if (rect.Contains(worldPoint))
                {
                    var go = hit.collider.gameObject;
                    Debug.Log(cols.IndexOf(go));
                }
            }
        }


    }
}
