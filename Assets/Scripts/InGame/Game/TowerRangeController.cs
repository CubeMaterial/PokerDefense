using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerRangeController : MonoBehaviour {

    Tower m_Tower;
    SphereCollider m_Collider;
    int m_Width;


	void Awake ()
    {
        //m_Width = gameObject.transform.parent.GetComponent<Image>().width;
        m_Collider = GetComponent<SphereCollider>();

        m_Tower = gameObject.transform.parent.GetComponent<Tower>();
        
    }

    public void SetRadius(int r)
    {
        m_Collider.radius = (int)(m_Width * 0.4f) + (m_Width * r) ;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print("enemy");
            m_Tower.PutEnemyTarget(other.gameObject.transform.parent.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_Tower.PullEnemyTarget(other.gameObject.transform.parent.GetComponent<Enemy>());
        }
    }
}
