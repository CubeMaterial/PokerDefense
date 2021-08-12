using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    private CardShape mCardShape;
    private CardLevel mCardLevel;

    public UISprite mSprite;

    public CardLevel ReturnCardLevel()
    {
        return mCardLevel;
    }

    public CardShape ReturnCardShape()
    {
        return mCardShape;
    }

    public void Init(int shape, int level)
    {
        mCardShape = (CardShape)shape;
        mCardLevel = (CardLevel)level;
    }

    public void Init(int i)
    {
        mCardShape = (CardShape)(i / System.Enum.GetNames(typeof(CardLevel)).Length);
        mCardLevel = (CardLevel)(i % System.Enum.GetNames(typeof(CardLevel)).Length);

        if(mSprite != null)
        {
            //mSprite.spriteName = 
        }
    }


}
