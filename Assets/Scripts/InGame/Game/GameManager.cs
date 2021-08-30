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

    private int iGrade;
    private int iLevel;
    private int iSubLevel;

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

    private IEnumerator  Start() 
    {
        yield return new WaitForSeconds(1f);

        PressStartBtn();    
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
        GameDataManager.instance.ChangeValueStageLevel(0);
        EnemyManager.instance.SummonEnemy(GameDataManager.instance.ReturnCurrentLevel());
    }

    public bool GetGameProcessing()
    {
        return m_IsGameProcessing;
    }

    public void PrintLog(string log)
    {
        Debug.Log(log);
    }

    public int ReturnGrade(List<Card> mList)
    {
        int iGrade = 0;
        bool m_IsFlush = false;
        int flush = 0, straight = 0, fourCard = 0, triple = 0, onePair = 0, twoPair = 0, fullHouse = 0, top = 0, grade = 0, straightChk = 0;
        int[] shape = new int[4];
        for (int i = 0; i < 4; i++)
        {
            shape[i] = 0;
        }

        int[] number = new int[13];
        for (int i = 0; i < 13; i++)
        {
            number[i] = 0;
        }


        for (int q = 0; q < mList.Count; q++)
        {
            switch (mList[q].ReturnCardShape())
            {
                case CardShape.Heart:
                    shape[0]++;
                    break;
                case CardShape.Diamond:
                    shape[1]++;
                    break;
                case CardShape.Clover:
                    shape[2]++;
                    break;
                case CardShape.Spade:
                    shape[3]++;
                    break;
            }

            if (shape[0] >= 5) flush = 1;
            else if (shape[1] >= 5) flush = 2;
            else if (shape[2] >= 5) flush = 3;
            else if (shape[3] >= 5) flush = 4;

            if (flush >= 1 && grade <= 6)
            {
                m_IsFlush = true;
                grade = 6;
            }
        }// flush check

        for (int q = 0; q < mList.Count; q++)
        {
            number[(int)mList[q].ReturnCardLevel()]++;
        }//number 배열에 카드의 숫자값을 빼서 넣어줌

        for (int q = 0; q < 9; q++)
        {
            for (int w = q; w < q + 5; w++)
            {
                if (number[w] >= 1)
                {
                    straightChk++;
                }
            }//작은 for
            if (straightChk >= 5)
            {
                straight = q + 6;
                
                if(m_IsFlush == true)
                {
                    grade = 9;
                }   
                else
                {
                    if (grade <= 5)
                    {
                        grade = 5;
                    }
                }                 
            }
            straightChk = 0;
        }//straight check

        for (int q = 0; q < 13; q++)
        {
            if (number[q] == 4)
            {
                fourCard = q + 2;

                if (grade <= 8)
                    grade = 8;
            }
            else if (triple >= 1 && onePair >= 1)
            {
                fullHouse = triple;
                if (grade <= 7)
                    grade = 7;
            }
            else if (number[q] == 3)
            {
                triple = q + 2;
                if (grade <= 4)
                    grade = 4;
            }
            else if (onePair >= 1 && number[q] == 2 && twoPair == 0)
            {
                twoPair = q + 2;
                if (grade <= 3)
                    grade = 3;
            }
            else if (onePair >= 1 && number[q] == 2 && twoPair >= 1)
            {
                onePair = twoPair;
                twoPair = q + 2;
                if (grade <= 3)
                    grade = 3;
            }// 3페어 방지
            else if (number[q] == 2 && grade != 3)
            { // 주의!!!!!!! 이 부분이 아주 중요함 && grade < 5
                onePair = q + 2;
                if (grade <= 2)
                    grade = 2;
            }//투페어가 당첨되면 원페어는 그대로 있기위한 조건
            else if (number[q] == 1)
            {
                top = q + 2;
                if (grade <= 1)
                    grade = 1;
            }
        }//원,투페어,트리플,풀하우스,포카드 확인


        switch (grade)
        {
            case 1:
                iLevel = gradeResult(top);
                iSubLevel = -1;
                //print("<Top> " + gradeResult(top));
                break;
            case 2:
                iLevel = gradeResult(onePair);
                iSubLevel = -1;
                //print("<One Pair> " + gradeResult(onePair));
                break;
            case 3:
                iLevel = gradeResult(twoPair);
                iSubLevel = gradeResult(onePair);
                //print("<Two Pair> " + gradeResult(twoPair) + " / " + gradeResult(onePair));
                break;
            case 4:
                iLevel = gradeResult(triple);
                iSubLevel = -1;
                //print("<Triple> " + gradeResult(triple));
                break;
            case 5:
                iLevel = gradeResult(straight);
                iSubLevel = -1;
                //print("<Straight> " + gradeResult(straight));
                break;
            case 6:
                iLevel = flush - 1;
                iSubLevel = -1;
                //print("<Flush> " + (CardShape)(flush - 1));
                break;
            case 7:
                iLevel = gradeResult(fullHouse);
                iSubLevel = gradeResult(onePair);
                //print("<FullHouse> " + gradeResult(fullHouse) + " / " + gradeResult(onePair));
                break;
            case 8:
                iLevel = gradeResult(fourCard);
                iSubLevel = -1;
                //print("<FourCard> " + gradeResult(fourCard));
                break;
        }

        return iGrade;
    }

    private int gradeResult(int grade)
    {
        int tmp = 0;
        //print("grade : " + (grade-2));

        tmp = (grade-2);
        //for (int q = 0; q < mHandList.Count; q++)
        //{
        //    if (mHandList[q].ReturnCardLevel().Equals("" + grade))
        //    {
        //        tmp += mHandList[q].ReturnCardShape() + " ";
        //    }
        //}
        return tmp;
    } //Grade Result
}