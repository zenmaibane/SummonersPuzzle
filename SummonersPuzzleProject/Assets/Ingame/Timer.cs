using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * 召喚時間のカウントダウン。
 * 0になったら、一列目の召喚、削除と、カウントダウンのリセットをする。
 * </summary>
*/
public class Timer : MonoBehaviour
{

    private float summonSpeedSec;
    private bool isPlaying;
    private double countTime = 0;
    private GameObject blockArea;
    private GridInfo gridInfo;
    private HPManager HPManager;

    [SerializeField] private Slider timeSlider;

    void Start()
    {
        // キャラクターによって詠唱時間を変える
        summonSpeedSec = 5;

        // リリース
        isPlaying = false;

        //これはデバッグ用
        StartGameTimer();
        timeSlider.maxValue = summonSpeedSec;

        blockArea = GameObject.Find("BlockArea");
        HPManager = GameObject.Find("HPManager").GetComponent<HPManager>();

        gridInfo = blockArea.GetComponent<GridInfo>();
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
        
        var canSummon = (countTime - summonSpeedSec) >= float.Epsilon;
        if (canSummon)
        {
            // 最上部のモンスターを消してダメージを与える
            int totalRank = 0;
            for (int i = 0; i < gridInfo.monsterPos.GetLength(0); i++)
            {
                var monster = gridInfo.monsterPos[i, 0];
                if (monster != null)
                {
                    totalRank += monster.GetComponent<Block>().blockData.Rank;
                    Destroy(monster);
                    monster = null;
                }
                HPManager.DamageRival(totalRank);
            }
            countTime = 0;
        }
        timeSlider.value = (float)(summonSpeedSec - countTime);
    }

    public void StartGameTimer()
    {
        isPlaying = true;
    }
}
