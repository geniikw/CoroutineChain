using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.CoroutineChain
{
    public class Call : IChain
    {
        System.Action _func;
        public Call(System.Action func)
        {
            _func = func;
        }
        
        public Coroutine Play(MonoBehaviour mono)
        {
            if (_func != null)
                _func();
            return null;
        }        
    }
}