using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public UISprite sprite;

    public Transform master;
    public Enemy target;

    private Vector3 dir;
    private bool isAttack = false;

    private float attackSpeed = 2f;

    private float Dmg;

    private float dy, dx, rad;
    private bool isTimeCheck = false;
    private float time = 0f;

    private float distance;
    private Vector3 tempPosition;
    private Vector3 enemyPosition;


    public void Attack(Enemy enemy, int level, float dmg)
    {
        time = 0f;
        isTimeCheck = false;
        target = enemy;
        Dmg = dmg;

        /*switch (type)
        {
            case TowerType.Card_Type_Skull:
                sprite.spriteName = "Skull_Attack_Effect1";
                break;


            case CardType.Card_Type_Boom:
                if (level < 3)
                {
                    sprite.spriteName = "Boom_Attack_Effect1";
                }
                else if (level >= 3 && level < 7)
                {
                    sprite.spriteName = "Boom_Attack_Effect2";
                }
                else
                {
                    sprite.spriteName = "Boom_Attack_Effect3";
                }
                break;

            case CardType.Card_Type_Devil:
                sprite.spriteName = "Devil_Attack_Effect1";
                break;

            case CardType.Card_Type_Ice:
                sprite.spriteName = "Ice_Attack_Effect1";

                break;

            case CardType.Card_Type_Hydra:
                sprite.spriteName = "Hydra_Attack_Effect1";
                break;
        }*/

        isAttack = true;
    }

    void Update()
    {
        if (isAttack == false)
            return;

        if (target == null)
        {
            print("target is null");
            isAttack = false;
            tempPosition = Vector2.zero;
            gameObject.SetActive(false);
            return;
        }

        if (target == null && GameManager.instance.GetGameProcessing() == false)
        {
            print("target is null. game is not process.");
            isAttack = false;
            tempPosition = Vector2.zero;
            gameObject.SetActive(false);
            return;
        }

        if (target == null && GameManager.instance.GetGameProcessing() == true)
        {
            enemyPosition = tempPosition;
        }
        else
        {
            enemyPosition = target.transform.position;
        }


        dir = (enemyPosition - gameObject.transform.position).normalized;

        distance = Vector2.Distance(enemyPosition, gameObject.transform.position);

        if (target.ReturnEnemyState() != EnemyState.Move && distance <= (Time.deltaTime * attackSpeed * GlobalValue.instance.m_Speed))
        {

            if (tempPosition == transform.position)
            {
                isAttack = false;
                tempPosition = Vector2.zero;
                target = null;
                gameObject.SetActive(false);
            }
            else
            {
                tempPosition = transform.position;
            }
            //	print ("Target : " + target.transform.position + " tra  : " + transform.position
            //	        + distance);
        }
        else
        {
            gameObject.transform.position += dir * Time.deltaTime * attackSpeed * GlobalValue.instance.m_Speed;

            target.transform.position = target.transform.position;

            /*if (type != CardType.Card_Type_Boom)
            {
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, GetAngle(gameObject.transform.position.x, gameObject.transform.position.y, target.transform.position.x, target.transform.position.y) + 180f);
            }

            if (target.ReturnEnemyState() == EnemyState.Die && target.transform.localPosition == gameObject.transform.localPosition)
            {
                isAttack = false;
                tempPosition = Vector2.zero;
                target = null;
                gameObject.SetActive(false);
            }*/
        }

        //	monsterPrevPosition = target.transform;
    }

    float GetAngle(float x1, float y1, float x2, float y2)
    {
        dx = x2 - x1;
        dy = y2 - y1;

        rad = Mathf.Atan2(dy, dx) * 180f / Mathf.PI;

        return rad;
    }

    void OnTriggerStay(Collider co)
    {
        DamageToTarget();
        co = null;
    }

    public void DamageToTarget()
    {

        /*switch (type)
        {
            case CardType.Card_Type_Skull:
                target.Damage(cardDmg, type);
                target.HitPoisonAttack();
                break;


            case CardType.Card_Type_Boom:
                GameManager.instance.ShakeScreen();
                target.SummonKillerQueen(cardDmg);
                break;

            case CardType.Card_Type_Devil:
                target.Damage(cardDmg, type);
                break;

            case CardType.Card_Type_Ice:
                target.Damage(cardDmg, type);
                target.SummonFrostNova();
                break;

            case CardType.Card_Type_Hydra:
                target.Damage(cardDmg, type);
                break;
        }
        */
        isAttack = false;
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        isAttack = false;
        gameObject.SetActive(false);
    }
}
