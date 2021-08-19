using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {

    public Enemy Target;
    private int StageLevel;
    public Transform m_EnemyParty;

    public Enemy EnemyOrigin;
    public List<Enemy> m_EnemyList;

    private Queue<Enemy> m_EnemyQueue;



    private void Awake()
    {
        m_EnemyList = new List<Enemy>();
        m_EnemyQueue = new Queue<Enemy>();

    }

    public void SummonEnemy(int level)
    {
        StageLevel = level;
        StartCoroutine(Summon());
    }

    IEnumerator Summon()
    {
        print("suoommon");
        int iEnemyCount = 20;
        while (iEnemyCount > 0)
        {
            yield return new WaitForSeconds(1f);
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

    public void SetEnemy(Enemy enemy)
    {
        m_EnemyQueue.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    private void AppearEnemy()
    {
        m_EnemyList.Add(GetEnemy());
        m_EnemyList[m_EnemyList.Count -1].gameObject.SetActive(true);
        m_EnemyList[m_EnemyList.Count -1].Init(CardShape.Clover, CardLevel.A);


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

   



    
}
