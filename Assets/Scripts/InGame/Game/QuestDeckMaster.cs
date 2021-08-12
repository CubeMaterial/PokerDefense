using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDeckMaster : MonoBehaviour {

    private List<Card> mHandList;
    private int[] iUpgradeLevel;
    private int[] iRandomUpgradeLevel;

    private void Awake()
    {
        mHandList = new List<Card>();
        iUpgradeLevel = new int[4];
        iRandomUpgradeLevel = new int[4];
    }

    public void GetCard()
    {
        if (GameManager.instance.ReturnCost(DeckType.Quest))
        {
            mHandList.Add(DeckMaster.instance.DrawQuestDeck());
        }
        else
        {
            print("not enought gold");
        }
    }

    public void UpgradeLevel(CardShape shape)
    { 
        for(int i = 0; i < mHandList.Count; i++)
        { 
            if(mHandList[i].ReturnCardShape() == shape && (int)mHandList[i].ReturnCardLevel() == iUpgradeLevel[(int)shape])
            {
                iUpgradeLevel[(int)shape]++;
                mHandList.RemoveAt(i);
                break;
            }
        }
    }

    //public void RandomUpgradeLevel(CardShape shape, CardLevel level)
    //{
    //    for (int i = 0; i < mHandList.Count; i++)
    //    {
    //        if (mHandList[i].ReturnCardShape() == shape && (int)mHandList[i].ReturnCardLevel() == iUpgradeLevel[(int)shape])
    //        {
    //            iUpgradeLevel[(int)shape]++;
    //            mHandList.RemoveAt(i);
    //            break;
    //        }
    //    }
    //}


}
