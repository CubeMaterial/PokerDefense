using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager> {


    private float m_fTimeDelta = 0;
    private float m_fTimeScale = 1.0f; //초기배수 1.
    private float m_fTimeAcc = 0.0f;
    private float m_fUnit = 10.0f;// 10초 = 1시간.
    float TIMESCALE
    {
        get
        {
            return m_fTimeScale;
        }
    }

    public void StopTime()
    {
        m_fTimeScale = 0f;
    }

    public void PlayTime()
    {
        m_fTimeScale = 1f;
    }

    public void AccTime(float f)
    {
        m_fTimeScale = f;
    }
    public float TIMEDELTA
    {
        get
        {
            return m_fTimeDelta * m_fTimeScale;
        }
    }

    private void FixedUpdate()
    {
        m_fTimeDelta = Time.deltaTime;
        //m_fTimeDelta = m_fTimeDelta * m_fTimeMultiple;

        //1일 = 4분
        //1시간 = 10초.. ? 10 * 24 = 240초 = 4분.

        m_fTimeAcc += m_fTimeDelta;

        //if (m_fTimeAcc >= m_fUnit)
        //{
        //    m_fTimeAcc = 0.0f;
        //    m_iHour++;
        //}
        //if (m_iHour >= 24)
        //{
        //    m_iDay++;
        //    m_iHour = 0;
        //}


    }
}
