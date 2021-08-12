using UnityEngine;
using System.Collections;


using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// //////
/// </summary>

public class GameManager : Singleton<GameManager>
{

    private int[] m_iWhiteChip;
    private int[] m_iBlueChip;
    private int[] m_iBlackChip;
    private int[] m_iTempChip;
    private int iCurrentGold;
    private int[] iCurrentCost;
    private int[] iCurrentDeckLimit;
    private bool m_IsGameProcessing = false;
    public Transform[] MovePoint;
    public TextAsset TowerData;

    private void Awake()
    {
        m_iWhiteChip = new int[10];
        m_iBlueChip = new int[10];
        m_iBlackChip = new int[10];
        m_iTempChip = new int[10];
        iCurrentCost = new int[3];
        iCurrentDeckLimit = new int[3];

        for(int i = 0; i < m_iWhiteChip.Length; i++ )
        {
            m_iWhiteChip[i] = 0;
            m_iBlueChip[i] = 0;
            m_iBlackChip[i] = 0;
        }

        for (int i = 0; i < iCurrentCost.Length; i++)
        {
            iCurrentCost[i] = 1;
            iCurrentDeckLimit[i] = GlobalValue.iDeckTotalCount / 2;
        }

        iCurrentGold = 100;
    }

    public void Restart()
    {
        GameDataManager.instance.Restart();
    }

    public bool ReturnCost(DeckType type)
    { 
        if(iCurrentCost[(int)type] <= iCurrentGold)
        {
            iCurrentGold -= iCurrentCost[(int)type];
            iCurrentCost[(int)type]++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CalculateCost(int c, ChipType type, string str = "")
    {
        if(c <= 0)
        {
            PrintLog("sagi");
        }
        else
        {
            switch(type)
            {
                case ChipType.White:

                break;
                case ChipType.Blue:
                break;
                case ChipType.Black:
                break;
            }
        }
    }

    public int ReturnDeckLimit(DeckType type)
    {
        return iCurrentDeckLimit[(int)type];
    }

     public int ReturnValue(int[] t)
    {
        int p = int.Parse(t[9] +""+ t[8] + t[7]+ t[6] + t[5]+ t[4]+ t[3]+ t[2]+ t[1] + t[0]); 
        return p;
    }

    public int[] ReturnValue(int t)
    {
        string temp = t.ToString();
        if(temp.Length < 10)
        {
            int cnt = 10 - temp.Length;
            for(int i = 0; 1 < cnt; i++)
            {
                temp  = "0" + temp;
            }
        }        
        print("tem p  ; " + temp);
        m_iTempChip[9] = int.Parse(temp.Substring(0,1));
        m_iTempChip[8] = int.Parse(temp.Substring(1,1));
        m_iTempChip[7] = int.Parse(temp.Substring(2,1));
        m_iTempChip[6] = int.Parse(temp.Substring(3,1));
        m_iTempChip[5] = int.Parse(temp.Substring(4,1));
        m_iTempChip[4] = int.Parse(temp.Substring(5,1));
        m_iTempChip[3] = int.Parse(temp.Substring(6,1));
        m_iTempChip[2] = int.Parse(temp.Substring(7,1));
        m_iTempChip[1] = int.Parse(temp.Substring(8,1));
        m_iTempChip[0] = int.Parse(temp.Substring(9,1));
        return m_iTempChip;
    }


    public void PressStartBtn()
    {
        StartCoroutine(Gamestart());
    }

    private IEnumerator Gamestart()
    {
        yield return new WaitForSeconds(1f);
        WaveStart();
    }

    public void WaveStart()
    {
        //GameDataManager.instance.ChangeValueStageLevel(1);
        //EnemyManager.instance.SummonEnemy(GameDataManager.instance.ReturnValueStageLevel());
    }

    public bool GetGameProcessing()
    {
        return m_IsGameProcessing;
    }

    public void PrintLog(string log)
    {
        Debug.Log(log);
    }
}