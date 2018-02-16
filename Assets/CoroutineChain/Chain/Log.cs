using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELogType
{
    NORMAL,
    WARRNING,
    ERROR
}

namespace geniikw.CoroutineChain {

    public class Log : IChain
    {
        string _log;
        ELogType _type;
        
        public Log(string log, ELogType type = ELogType.NORMAL)
        {
            _log = log;
            _type = type;
        }
        
        public Coroutine Play(MonoBehaviour mono)
        {
            switch (_type)
            {
                case ELogType.NORMAL:
                    Debug.Log(_log);break;
                case ELogType.WARRNING:
                    Debug.LogWarning(_log);break;
                case ELogType.ERROR:
                    Debug.LogError(_log);break;
            }
            return null;
        }


    }
}