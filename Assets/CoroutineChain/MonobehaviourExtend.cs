using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.CoroutineChain;

public static class MonobehaviourExtend {
    
    public static Chain StartChain(this MonoBehaviour mono)
    {
        return new Chain();
    }
}


public static class CoroutineChain
{
    class Dispather : MonoBehaviour { }
    static Dispather m_instance;
    static Dispather Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("CoroutineChain").AddComponent<Dispather>();
                UnityEngine.Object.DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    public static Chain Start
    {
        get
        {
            return new Chain();
        }
    }

}
