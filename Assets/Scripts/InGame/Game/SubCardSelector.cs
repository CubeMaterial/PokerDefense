using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCardSelector : MonoBehaviour 
{
    public Image m_Frame;
    private Card m_Card;
    private int m_iIndex;
    private bool m_IsSelected;
    private void Awake()
    {
        m_IsSelected = false;
        m_Card = GetComponent<Card>();
    }
   



    public void SelectCard()
    {
        m_IsSelected = !m_IsSelected;
        SubDeckMaster.instance.SelectCard(m_Card, m_IsSelected);
        if (m_IsSelected)
        { 
            // select

        }
        else
        {
            // select cancel    
        }

    }
}
