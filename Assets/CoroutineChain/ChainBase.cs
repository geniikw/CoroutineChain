using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.CoroutineChain
{
    public interface IChain
    {
        Coroutine Play(MonoBehaviour mono);
    }
    
    public class ChainBase : CustomYieldInstruction
    {
        MonoBehaviour _mono;

        Queue<IChain> m_chainQueue= new Queue<IChain>();
        bool m_isPlay = true;

        public override bool keepWaiting
        {
            get
            {
                return m_isPlay;
            }
        }

        public ChainBase(MonoBehaviour mono)
        {
            _mono = mono;
            _mono.StartCoroutine(Routine());
        }
        
        IEnumerator Routine()
        {
            //Comment
            //처음 생성자에서 StartCoroutine을 할땐 큐가 비어있다. 그다음 체인에서
            //큐를 쌓는데 그동안 기다려야 한다.
            //만약 사용자가 빈 Chain을 실행하는 경우 끝나지 않는 Coroutine이 됨으로 
            //1초간만 기다린다.
            
            var startTime = Time.realtimeSinceStartup;
            while (m_chainQueue.Count == 0 && Time.realtimeSinceStartup - startTime < 1)
                yield return null;
                        
            while (m_chainQueue.Count > 0)
            {
                var cr = m_chainQueue.Dequeue().Play(_mono);
                if(cr != null)
                    yield return cr;
            }
            m_isPlay = false;
        }

        public ChainBase Play(IEnumerator routine)
        {
            m_chainQueue.Enqueue(new Play(routine));
            return this;
        }

        public ChainBase Wait(float waitSec)
        {
            m_chainQueue.Enqueue(new Wait(waitSec));
            return this;
        }

        public ChainBase Parallel(params IEnumerator[] routines)
        {
            m_chainQueue.Enqueue(new Parallel(routines));
            return this;
        }

        public ChainBase Sequential(params IEnumerator[] routines)
        {
            foreach (var routine in routines)
                m_chainQueue.Enqueue(new Play(routine));
            return this;
        }

        public ChainBase Log(string log, ELogType type = ELogType.NORMAL)
        {
            m_chainQueue.Enqueue(new Log(log, type));
            return this;
        }

        public ChainBase Call(System.Action f)
        {
            m_chainQueue.Enqueue(new Call(f));
            return this;
        }

    }
}