using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELogType
{
    NORMAL,
    WARRNING,
    ERROR
}

namespace geniikw.CChain
{
    public interface IChain
    {
        Coroutine Play(MonoBehaviour mono);
    }
    
    public class ChainBase : CustomYieldInstruction
    {
        static MemoryPool<ChainBase> BasePool = new MemoryPool<ChainBase>(null,c => c.Clear());
        static MemoryPool<Chain> ChainPool = new MemoryPool<Chain>(null,c => c.Clear());
        
        MonoBehaviour _player;

        Queue<Chain> m_chainQueue = new Queue<Chain>();
        
        bool m_isPlay;

        Chainer _chainer = null;

        public override bool keepWaiting
        {
            get
            {
                return m_isPlay;
            }
        }

        public Chainer Setup(MonoBehaviour player)
        {
            m_isPlay = true;
            _player = player;
            _player.StartCoroutine(Routine());

            if (_chainer == null)
                _chainer = new Chainer(this);

            return _chainer;
        }

        void Clear()
        {
            _player = null;
            m_chainQueue.Clear();
        }
        
        IEnumerator Routine()
        {
            yield return null;
                        
            while (m_chainQueue.Count > 0)
            {
                var chain = m_chainQueue.Dequeue();
                var cr = chain.Play();
                if(cr != null)
                    yield return cr;
                ChainPool.Despawn(chain);
            }

            m_isPlay = false;
            BasePool.Despawn(this);
        }

        void Play(IEnumerator routine)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
        }

        void Wait(float waitSec)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(WaitRoutine(waitSec), _player));
        }

        void Parallel(params IEnumerator[] routines)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupParallel(routines, _player));
        }

        void Sequential(params IEnumerator[] routines)
        {
            foreach (var routine in routines)
                m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
        }

        public ChainBase Log(string log, ELogType type = ELogType.NORMAL)
        {
            System.Action action;
            switch (type)
            {
                default:
                case ELogType.NORMAL:
                    action = ()=> Debug.Log(log); break;
                case ELogType.WARRNING:
                    action = ()=>Debug.LogWarning(log); break;
                case ELogType.ERROR:
                    action = ()=> Debug.LogError(log); break;
            }
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupNon(action, _player));
            return this;
        }

        public ChainBase Call(System.Action action)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupNon(action, _player));
            return this;
        }
        
        public class Chainer
        {
            ChainBase _base;
            public Chainer(ChainBase b)
            {
                _base = b;
            }

            public Chainer Play(IEnumerator routine)
            {
                _base.Play(routine);
                return this;
            }

            public Chainer Call(System.Action action)
            {
                _base.Call(action);
                return this;
            }

            public Chainer Parallel(params IEnumerator[] routines)
            {
                _base.Parallel(routines);
                return this;
            }

            public Chainer Sequential(params IEnumerator[] routines)
            {
                _base.Sequential(routines);
                return this;
            }

            public Chainer Wait(float sec)
            {
                _base.Wait(sec);
                return this;
            }

            public Chainer Log(string log, ELogType type = ELogType.NORMAL)
            {
                _base.Log(log, type);
                return this;
            }
        }


        IEnumerator WaitRoutine(float wait)
        {
            var t = 0f;
            while(t < 1f)
            {
                t += Time.deltaTime / wait;
                yield return null;
            }
        }
    }
}