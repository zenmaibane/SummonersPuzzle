using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CharaName
{
    Alice,
    Becca,
    Charlotte
}

public class CharaDataGenerator : MonoBehaviour
{
    public CharaData GetNextBlock(CharaName charaName)
    {
        switch (charaName)
        {
            case CharaName.Alice:
                return new CharaData(CharaName.Alice, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green }, 14f, 2, 3);
            case CharaName.Becca:
                return new CharaData(CharaName.Becca, new BlockColor[] { BlockColor.Yellow, BlockColor.Green }, 10f, 1, 3);
            case CharaName.Charlotte:
                return new CharaData(CharaName.Charlotte, new BlockColor[] { BlockColor.Yellow}, 7f, 1, 2);
            default:
                throw new ArgumentException($"{Enum.GetName(typeof(CharaName), charaName)} Data is not defined.");
        }
    }
}
