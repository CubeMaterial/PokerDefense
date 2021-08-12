using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {

    public Enemy Target;
    private int StageLevel;

    public Enemy EnemyOrigin;
    public List<Enemy> mEnemyList;
    public Enemy[] EnemyList;


    private void Awake()
    {
        EnemyList = new Enemy[100];
    }

    public void SummonEnemy(int level)
    {
        StageLevel = level;
        StartCoroutine(Summon());
    }

    IEnumerator Summon()
    {
        int EnemyCount = 0;
        while (EnemyCount < 0)
        {
            yield return new WaitForSeconds(1f);
            EnemyCount++;
        }
    }

    private void AppearEnemy()
    {
        int index = 0;
        for (int i = 0; i < EnemyList.Length; i++)
        {
            if (EnemyList[i].gameObject.activeInHierarchy == false)
            {
                index = i;
                break;
            }
        }

        //EnemyList[index].EnemyReset();
        //EnemyList[index].SetParameter(1, 1f,0,0,0);
        
    }

   



    
}
