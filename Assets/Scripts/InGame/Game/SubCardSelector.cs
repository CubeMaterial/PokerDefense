using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCardSelector : MonoBehaviour 
{
    public UISprite mFrame;
    private Card mCard;
    private int iIndex;
    private bool hasSelected;
    private void Awake()
    {
        hasSelected = false;
        mCard = GetComponent<Card>();
    }
   



    public void SelectCard()
    {
        hasSelected = !hasSelected;
        SubDeckMaster.instance.SelectCard(mCard, hasSelected);
        if (hasSelected)
        { 
            // select

        }
        else
        {
            // select cancel    
        }

    }
}
