using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    private CardShape m_CardShape;
    private CardLevel m_CardLevel;

    public Image m_Image;

    public CardLevel ReturnCardLevel()
    {
        return m_CardLevel;
    }

    public CardShape ReturnCardShape()
    {
        return m_CardShape;
    }

    public void Init(int shape, CardLevel level)
    {
        m_CardShape = (CardShape)shape;
        m_CardLevel = level;
    }

    public void Init(int shape, int level)
    {
        m_CardShape = (CardShape)shape;
        m_CardLevel = (CardLevel)level;
    }

    public void Init(int i)
    {
        m_CardShape = (CardShape)(i / System.Enum.GetNames(typeof(CardLevel)).Length);
        m_CardLevel = (CardLevel)(i % System.Enum.GetNames(typeof(CardLevel)).Length);

        if(m_Image != null)
        {
            //mSprite.spriteName = 
        }
    }


}
