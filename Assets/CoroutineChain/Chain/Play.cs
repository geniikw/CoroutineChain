using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.CoroutineChain
{
    public class Play : IChain
    {
        IEnumerator _routine;

        public Play(IEnumerator routine)
        {
            _routine = routine;
        }

        Coroutine IChain.Play(MonoBehaviour mono)
        {
            return mono.StartCoroutine(_routine);
        }
    }
}