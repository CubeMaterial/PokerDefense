using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour 
{

    private Grade m_Grade;
    private List<Card> m_EnemyCardList;

    private int iDebuff;

    private float[,] mEnemyStatus;

    public Image[] m_CardImage;


    private EnemyDebuff m_EnemyDebuff;
    private EnemyBuff m_EnemyBuff;

    private int m_iShieldPoint;

    private int[] mDebuffLevel;

    public Image m_EnemySprite;

    public Transform GoalPosition;
    public GameObject EnemyCore;

    private EnemyState m_EnemyState;

    public EnemyMoveState m_EnemyMoveState;



    private void Awake()
    {
    //     m_EnemyCardList = new List<Card>();
    //     //mDebuffLevel = new int[(int)EnemyDebuff.End];
    //     mEnemyStatus = new float[(int)Enemy_Status.End, (int)Status_State.End]; 
    //     print("mEnemyStatus");
        // Respawn();
    }

    

    public void Init(Grade grade, List<Card> m_list)
    {
        if(m_EnemyCardList == null)
        {
            m_EnemyCardList = new List<Card>();
            mEnemyStatus = new float[(int)Enemy_Status.End, (int)Status_State.End]; 
            // print("mEnemyStatus");
        }

        m_EnemyCardList = m_list;
        m_Grade = grade;

        SetDefaultParameter();
        for(int i = 0; i < m_list.Count; i++)
        {
            SetParameter(i);
        }
        m_EnemyState = EnemyState.Ready;
        m_EnemyMoveState = EnemyMoveState.AtPoint0;

        foreach(Image image in m_CardImage)
        {
            image.gameObject.SetActive(false);
        }



        
        if(m_EnemyCardList.Count == 5)
        {
            m_CardImage[0].transform.localPosition = new Vector3(-40f, 0f, 0f);
            m_CardImage[1].transform.localPosition = new Vector3(-20f, 0f, 0f);
            m_CardImage[2].transform.localPosition = new Vector3(0f, 0f, 0f);
            m_CardImage[3].transform.localPosition = new Vector3(20f, 0f, 0f);
            m_CardImage[4].transform.localPosition = new Vector3(40f, 0f, 0f);
        }
        else if(m_EnemyCardList.Count == 4)
        {
            m_CardImage[0].transform.localPosition = new Vector3(-30f, 0f, 0f);
            m_CardImage[1].transform.localPosition = new Vector3(-10f, 0f, 0f);
            m_CardImage[2].transform.localPosition = new Vector3(10f, 0f, 0f);
            m_CardImage[3].transform.localPosition = new Vector3(30f, 0f, 0f);
        }
        else if(m_EnemyCardList.Count == 3)
        {
            m_CardImage[0].transform.localPosition = new Vector3(-20f, 0f, 0f);
            m_CardImage[1].transform.localPosition = new Vector3(0f, 0f, 0f);
            m_CardImage[2].transform.localPosition = new Vector3(20f, 0f, 0f);
        }
        else if(m_EnemyCardList.Count == 2)
        {
            m_CardImage[0].transform.localPosition = new Vector3(-10f, 0f, 0f);
            m_CardImage[1].transform.localPosition = new Vector3(10f, 0f, 0f);
        }
        else
        { 
            m_CardImage[0].transform.localPosition = new Vector3(0f, 0f, 0f);

        }

        for(int i = 0; i < m_EnemyCardList.Count; i++)
        {
            m_CardImage[i].sprite = DeckMaster.instance.ReturnCardSprite(m_EnemyCardList[i].ReturnCardShape(), m_EnemyCardList[i].ReturnCardLevel());
            m_CardImage[i].gameObject.SetActive(true);
        }

        Respawn();

    }

    private void SetDefaultParameter()
    {

        
        mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] =
        10 * GameDataManager.instance.ReturnCurrentLevel();

        mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] = 10f;

        mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Current] = 0f;

        mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Current] = 1;

        mEnemyStatus[(int)Enemy_Status.ReduceDamageRate, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.ReduceDamageRate, (int)Status_State.Current] = 0f;
    }

    private void SetParameter(int i)
    {

        switch(m_EnemyCardList[i].ReturnCardLevel())
        {
            case CardLevel.Two:
            break;    
            case CardLevel.Three:
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] =
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] =
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] * 1.5f;
            break;
            case CardLevel.Four:
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] =
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] =
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] * 1.5f;
            break;
            case CardLevel.Five:
                mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Origin] += 10f;
                mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Current] += 10f;
            break;
            
            case CardLevel.Six:
                mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Origin] += (((float)GameDataManager.instance.ReturnCurrentLevel()/10f) + 1f);
                mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Current] += (((float)GameDataManager.instance.ReturnCurrentLevel()/10f) + 1f);
            break;
            case CardLevel.Seven:
                mEnemyStatus[(int)Enemy_Status.ReduceDamageRate, (int)Status_State.Origin] += (((float)GameDataManager.instance.ReturnCurrentLevel()/5f) + 1f);
                mEnemyStatus[(int)Enemy_Status.ReduceDamageRate, (int)Status_State.Current] += (((float)GameDataManager.instance.ReturnCurrentLevel()/5f) + 1f);
            break;
            case CardLevel.Eight:
                switch(m_EnemyCardList[i].ReturnCardShape())
                {
                    case CardShape.Heart:
                        m_EnemyBuff |= EnemyBuff.ResistBleeding;
                    break;
                    case CardShape.Diamond:
                        m_EnemyBuff |= EnemyBuff.ResistBurning; 
                    break;
                    case CardShape.Clover:
                        m_EnemyBuff |= EnemyBuff.ResistPoison;
                    break;
                    case CardShape.Spade:
                        m_EnemyBuff |= EnemyBuff.ResistAcid;
                    break;
                }
            break;
            case CardLevel.Nine:
              switch(m_EnemyCardList[i].ReturnCardShape())
                {
                    case CardShape.Heart:
                        m_EnemyBuff |= EnemyBuff.ResistSleep;
                    break;
                    case CardShape.Diamond:
                        m_EnemyBuff |= EnemyBuff.ResistFreezing; 
                    break;
                    case CardShape.Clover:
                        m_EnemyBuff |= EnemyBuff.ResistSlow;
                    break;
                    case CardShape.Spade:
                        m_EnemyBuff |= EnemyBuff.ResistStun;
                    break;
                }
            break;
            case CardLevel.Ten:
                switch(m_EnemyCardList[i].ReturnCardShape())
                {
                    case CardShape.Heart:
                        m_EnemyBuff |= EnemyBuff.HolyShield;
                    break;
                    case CardShape.Diamond:
                        m_EnemyBuff |= EnemyBuff.WindShield; 
                    break;
                    case CardShape.Clover:
                        m_EnemyBuff |= EnemyBuff.Barrier;
                    break;
                    case CardShape.Spade:
                        m_EnemyBuff |= EnemyBuff.MagicBarrier;
                    break;
                }
            break;
            case CardLevel.J:
            break;
            case CardLevel.Q:
            break;
            case CardLevel.K:
            break;
            case CardLevel.A:

            break;
        }

        switch(m_EnemyCardList[i].ReturnCardShape())
        {
            case CardShape.Heart:
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] =
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] =
                mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] * 2f;
            break;    
            case CardShape.Diamond:
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] =
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] =
                mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] * 2.5f;
            break;
            case CardShape.Clover:
                mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Origin] += 15f;
                mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Current] += 15f;
            break;
            case CardShape.Spade:
                mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Origin] += ((float)m_EnemyCardList[i].ReturnCardLevel()+2f);
                mEnemyStatus[(int)Enemy_Status.Armor, (int)Status_State.Current] += ((float)m_EnemyCardList[i].ReturnCardLevel()+2f);
            break;
            case CardShape.Joker:
            break;
        }

        
    }



    public void SetDebuff(int index, int level)
    {
        mDebuffLevel[index] = level;
    }

    public void Respawn()
    {
        GoalPositionCheck();

        gameObject.transform.localPosition = GameManager.instance.MovePoint[0].localPosition;
        gameObject.transform.localEulerAngles = Vector2.zero;

        gameObject.SetActive(true);
        m_EnemyState = EnemyState.Move;


        StartCoroutine(Move());

    }

    // private void Update()
    // {
    //     if (m_EnemyState == EnemyState.Move)
    //     {
    //         if (gameObject.transform.localPosition == GoalPosition.localPosition)
    //         {
    //             GoalPositionCheck();
    //         }
    //     }
    // }

    IEnumerator Move()
    {
        while (gameObject.transform.localPosition != GoalPosition.localPosition)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, GoalPosition.localPosition) < mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current])
            {
                gameObject.transform.localPosition = GoalPosition.localPosition;
                GoalPositionCheck();
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, GoalPosition.position, mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void GoalPositionCheck()
    {
        switch (m_EnemyMoveState)
        {
            case EnemyMoveState.AtPoint0:
                m_EnemyMoveState = EnemyMoveState.AtPoint1;
                GoalPosition = GameManager.instance.MovePoint[1];
                break;
            case EnemyMoveState.AtPoint1:
                m_EnemyMoveState = EnemyMoveState.AtPoint2;
                GoalPosition = GameManager.instance.MovePoint[2];
                break;
            case EnemyMoveState.AtPoint2:
                m_EnemyMoveState = EnemyMoveState.AtPoint3;
                GoalPosition = GameManager.instance.MovePoint[3];
                break;
            case EnemyMoveState.AtPoint3:
                m_EnemyMoveState = EnemyMoveState.AtPoint4;
                GoalPosition = GameManager.instance.MovePoint[4];
                break;
            case EnemyMoveState.AtPoint4:
                m_EnemyMoveState = EnemyMoveState.AtPoint5;
                GoalPosition = GameManager.instance.MovePoint[5];
                break;
            case EnemyMoveState.AtPoint5:
                m_EnemyMoveState = EnemyMoveState.AtPoint6;
                GoalPosition = GameManager.instance.MovePoint[6];
                break;
            case EnemyMoveState.AtPoint6:
                m_EnemyMoveState = EnemyMoveState.AtPoint7;
                GoalPosition = GameManager.instance.MovePoint[7];
                break;
            case EnemyMoveState.AtPoint7:
                m_EnemyMoveState = EnemyMoveState.AtPoint8;
                GoalPosition = GameManager.instance.MovePoint[8];
                break;
            case EnemyMoveState.AtPoint8:
                m_EnemyMoveState = EnemyMoveState.AtPoint9;
                GoalPosition = GameManager.instance.MovePoint[9];
                break;
            case EnemyMoveState.AtPoint9:
                m_EnemyMoveState = EnemyMoveState.AtPoint10;
                GoalPosition = GameManager.instance.MovePoint[10];
                break;
                case EnemyMoveState.AtPoint10:
                m_EnemyMoveState = EnemyMoveState.AtPoint11;
                GoalPosition = GameManager.instance.MovePoint[11];
                break;
            case EnemyMoveState.AtPoint11:
                m_EnemyMoveState = EnemyMoveState.AtPoint12;
                GoalPosition = GameManager.instance.MovePoint[12];
                break;
            case EnemyMoveState.AtPoint12:
                m_EnemyMoveState = EnemyMoveState.AtPoint13;
                GoalPosition = GameManager.instance.MovePoint[13];
                break;
            case EnemyMoveState.AtPoint13:
                m_EnemyMoveState = EnemyMoveState.AtPoint14;
                GoalPosition = GameManager.instance.MovePoint[14];
                break;
            case EnemyMoveState.AtPoint14:
                m_EnemyMoveState = EnemyMoveState.AtPoint15;
                GoalPosition = GameManager.instance.MovePoint[15];
                break;
            case EnemyMoveState.AtPoint15:
                m_EnemyMoveState = EnemyMoveState.AtPoint16;
                GoalPosition = GameManager.instance.MovePoint[16];
                break;
            case EnemyMoveState.AtPoint16:
                m_EnemyMoveState = EnemyMoveState.AtPoint17;
                GoalPosition = GameManager.instance.MovePoint[17];
                break;
            case EnemyMoveState.AtPoint17:
                m_EnemyMoveState = EnemyMoveState.AtPoint18;
                GoalPosition = GameManager.instance.MovePoint[18];
                break;
            case EnemyMoveState.AtPoint18:
                m_EnemyMoveState = EnemyMoveState.AtPoint19;
                GoalPosition = GameManager.instance.MovePoint[19];
                break;
            case EnemyMoveState.AtPoint19:
                m_EnemyMoveState = EnemyMoveState.AtPoint0;
                GoalPosition = GameManager.instance.MovePoint[0];
                break;

        }
    }

    public EnemyState ReturnEnemyState()
    {
        return m_EnemyState;
    }
}


