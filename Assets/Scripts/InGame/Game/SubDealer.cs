using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDealer : MonoBehaviour 
{
    private Enemy mTarget;

    public float[] fStat;
    public int[] iMainAbility;
    public int[] iSubAbility;

    private float fDmg;
    private float fFixedDmg;
    private float fAddDmg;
    private float fBossDmg;

    private float fAttackDelay;
    private float fTime;

    public Missile mMissile;

    private Grade iGrade;
    private CardLevel iLevel;
    private CardLevel iSubLevel;

    public void ActiveDealer()
    {
        if (SubDeckMaster.instance.ReturnSelectGrade() > 0)
        {
            iGrade = (Grade)SubDeckMaster.instance.ReturnSelectGrade();
            iLevel = (CardLevel)SubDeckMaster.instance.ReturnSelectLevel();
            iSubLevel = (CardLevel)SubDeckMaster.instance.ReturnSelectSubLevel(); 
        }   
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
            if (EnemyManager.instance.m_EnemyList.Count > 0)
            {
                mTarget = EnemyManager.instance.m_EnemyList[0];
            }
            else
            {
                mTarget = null;
            }
        }

        if (mTarget != null)
        {
            if (fTime <= 0f)
            {
                Attack();
            }
        }

        if (fTime >= 0)
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

        if (hand == 21)
        {
            fAddDmg += 1f;
            fBossDmg += 3f;
        }
        else if (hand > 15)
        {
            fAddDmg += (((float)hand - 15f) * 0.1f);
        }

    }

}
