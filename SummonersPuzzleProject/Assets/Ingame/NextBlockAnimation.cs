using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBlockAnimation : MonoBehaviour
{
    public Vector2 TargetPosition { get; set; }
    public float MoveSpeed { get; set; } = 0.2f;
    void Update()
    {
        Vector2 nowPosition = this.transform.position;
        if (!IsTargetPosition())
        {
            // 今の位置と目標地点が違った場合の処理（移動する）
            Vector2 targetDir = (TargetPosition - nowPosition);
            if (targetDir.x != 0) targetDir.x /= Mathf.Abs(targetDir.x);
            if (targetDir.y != 0) targetDir.y /= Mathf.Abs(targetDir.y);
            targetDir.y *= -1;
            // classify = (input > 0) ? "positive" : "negative";
            transform.position += new Vector3(targetDir.x * MoveSpeed, targetDir.y * MoveSpeed);
            if (transform.position.x > TargetPosition.x)
            {
                transform.position = new Vector3(TargetPosition.x, transform.position.y);
            }
            if (transform.position.y > TargetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, TargetPosition.y);
            }
        }

    }
    public bool IsTargetPosition()
    {
        return Mathf.Abs(this.transform.position.x - TargetPosition.x) < float.Epsilon &&
                Mathf.Abs(this.transform.position.y - TargetPosition.y) < float.Epsilon;
    }
}
