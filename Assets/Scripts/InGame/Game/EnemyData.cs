using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    private int m_iGrade;
    public int iGrade{get{return m_iGrade;}}

    private List<Card> m_CardList;
    public List<Card> CardList{get{return m_CardList;}}

    
    public void GetData()
    {
        
    }
    
}
