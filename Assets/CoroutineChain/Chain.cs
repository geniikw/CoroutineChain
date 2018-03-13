using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.CChain
{
    public class Chain 
    {
        EType type;
        MonoBehaviour player;
        IEnumerator routine;
        IEnumerator[] parallelRoutine;
        System.Action action;
        
        public Coroutine Play()
        {
            switch (type)
            {
                default:
                case EType.NonCoroutine:
                    action();
                    return null;
                case EType.Parallel:
                    return player.StartCoroutine(Parallel(parallelRoutine));
                case EType.Single:
                    return player.StartCoroutine(routine);
            }
        }

        public Chain SetupRoutine(IEnumerator routine, MonoBehaviour player)
        {
            type = EType.Single;
            this.player = player;
            this.routine = routine;
            return this;
        }

        public Chain SetupParallel(IEnumerator[] routines, MonoBehaviour player)
        {
            type = EType.Parallel;
            this.player = player;
            this.parallelRoutine = routines;
            return this;
        }

        public Chain SetupNon(System.Action action, MonoBehaviour player)
        {
            type = EType.NonCoroutine;
            this.player = player;
            this.action = action;
            return this;
        }

        public void Clear()
        {
            player = null;
            routine = null;
            action = null;
            parallelRoutine = null;
        }

        IEnumerator Parallel(IEnumerator[] routines)
        {
            var all = 0;
            foreach (var r in routines)
                all++;

            var c = 0;
            foreach (var r in routines)
                player.StartChain()
                    .Play(r)
                    .Call(() => c++);

            while (c < all)
                yield return null;
        }
        
        public enum EType
        {
            Single,
            Parallel,
            NonCoroutine
        }
    }
}