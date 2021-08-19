using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour 
{


    private CardShape mCardShape;
    private CardLevel mCardLevel;

    private int iDebuff;

    private float[,] mEnemyStatus;


    private int[] mDebuffLevel;

    public Image m_EnemySprite;

    public Transform GoalPosition;
    public GameObject EnemyCore;

    private EnemyState m_EnemyState;

    public EnemyMoveState m_EnemyMoveState;

    private void Awake()
    {
        mDebuffLevel = new int[(int)EnemyDebuff.End];
        mEnemyStatus = new float[(int)Enemy_Status.End, (int)Status_State.End]; 
        // Respawn();
    }

    public void Init(CardShape shape, CardLevel level)
    {
        mCardShape = shape;
        mCardLevel = level;


        SetParameter();
        m_EnemyState = EnemyState.Ready;
        m_EnemyMoveState = EnemyMoveState.AtPoint0;

        Respawn();

    }

    private void SetParameter()
    {

        mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] =
        ((int)mCardLevel + 2) * GameDataManager.instance.ReturnCurrentLevel();

        mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] = 5f;

        mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Current] = 5f;

        mEnemyStatus[(int)Enemy_Status.FixedArmor, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.FixedArmor, (int)Status_State.Current] =
        GameDataManager.instance.ReturnCurrentLevel() / 10f;

        mEnemyStatus[(int)Enemy_Status.ReduceArmor, (int)Status_State.Origin] =
        mEnemyStatus[(int)Enemy_Status.ReduceArmor, (int)Status_State.Current] = 0f;

        if (mCardShape == CardShape.Heart)
        {
            mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Origin] =
            mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] =
            mEnemyStatus[(int)Enemy_Status.Life, (int)Status_State.Current] * 1.5f;
        }


        if (mCardShape == CardShape.Diamond)
        {
            mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Origin] =
            mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] = 2f;
        }

       
        if (mCardShape == CardShape.Clover)
        {
            mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Origin] =
            mEnemyStatus[(int)Enemy_Status.AvoidRate, (int)Status_State.Current] = 15f;
        }

        if(mCardShape == CardShape.Spade)
        {
            mEnemyStatus[(int)Enemy_Status.FixedArmor, (int)Status_State.Origin] += GameDataManager.instance.ReturnCurrentLevel() * 10f;
            mEnemyStatus[(int)Enemy_Status.FixedArmor, (int)Status_State.Current] += GameDataManager.instance.ReturnCurrentLevel() * 10f;
        }

        if(mCardShape == CardShape.Joker)
        { 
        
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
                //print("speed : "+ mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);

                // if(Mathf.Abs(gameObject.transform.localPosition.x - GoalPosition.transform.localPosition.x) > mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current])
                // {
                //     if( gameObject.transform.localPosition.x > GoalPosition.transform.localPosition.x)
                //     {
                //         print("left");
                //         transform.Translate(-Vector3.right * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                //     }
                //     // 목표가 우측에 있을때
                //     else if(gameObject.transform.localPosition.x < GoalPosition.transform.localPosition.x)
                //     {
                //         print("right");
                //         transform.Translate(Vector3.right * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                //     }
                // }
                // else if(Mathf.Abs(gameObject.transform.localPosition.y - GoalPosition.transform.localPosition.y) > mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current])
                // {
                //     if(gameObject.transform.localPosition.y > GoalPosition.transform.localPosition.y)
                //     {
                //         print("down");
                //         transform.Translate(-Vector3.up * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                //     }
                //     // 목표가 위에 있을때
                //     else if(gameObject.transform.localPosition.y < GoalPosition.transform.localPosition.y)
                //     {
                //         print("up");
                //         transform.Translate(Vector3.up * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                //     }
                // }
                // else
                // {
                //     print("none");
                // }
                // gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, GoalPosition.localPosition,0.5f* Time.deltaTime);
                gameObject.transform.position = Vector3.MoveTowards(transform.position, GoalPosition.position, mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current] * Time.deltaTime);


                // 목표가 좌측에 있을때
                // if( gameObject.transform.localPosition.x > GoalPosition.transform.localPosition.x)
                // {
                //     print("left");
                //     transform.Translate(-Vector3.right * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // // 목표가 우측에 있을때
                // else if(gameObject.transform.localPosition.x < GoalPosition.transform.localPosition.x)
                // {
                //     print("right");
                //     transform.Translate(Vector3.right * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // // 목표가 아래에 있을때
                // else if(gameObject.transform.localPosition.y > GoalPosition.transform.localPosition.y)
                // {
                //     print("down");
                //     transform.Translate(-Vector3.up * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // // 목표가 위에 있을때
                // else if(gameObject.transform.localPosition.y < GoalPosition.transform.localPosition.y)
                // {
                //     print("up");
                //     transform.Translate(Vector3.up * Time.deltaTime * mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // else
                // {
                //     print("none");
                // }

                // gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, GoalPosition.localPosition,Time.deltaTime);
                // if (m_EnemyMoveState == EnemyMoveState.AtPoint1 || m_EnemyMoveState == EnemyMoveState.AtPoint5 || m_EnemyMoveState == EnemyMoveState.AtPoint7)
                // {
                //     // down
                //     gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // else if (m_EnemyMoveState == EnemyMoveState.AtPoint3 || m_EnemyMoveState == EnemyMoveState.AtPoint9)
                // {
                //     // up
                //     gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current]);
                // }
                // else if (m_EnemyMoveState == EnemyMoveState.AtPoint2 || m_EnemyMoveState == EnemyMoveState.AtPoint4 || m_EnemyMoveState == EnemyMoveState.AtPoint8)
                // {
                //     // right
                //     gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x + mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current], gameObject.transform.localPosition.y);
                // }
                // else if (m_EnemyMoveState == EnemyMoveState.AtPoint6 || m_EnemyMoveState == EnemyMoveState.AtPoint0)
                // {
                //     // left
                //     gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x - mEnemyStatus[(int)Enemy_Status.Speed, (int)Status_State.Current], gameObject.transform.localPosition.y);
                // }
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


