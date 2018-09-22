using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * 各ブロックオブジェクトにアタッチされ、BlockDataの情報を使って、ブロックの画像を張り替えたりする。
 * </summary>
*/

public class BlockImageManager : MonoBehaviour
{

    private BlockData blockData;

    void Start()
    {
        blockData = GetComponent<Block>().blockData;
    }

    public void ImageReload()
    {
        if (blockData == null)
        {
            blockData = GetComponent<Block>().blockData;
        }
        //print("load image (sprite) : " + GenerateFileName(blockData.Rank, blockData.Color));
        
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GenerateFileName(blockData.Rank, blockData.Color));
    }

    private string GenerateFileName(int rank, BlockColor color)
    {
        string colorInitial = char.ToLower(color.ToString()[0]).ToString();
        return "block_kari_" + colorInitial + rank;
    }
}
