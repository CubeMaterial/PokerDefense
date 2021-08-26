using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainDeckMaster : Singleton<MainDeckMaster> 
{
    private List<Card> mHandList;
    public Image[] m_CardList; 
    private int iHandSum;
    private int iHandSumA;

    public Text m_MainDealerAttackText;
    public Text m_MainDealerAttackGuide;

    private bool m_IsBlock = false;

     

    private void Awake()
    {
        mHandList = new List<Card>(); 
        iHandSum = 0;
        iHandSumA = 0;
    }

    public void GetCard()
    {
        if (m_IsBlock == true)
            return;

        if (GameManager.instance.ReturnCost(DeckType.Main))
        {
            m_IsBlock = true;
            mHandList.Add(DeckMaster.instance.DrawMainDeck());
            CheckHand();
            Draw();
        }
        else
        {
            print("not enought gold");
        }
    }

    private void CheckHand()
    {
        iHandSum = 0;
        bool hasACard = false;
        for (int i = 0; i < mHandList.Count; i++)
        {
            iHandSum += ReturnValue(mHandList[i].ReturnCardLevel());
            print("ihand : " + iHandSum + " / i : " + i + " / level  : " + mHandList[i].ReturnCardLevel());
        }

        for (int i = 0; i < mHandList.Count; i++)
        {
            if (mHandList[i].ReturnCardLevel() == CardLevel.A)
            {
                hasACard = true;
                mHandList.RemoveAt(i);
            }   
        }

        if (iHandSum > 21)
        {
            if (hasACard == true)
            {
                iHandSum = 0;
                for (int i = 0; i < mHandList.Count; i++)
                {
                    iHandSum += ReturnValue(mHandList[i].ReturnCardLevel());
                    iHandSum += 1;
                }

                if (iHandSum > 21)
                {
                    Burst();
                }
            }
        }

        print("iHandSum : " + iHandSum);
    }

    private void Draw()
    {
        m_CardList[mHandList.Count - 1].sprite =
        DeckMaster.instance.ReturnCardSprite(mHandList[mHandList.Count - 1].ReturnCardShape(), mHandList[mHandList.Count - 1].ReturnCardLevel());
    }

    private void CheckResult()
    { 
    
    }

    public void Apply()
    {
        MainDealer.instance.AddAtk(iHandSum);
        mHandList.Clear();

    }
    private void Burst()
    {
        // Destory card
        m_IsBlock = true;
        mHandList.Clear();
    }

    private int ReturnValue(CardLevel level)
    {
        int i = 0;

        if (level == CardLevel.A)
        {
            i = 11;
        }
        else if (level == CardLevel.J || level == CardLevel.Q || level == CardLevel.K)
        {
            i = 10;
        }
        else
        {
            i = (int)level + 2;
        }

        return i;
    }
}
