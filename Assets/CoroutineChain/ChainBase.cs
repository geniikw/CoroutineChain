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
        public static MemoryPool<ChainBase> BasePool = new MemoryPool<ChainBase>(null,c => c.Clear());
        public static MemoryPool<Chain> ChainPool = new MemoryPool<Chain>(null,c => c.Clear());
        
        MonoBehaviour _player;

        Queue<Chain> m_chainQueue = new Queue<Chain>();
        
        bool m_isPlay = true;

        public override bool keepWaiting
        {
            get
            {
                return m_isPlay;
            }
        }

        public ChainBase Setup(MonoBehaviour player)
        {
            m_isPlay = true;
            _player = player;
            _player.StartCoroutine(Routine());
            return this;
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

        public ChainBase Play(IEnumerator routine)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
            return this;
        }

        public ChainBase Wait(float waitSec)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(WaitRoutine(waitSec), _player));
            return this;
        }

        public ChainBase Parallel(params IEnumerator[] routines)
        {
            m_chainQueue.Enqueue(ChainPool.Spawn().SetupParallel(routines, _player));
            return this;
        }

        public ChainBase Sequential(params IEnumerator[] routines)
        {
            foreach (var routine in routines)
                m_chainQueue.Enqueue(ChainPool.Spawn().SetupRoutine(routine, _player));
            return this;
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

        IEnumerator WaitRoutine(float wait)
        {
            yield return new WaitForSeconds(wait);
        }
    }
}