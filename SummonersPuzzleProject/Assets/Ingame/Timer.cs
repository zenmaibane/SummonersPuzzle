using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * 召喚時間のカウントダウン。
 * 0になったら、一列目の召喚、削除と、カウントダウンのリセットをする。
 * </summary>
*/
public class Timer : MonoBehaviour
{

    private float summonSpeed;
    private bool isPlaying;
    private double countTime = 0;
    private GameObject blockArea;
    private GameObject HPManager;

    void Start()
    {
        // キャラクターによって詠唱時間を変える
        summonSpeed = 3000;

        isPlaying = false;
        blockArea = GameObject.Find("BlockArea");
        HPManager = GameObject.Find("HPManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            Summon(Time.deltaTime);
        }
    }

    public void StartGame()
    {
        isPlaying = true;
        countTime = 0;
    }

    private void Summon(double deltaTime)
    {
        countTime += deltaTime;
        var canSummon = (countTime - summonSpeed) >= float.Epsilon;
        if (canSummon)
        {
            // 最上部のモンスターを消してダメージを与える
            GridInfo gridInfo = blockArea.GetComponent<GridInfo>();
            int totalRank = 0;
            for (int i = 0; i <= gridInfo.monsterPos.Length; i++)
            {
                totalRank += gridInfo.monsterPos[0, i].GetComponent<Block>().blockData.Rank;
                Destroy(gridInfo.monsterPos[0, i]);
                gridInfo.monsterPos[0, i] = null;
            }
            HPManager.GetComponent<HPManager>().DamageRival(totalRank);
            countTime = 0;
        }
    }
}
