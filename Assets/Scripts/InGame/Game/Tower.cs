using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private int m_iTowerIndex;
    private Image m_TowerBase;
    public Image m_TowerSprite;
    public Transform FirePoint;

    public Enemy Target;

    private int TargetCount;
    private float AttackPoint;
    private float AttackSpeed;
    private float Range;

    private int MaxTargetCount;

    private string TowerLevel;

    private float Accurate;
    private float CriticalRate;
    private float CriticalValue;

    private bool IsAttackSuccess;
    

    private string isLock = "L";
    private bool isAttack = false;
    private void Awake()
    {
        m_iTowerIndex = int.Parse(gameObject.name.Split('_')[1]); 
        m_TowerBase = GetComponent<Image>();
        TargetCount = 0;
        TowerLevel = "None";
        Accurate = 100;
        CriticalRate = 0;
        CriticalValue = 1.5f;
    }

    private void Update()
    {
        // 타겟이 있을 경우
        if (Target != null)
        {
            // 매직 타워 외에는 타겟을 향해 방향을 움직임

            if (isAttack == false)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;

        /*if (100f - Accurate + Target)
        {

        }*/

        yield return new WaitForSeconds(AttackSpeed);

        if (Target != null)
        {
            StartCoroutine(Attack());
        }
        else
        {
            isAttack = false;
            Idle();
        }
    }

    private void Idle()
    {

    }
   
    public void ClickTower()
    {
        //TowerManager.instance.ClickTower(TowerIndex, Type);    
    }

    public void SetLock(string str)
    {
        if (str == "L")
        {
            isLock = "L";
        //    print("this tower is lock");
        }
        else if (str == "U")
        {
            isLock = "U";
        //    print("this tower is unlock");
        }
        else
        {
            print("Bazinga");
        }

        TowerInit();
    }

    private void TowerInit()
    {
        if (isLock == "L")
        {
            m_TowerSprite.gameObject.SetActive(false);
            FirePoint.gameObject.SetActive(false);
        }
        else
        {
            print("unlock");
        }
    }

    // 타겟에 받은 에너미를 집어넣음
    public void PutEnemyTarget(Enemy e)
    {
        /*if (Targets.Length > TargetCount)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                if (Targets[i] == null)
                {
                    Targets[i] = e;
                    
                    TargetCount++;
                    print("put / i : " + i + " / name : " + e.gameObject.name);
                    break;
                }
            }
        }*/
        if (Target == null)
        {
            Target = e;
        }
    }

    // 타겟에 받은 에너미를 뺌
    public void PullEnemyTarget(Enemy e)
    {
        /*if (TargetCount > 0)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                if (Targets[i] == e)
                {
                    Targets[i] = null;
                    TargetCount--;
                    print("pull");
                    break;
                }
            }
        }*/

        if (Target != null)
        {
            Target = null;
        }
    }

    public void SetTower()
    {
        //Type = t;

        //TowerSprite.spriteName = "Tower_" + Type;
        //TowerSprite.gameObject.SetActive(true);
        //TowerRange.gameObject.SetActive(true);
        //FirePoint.gameObject.SetActive(true);

        //AttackPoint = TowerManager.instance.ReturnTowerStat(Type, TowerStat.AttackPoint);
        //AttackSpeed = TowerManager.instance.ReturnTowerStat(Type, TowerStat.AttackSpeed);
        //Range = TowerManager.instance.ReturnTowerStat(Type, TowerStat.AttackRange);
        //Accurate = TowerManager.instance.ReturnTowerStat(Type, TowerStat.Accurate);
        //CriticalRate = TowerManager.instance.ReturnTowerStat(Type, TowerStat.CriticalRate);
        //CriticalValue = TowerManager.instance.ReturnTowerStat(Type, TowerStat.CriticalValue);

        //if (t == TowerType.Common)
        //{
        //    TMT = TowerMajorType.Common;
        //}
        //else if (t == TowerType.Archer || t == TowerType.ArrowLv1 || t == TowerType.ArrowLv2 || t == TowerType.ArrowLv3 || t == TowerType.Machinegun || t == TowerType.Ranger || t == TowerType.Sniper)
        //{
        //    TMT = TowerMajorType.Arrow;
        //}
        //else if (t == TowerType.MeleeLv1 || t == TowerType.MeleeLv2 || t == TowerType.MeleeLv3 || t == TowerType.Weapon || t == TowerType.Fighter || t == TowerType.Hammer || t == TowerType.Swordman)
        //{
        //    TMT = TowerMajorType.Melee;
        //}
        //else
        //{
        //    TMT = TowerMajorType.Magic;
        ////    MagicSshere.SetActive(true);
        //}

        ////print("ap : " + AttackPoint + " / as : " + AttackSpeed + " / r : " + Range);

            //TowerRange.width = TowerRange.height = (int)(Range * TowerSprite.width + TowerSprite.width / 2);

    }

  //  public int Return
	
}
