using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.CChain
{
    public class Wait : IChain
    {
        float _waitSec;
        public Wait(float waitSec)
        {
            _waitSec = waitSec;
        }

        public Coroutine Play(MonoBehaviour mono)
        {
            return mono.StartCoroutine(WaitRoutine());
        }

        IEnumerator WaitRoutine()
        {
            yield return new WaitForSeconds(_waitSec);
        }
    }
}