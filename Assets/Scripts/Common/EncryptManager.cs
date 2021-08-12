using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptManager : Singleton<EncryptManager> {

    private int m_IXorCode;

    private void Awake()
    {
        m_IXorCode = Random.Range(0, 100000) + Random.Range(0, 100000) + Random.Range(0, 100000) + Random.Range(0, 100000) + Random.Range(0, 100000);
    }

    public int ReturnData(int i)
    {
        return i ^ m_IXorCode;
    }

}
