using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMaster : Singleton<DeckMaster> {

    private List<Card> mMainDeckList;
    private List<Card> mSubDeckList;
    private List<Card> mQuestDeckList;


    public Sprite[] m_SpadeCardSprite;
    public Sprite[] m_CloverCardSprite;
    public Sprite[] m_HeartardSprite;
    public Sprite[] m_DiamondCardSprite;

    private List<int> mTempList;

    private Card mCard;
    void Awake () 
    {
        mTempList = new List<int>();

        mMainDeckList = new List<Card>();
        mSubDeckList = new List<Card>();
        mQuestDeckList = new List<Card>();
    }

    private void NewDeck(DeckType state)
    {
        print("new deck : " + state);
        mTempList.Clear();
        for (int i = 0; i < GlobalValue.iDeckTotalCount; i++)
        {
            mTempList.Add(i);
        }

        for (int i = 0; i < GlobalValue.iDeckTotalCount; i++)
        {
            int ran = Random.Range(0, mTempList.Count);
            Card card = new Card();
            card.Init(mTempList[ran]);
            switch (state)
            {
                case DeckType.Main:
                    mMainDeckList.Add(card);
                    break;
                case DeckType.Sub:
                    mSubDeckList.Add(card);
                    break;
                case DeckType.Quest:
                    mQuestDeckList.Add(card);
                    break;
            }
            mTempList.RemoveAt(ran);
        }
    }

    public Card DrawMainDeck()
    {
        if(mMainDeckList.Count < GameManager.instance.ReturnDeckLimit(DeckType.Main))
        {
            mMainDeckList.Clear();
            NewDeck(DeckType.Main);
        }

        mCard = mMainDeckList[0];
        mMainDeckList.RemoveAt(0);
        //if(mMainDeckList.Count < )
        //{ 
        
        //}

        return mCard;
    }

    public Card DrawSubDeck()
    {
        if (mSubDeckList.Count < GameManager.instance.ReturnDeckLimit(DeckType.Sub))
        {
            mSubDeckList.Clear();
            NewDeck(DeckType.Sub);
        }
        mCard = mSubDeckList[0];
        mSubDeckList.RemoveAt(0);
        return mCard;
    }

    public Card DrawQuestDeck()
    {
        mCard = mQuestDeckList[0];
        mQuestDeckList.RemoveAt(0);
        return mCard;
    }

    public void NewMainDeck()
    {
        NewDeck(DeckType.Main);
    }

    public void NewSubDeck()
    {
        NewDeck(DeckType.Sub);
    }

    public void NewQuestDeck()
    {
        NewDeck(DeckType.Quest);
    }

    public Sprite ReturnCardSprite(CardShape shape, CardLevel level)
    {
        Sprite sprite = null;

        switch(shape)
        {
            case CardShape.Spade:
                sprite = m_SpadeCardSprite[(int)level];
                break;
            case CardShape.Clover:
                sprite = m_CloverCardSprite[(int)level];
                break;
            case CardShape.Heart:
                sprite = m_HeartardSprite[(int)level];
                break;
            case CardShape.Diamond:
                sprite = m_DiamondCardSprite[(int)level];
                break;
        }
        return sprite;
    }
}
