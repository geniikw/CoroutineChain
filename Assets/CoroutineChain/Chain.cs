using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.CoroutineChain
{

    public class Chain
    {
        public Chain() { }


        public Chain Play(IEnumerator routine)
        {
            return new Chain();
        }

        public Chain Wait(float waitSec)
        {
            return new Chain();
        }

        public Chain Parallel(params IEnumerator[] routines)
        {
            return new Chain();
        }

        public Chain Sequential(params IEnumerator[] routines)
        {
            return new Chain();
        }

        public Chain Log(string log)
        {
            return new Chain();
        }

        public Chain Call(System.Action f)
        {
            return new Chain();
        }

    }
}