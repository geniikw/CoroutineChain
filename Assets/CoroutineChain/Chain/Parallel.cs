using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.CoroutineChain
{
    public class Parallel : IChain
    {
        IEnumerator[] _routines;

        public Parallel(IEnumerator[] routines)
        {
            _routines = routines;
        }
        
        public Coroutine Play(MonoBehaviour mono)
        {
            return mono.StartCoroutine(Routine(mono));
        }

        IEnumerator Routine(MonoBehaviour mono)
        {
            var all = 0;
            foreach (var r in _routines)
                all++;

            var c = 0;
            foreach (var r in _routines)
                mono.StartChain()
                    .Play(r)
                    .Call(() => c++);

            while (c < all)
                yield return null;
        }
    }
}