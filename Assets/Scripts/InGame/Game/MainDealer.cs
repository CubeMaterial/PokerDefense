using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDealer : Singleton<MainDealer> 
{
    private Enemy mTarget;

    private float fDmg;
    private float fAddDmg;
    private float fBossDmg;

    private float fAttackDelay;
    private float fTime;

    public Missile mMissile;
	
	private void Awake () 
    {
        fAttackDelay = 1f;
        fDmg = 1f;
        fAddDmg = 0f;
        fBossDmg = 0f;
    }
	
	
    private void Update()
    {
        if (GameManager.instance.GetGameProcessing() == false)
            return;

        if (EnemyManager.instance.Target != null)
        {
            mTarget = EnemyManager.instance.Target;
        }
        else
        { 
            if(EnemyManager.instance.mEnemyList.Count > 0)
            {
                mTarget = EnemyManager.instance.mEnemyList[0];
             }
            else
            {
                mTarget = null;
            }
        }

        if(mTarget != null)
        { 
            if(fTime <= 0f)
            {
                Attack();
            }
        }

        if(fTime >= 0)
        { 
            fTime -= TimeManager.instance.TIMEDELTA;
        }
    }

    private void Attack()
    {
        fTime = fAttackDelay;
    }

    public void AddAtk(int hand)
    {
        fDmg += hand;

        if(hand == 21)
        {
            fAddDmg += 1f;
            fBossDmg += 3f;
        }
        else if(hand > 15)
        {
            fAddDmg += (((float)hand - 15f) * 0.1f);
        }

    }

    public float ReturnDmg()
    {
        return fDmg;
    }


}
