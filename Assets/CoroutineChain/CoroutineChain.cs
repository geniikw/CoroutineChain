using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.CoroutineChain;

public static class MonobehaviourExtend
{    
    public static ChainBase StartChain(this MonoBehaviour mono)
    {
        var chain = new ChainBase(mono);
        return chain;
    }
}

/// <summary>
/// 호환성을 위해 이름유지.
/// </summary>
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
                Object.DontDestroyOnLoad(m_instance);
            }
            return m_instance;
        }
    }

    public static void StopAll()
    {
        m_instance.StopAllCoroutines();
    }
    
    public static ChainBase Start
    {
        get
        {
            var b = new ChainBase(Instance);
            return b;
        }
    }

}
