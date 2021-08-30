using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class EnemyManager : Singleton<EnemyManager> {

    public Enemy Target;
    private int StageLevel;
    public Transform m_EnemyParty;

    public Enemy EnemyOrigin;
    public List<Enemy> m_EnemyList;

    private Queue<Enemy> m_EnemyQueue;

    private JSONNode m_Node;
    public TextAsset  m_TextAsset;
    private int m_iSelectModIndex;

    private List<Card> m_TempCardList;



    private List<int> m_ShapeList;
    private Grade m_TempGrade;
    private int m_TempCardLevel;
    private int m_TempCardShape;

    private int iEnemyCount;

    private void Awake()
    {
        m_Node = JSON.Parse(m_TextAsset.text);
        m_EnemyList = new List<Enemy>();
        m_EnemyQueue = new Queue<Enemy>();
        m_TempCardList = new List<Card>();
        m_ShapeList = new List<int>();

    }

    public void SummonEnemy(int level)
    {
        StageLevel = level;
        iEnemyCount = m_Node["Wave"][StageLevel]["Count"].AsInt;
        print("StageLevel : " + StageLevel + " / en : " + iEnemyCount) ;

        if(iEnemyCount > 1)
        {
            StartCoroutine(Summon());
        }
        else
        {

        }
    }



    IEnumerator Summon()
    {
        print("suoommon");

        while (iEnemyCount > 0)
        {
            GetData();   
            yield return new WaitForSeconds(2f);
//            print("1EnemyCount  " + iEnemyCount);
            AppearEnemy();
            iEnemyCount--;
        }
    }

    public Enemy GetEnemy()
    {
        Enemy go = null;

        if(m_EnemyQueue.Count <= 0)
        {
            go = Instantiate(EnemyOrigin, Vector3.zero, Quaternion.identity);
            go.gameObject.SetActive(false);
            go.transform.SetParent(m_EnemyParty);
            go.transform.localScale = Vector3.one;
            m_EnemyQueue.Enqueue(go);
        }

        go = m_EnemyQueue.Dequeue();

        return go;
    }

    // public void GetData(Enemy e )
    // {

    //     //e.Init()
    // }

    public void SetEnemy(Enemy enemy)
    {
        m_EnemyQueue.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    private void AppearEnemy()
    {
        m_EnemyList.Add(GetEnemy());
        m_EnemyList[m_EnemyList.Count -1].Init(m_TempGrade, m_TempCardList);// CardShape.Clover, CardLevel.A);
        m_EnemyList[m_EnemyList.Count -1].gameObject.SetActive(true);


        // for (int i = 0; i < m_EnemyList.Count; i++)
        // {
        //     if (EnemyList[i].gameObject.activeInHierarchy == false)
        //     {
        //         index = i;
        //         break;
        //     }
        // }

        //EnemyList[index].EnemyReset();
        //EnemyList[index].SetParameter(1, 1f,0,0,0);
        
    }

    private void GetData()
    {
        if(m_Node["Wave"][StageLevel]["Mob"].Count > 1)
        {
            float[] fRatio = new float[m_Node["Wave"][StageLevel]["Mob"].Count];
            for(int i = 0; i < fRatio.Length; i++ )
            {
                fRatio[i] = m_Node["Wave"][StageLevel]["Mob"][i]["Ratio"].AsFloat;
                if(i > 0)
                {
                    fRatio[i] += fRatio[i-1];
                }
            }

            float ran = UnityEngine.Random.Range(0, 100f);

            for(int i = 0; i < fRatio.Length; i++)
            {
                if(ran <= fRatio[i])
                {
                    m_iSelectModIndex = i;
                    break;
                }
            }
            //m_iSelectModIndex = Random.Range(0, m_Node["Wave"][StageLevel]["Mob"].Count -1);
        }
        else
        {
            m_iSelectModIndex = 0;
        }

        string grade = m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["Grade"];
        // (Enum형식)Enum.Parse(typeof(Enum형식), 문자열);
        m_TempCardList.Clear();
        Card card;
        switch((Grade)Enum.Parse(typeof(Grade), grade))
        {
            case Grade.Top:
            m_TempGrade = Grade.Top;
            card = new Card();
            m_TempCardShape = UnityEngine.Random.Range(0,4);
            
            card.Init(m_TempCardShape,ReturnLevel(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"]));
            
            m_TempCardList.Add(card);
            break;


            case Grade.OnePair:
            m_TempGrade = Grade.OnePair;
            InPutShape();

            for(int i = 0; i < 2; i++)
            {
                card = new Card();
                m_TempCardShape = m_ShapeList[UnityEngine.Random.Range(0,m_ShapeList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"]));
                m_TempCardList.Add(card);
                m_ShapeList.RemoveAt(m_TempCardShape);
            }
            break;

            case Grade.TwoPair:
            m_TempGrade = Grade.TwoPair;
            InPutShape();
            break;
            case Grade.Triple:
            m_TempGrade = Grade.Triple;
            InPutShape();
            for(int i = 0; i < 3; i++)
            {
                card = new Card();
                m_TempCardShape = m_ShapeList[UnityEngine.Random.Range(0,m_ShapeList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"]));
                m_TempCardList.Add(card);
                m_ShapeList.RemoveAt(m_TempCardShape);
            }
            break;
            case Grade.Straight:
            m_TempGrade = Grade.Straight;
            
            break;
            case Grade.Flush:
            m_TempGrade = Grade.Flush;
            
            break;
            case Grade.FullHouse:
            m_TempGrade = Grade.FullHouse;
            
            break;
            
            case Grade.FourCard:
            m_TempGrade = Grade.FourCard;
            for(int i = 0; i < 4; i++)
            {
                card = new Card();
                m_TempCardShape = i;
                card.Init(m_TempCardShape, ReturnLevel(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"]));
                m_TempCardList.Add(card);
            }
            break;
            case Grade.StraightFlush:
            m_TempGrade = Grade.StraightFlush;
            
            break;

        }

    }

    public void InPutShape()
    {
        for(int i = 0; i < 4; i++)
        {
            m_ShapeList.Add(i);
        }
    }

    public CardLevel ReturnLevel(string str)
    {
        CardLevel level = CardLevel.Two;

        if(int.TryParse(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"], out int t))
        {
            level = (CardLevel)(t -2);
        }
        else
        {
            switch (m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardLevel"])
            {
                case "J":
                level = CardLevel.J;
                break;
                case "QJ":
                level = CardLevel.Q;
                break;
                case "K":
                level = CardLevel.K;
                break;
                case "A":
                level = CardLevel.A;
                break;
            }
        }

        return level;
    }
    // public CardShape GetShape()
    // {
    //     if(m_Node["Wave"][StageLevel]["Mob"][m_iSelectModIndex]["CardShape"] == "Random")
    //     {
    //         m_TempCardShape = (CardShape)UnityEngine.Random.Range(0,4);
    //     } 
    //     else
    //     {
            
    //     }
    //     return m_TempCardShape;
    // }

   



    
}
