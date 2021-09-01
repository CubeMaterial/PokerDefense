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



    private List<int> m_TempIntList;
    // private Grade m_TempGrade;
    private string m_TempGradeIndex;

    private string m_TempIndex;
    private int m_TempCardLevel;
    private int m_TempCardShape;

    private string m_TempEnemyType;

    private int iEnemyCount;

    private void Awake()
    {
        m_Node = JSON.Parse(m_TextAsset.text);
        m_EnemyList = new List<Enemy>();
        m_EnemyQueue = new Queue<Enemy>();
        m_TempCardList = new List<Card>();
        m_TempIntList = new List<int>();

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
            if(iEnemyCount == 1)
            {
                m_TempEnemyType = "Final";
            }
            else
            {
                m_TempEnemyType = "Mob"; 
            }
            GetData(m_TempEnemyType);   
            yield return new WaitForSeconds(1f);
            // AppearEnemy();
            iEnemyCount--;
            yield return new WaitForSeconds(1f);
//            print("1EnemyCount  " + iEnemyCount);
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
        m_EnemyList[m_EnemyList.Count -1].Init((Grade)Enum.Parse(typeof(Grade), m_TempGradeIndex), m_TempCardList);// CardShape.Clover, CardLevel.A);
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

    private void GetData(string enemyType)
    {
        if(enemyType == "Mob")
        {
            if(m_Node["Wave"][StageLevel][enemyType].Count > 1)
            {
                float[] fRatio = new float[m_Node["Wave"][StageLevel][enemyType].Count];
                for(int i = 0; i < fRatio.Length; i++ )
                {
                    fRatio[i] = m_Node["Wave"][StageLevel][enemyType][i]["Ratio"].AsFloat;
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
            m_TempGradeIndex = m_Node["Wave"][StageLevel][enemyType][m_iSelectModIndex]["Grade"];
            m_TempIndex = m_Node["Wave"][StageLevel][enemyType][m_iSelectModIndex]["CardLevel"];
        }
        else
        { 
            //m_iSelectModIndex = 0;
            m_TempGradeIndex = m_Node["Wave"][StageLevel]["Final"]["Grade"];
            m_TempIndex = m_Node["Wave"][StageLevel]["Final"]["CardLevel"];
        }


        print("m_TempGradeIndex : " + m_TempGradeIndex + " / index : " + m_TempIndex);

        // BuildHands();
        // if((grade == (int)Grade.Straight || grade == (int)Grade.StraightFlush) && index < 6)
        // {
        //     print("pumble");
        // }
        // else
        // {

        // }
        // (Enum형식)Enum.Parse(typeof(Enum형식), 문자열);
       m_TempCardList.Clear();
        Card card;
        switch((Grade)Enum.Parse(typeof(Grade), m_TempGradeIndex))
        {
            ///////// Top
            case Grade.Top:
            // m_TempGrade = Grade.Top;
            card = new Card();
            m_TempCardShape = UnityEngine.Random.Range(0,4);
            
            card.Init(m_TempCardShape,ReturnLevel(m_TempIndex));
            
            m_TempCardList.Add(card);

            print("shape : " +m_TempCardList[m_TempCardList.Count -1].ReturnCardShape() + " / level : " + m_TempCardList[m_TempCardList.Count -1].ReturnCardLevel() );
            break;
            ///////// OnePair
            case Grade.OnePair:
            // m_TempGrade = Grade.OnePair;
            InPutShape();

            for(int i = 0; i < 2; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            break;
            ///////// TwoPair
            case Grade.TwoPair:
            // m_TempGrade = Grade.TwoPair;
            InPutShape();
            for(int i = 0; i < 2; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }

            InPutLevel(int.Parse(m_TempIndex));
            m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)];

            InPutShape();
            for(int i = 0; i < 2; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempCardLevel.ToString()));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            
            break;
            ///////// Triple
            case Grade.Triple:
            // m_TempGrade = Grade.Triple;
            InPutShape();
            for(int i = 0; i < 3; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }
            break;
            ///////// Straight
            case Grade.Straight:
            // m_TempGrade = Grade.Straight;
            for(int i = 0; i < 5; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)]; 
                card.Init(m_TempCardShape, ReturnLevel((int.Parse(m_TempIndex) - i).ToString()));
                m_TempCardList.Add(card);
            }

            int[] arr = new int[4];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
            for(int i = 0; i < m_TempCardList.Count; i++)
            {
                arr[(int)(m_TempCardList[i].ReturnCardShape())]++;
            }

            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == 5)
                {
                    m_TempCardList.RemoveAt(m_TempCardList.Count -1);
                    GetData(m_TempEnemyType);
                    break;
                }
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            
            break;
            ///////// Flush
            case Grade.Flush:
            // m_TempGrade = Grade.Flush;
            InPutLevel(int.Parse(m_TempIndex));
            m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)];
            for(int i = 0; i < 5; i++)
            {
                card = new Card();
                m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, m_TempCardLevel);
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardLevel);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);

            
            break;
            ///////// FullHouse
            case Grade.FullHouse:
            // m_TempGrade = Grade.FullHouse;
            
            InPutShape();
            for(int i = 0; i < 3; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }

            InPutLevel(int.Parse(m_TempIndex));
            m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)];

            InPutShape();
            for(int i = 0; i < 2; i++)
            {
                card = new Card();
                m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
                card.Init(m_TempCardShape, ReturnLevel(m_TempCardLevel.ToString()));
                m_TempCardList.Add(card);
                m_TempIntList.RemoveAt(m_TempCardShape);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            break;
            ///////// FourCard
            case Grade.FourCard:
            // m_TempGrade = Grade.FourCard;
            for(int i = 0; i < 4; i++)
            {
                card = new Card();
                m_TempCardShape = i;
                card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
                m_TempCardList.Add(card);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            break;
            ///////// StraightFlush
            case Grade.StraightFlush:
            // m_TempGrade = Grade.StraightFlush;
            m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)]; 
            
            for(int i = 0; i < 5; i++)
            {
                card = new Card();
                card.Init(m_TempCardShape, ReturnLevel((int.Parse(m_TempIndex) - i).ToString()));
                m_TempCardList.Add(card);
            }
            m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);// .Sort();
            break;

        }

        //appear
         m_EnemyList.Add(GetEnemy());
         print("m_TempGradeIndex : " + m_TempGradeIndex + " / list" + m_TempCardList.Count);
        m_EnemyList[m_EnemyList.Count -1].Init((Grade)Enum.Parse(typeof(Grade), m_TempGradeIndex), m_TempCardList);// CardShape.Clover, CardLevel.A);
        m_EnemyList[m_EnemyList.Count -1].gameObject.SetActive(true);
    }

    private void BuildHands()
    {
        // m_TempCardList.Clear();
        // Card card;
        // switch((Grade)Enum.Parse(typeof(Grade), m_TempGradeIndex))
        // {
        //     ///////// Top
        //     case Grade.Top:
        //     // m_TempGrade = Grade.Top;
        //     card = new Card();
        //     m_TempCardShape = UnityEngine.Random.Range(0,4);
            
        //     card.Init(m_TempCardShape,ReturnLevel(m_TempIndex));
            
        //     m_TempCardList.Add(card);

        //     print("shape : " +m_TempCardList[m_TempCardList.Count -1].ReturnCardShape() + " / level : " + m_TempCardList[m_TempCardList.Count -1].ReturnCardLevel() );
        //     break;
        //     ///////// OnePair
        //     case Grade.OnePair:
        //     // m_TempGrade = Grade.OnePair;
        //     InPutShape();

        //     for(int i = 0; i < 2; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
        //     break;
        //     ///////// TwoPair
        //     case Grade.TwoPair:
        //     // m_TempGrade = Grade.TwoPair;
        //     InPutShape();
        //     for(int i = 0; i < 2; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }

        //     InPutLevel(int.Parse(m_TempIndex));
        //     m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)];

        //     InPutShape();
        //     for(int i = 0; i < 2; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempCardLevel.ToString()));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            
        //     break;
        //     ///////// Triple
        //     case Grade.Triple:
        //     // m_TempGrade = Grade.Triple;
        //     InPutShape();
        //     for(int i = 0; i < 3; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }
        //     break;
        //     ///////// Straight
        //     case Grade.Straight:
        //     // m_TempGrade = Grade.Straight;
        //     for(int i = 0; i < 5; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)]; 
        //         card.Init(m_TempCardShape, ReturnLevel((int.Parse(m_TempIndex) - i).ToString()));
        //         m_TempCardList.Add(card);
        //     }

        //     int[] arr = new int[4];
        //     for(int i = 0; i < arr.Length; i++)
        //     {
        //         arr[i] = 0;
        //     }
        //     for(int i = 0; i < m_TempCardList.Count; i++)
        //     {
        //         arr[(int)(m_TempCardList[i].ReturnCardShape())]++;
        //     }

        //     for(int i = 0; i < arr.Length; i++)
        //     {
        //         if(arr[i] == 5)
        //         {
        //             m_TempCardList.RemoveAt(m_TempCardList.Count -1);
        //             GetData(m_TempEnemyType);
        //             break;
        //         }
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
            
        //     break;
        //     ///////// Flush
        //     case Grade.Flush:
        //     // m_TempGrade = Grade.Flush;
        //     InPutLevel(int.Parse(m_TempIndex));
        //     m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)];
        //     for(int i = 0; i < 5; i++)
        //     {
        //         card = new Card();
        //         m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, m_TempCardLevel);
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardLevel);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);

            
        //     break;
        //     ///////// FullHouse
        //     case Grade.FullHouse:
        //     // m_TempGrade = Grade.FullHouse;
            
        //     InPutShape();
        //     for(int i = 0; i < 3; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }

        //     InPutLevel(int.Parse(m_TempIndex));
        //     m_TempCardLevel = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)];

        //     InPutShape();
        //     for(int i = 0; i < 2; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,m_TempIntList.Count)]; 
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempCardLevel.ToString()));
        //         m_TempCardList.Add(card);
        //         m_TempIntList.RemoveAt(m_TempCardShape);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
        //     break;
        //     ///////// FourCard
        //     case Grade.FourCard:
        //     // m_TempGrade = Grade.FourCard;
        //     for(int i = 0; i < 4; i++)
        //     {
        //         card = new Card();
        //         m_TempCardShape = i;
        //         card.Init(m_TempCardShape, ReturnLevel(m_TempIndex));
        //         m_TempCardList.Add(card);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);
        //     break;
        //     ///////// StraightFlush
        //     case Grade.StraightFlush:
        //     // m_TempGrade = Grade.StraightFlush;
        //     m_TempCardShape = m_TempIntList[UnityEngine.Random.Range(0,4)]; 
            
        //     for(int i = 0; i < 5; i++)
        //     {
        //         card = new Card();
        //         card.Init(m_TempCardShape, ReturnLevel((int.Parse(m_TempIndex) - i).ToString()));
        //         m_TempCardList.Add(card);
        //     }
        //     m_TempCardList = GameManager.instance.ReturnSortList(m_TempCardList);// .Sort();
        //     break;

        // }
    }

    public void InPutShape()
    {
        m_TempIntList.Clear();
        for(int i = 0; i < 4; i++)
        {
            m_TempIntList.Add(i);
        }
    }

    public void InPutLevel(int k = 0)
    {
        m_TempIntList.Clear();
        for(int i = 0; i < 13 - k; i++)
        {
            m_TempIntList.Add(i);
        }
    }

    public CardLevel ReturnLevel(string str)
    {
        CardLevel level = CardLevel.Two;

        if(int.TryParse(str, out int t))
        {
            level = (CardLevel)(t - 2);
        }
        else
        {
            switch (str)
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
