using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/**
 * <summary>
 * 召喚時間のカウントダウン。
 * 0になったら、一列目の召喚、削除と、カウントダウンのリセットをする。
 * </summary>
*/
public class Timer : MonoBehaviour
{

    [SerializeField] private float summonSpeedSec;
    private bool isPlaying;
    private double countTime = 0;
    private GameObject blockArea;

    private AttackManager attackManager;
    private GridInfo gridInfo;
    [SerializeField] private TextMeshProUGUI summonTimeText;

    void Start()
    {
        // リリース
        // summonSpeedSec = GameStateManager.Instance.MyCharaData.SummonSpeedSec;
        isPlaying = false;

        //これはデバッグ用
        //StartGameTimer();
        summonSpeedSec = 15;

        blockArea = GameObject.Find("BlockArea");
        attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();

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
			GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("summon", 0.5f);
			// 最上部のモンスターを消してダメージを与える
			int totalRank = 0;
            var delaySec = 0f;
            for (int i = 0; i < gridInfo.monsterPos.GetLength(0); i++)
            {
                var monster = gridInfo.monsterPos[i, 0];
                if (monster != null)
                {
                    totalRank += monster.GetComponent<Block>().blockData.Rank;
                    BlockLight light = monster.transform.Find("Light").GetComponent<BlockLight>();
                    Color color = monster.GetComponent<SpriteRenderer>().color;
                    StartCoroutine(DelayMethod(delaySec, () =>
                    {
                        monster.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
                        light.Merge();
                    }));
                    delaySec += 0.1f;
                }
            }
            attackManager.Attack(totalRank);
            countTime = 0;
        }
        summonTimeText.SetText(string.Format("{0:0.0}", (summonSpeedSec - countTime)));
    }

    public void StartGameTimer()
    {
        isPlaying = true;
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

}
