///*
////https://gist.github.com/geniikw/071463c491eee975c863a9163c9dcf69
//Copyright (c) 2017 geniikw
//no duty all free.
//*/
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Linq;

//public static class CoroutineChain
//{
//    class Dispather : MonoBehaviour { }
//    static Dispather m_instance;
//    static Dispather Instance
//    {
//        get
//        {
//            if (m_instance == null)
//            {
//                m_instance = new GameObject("CoroutineChain").AddComponent<Dispather>();
//                UnityEngine.Object.DontDestroyOnLoad(m_instance);
//            }
//            return m_instance;
//        }
//    }

//    public class Chain : CustomYieldInstruction
//    {
//        public bool isPlay = true;

//        public override bool keepWaiting
//        {
//            get
//            {
//                return isPlay;
//            }
//        }

//        public Chain() { }

//        public Chain Play(IEnumerator next)
//        {
//            return Chaining(next, this);
//        }

//        public Chain Play(Func<IEnumerator> next)
//        {
//            return Chaining(next(), this);
//        }

//        public Chain Sequential(params Func<IEnumerator>[] next)
//        {
//            Chain current = this;
//            for (int i = 0; i < next.Length; i++)
//                current = Chaining(next[i](), current);

//            return current;
//        }

//        public Chain Sequential(params IEnumerator[] next)
//        {
//            Chain current = this;
//            for (int i = 0; i < next.Length; i++)
//                current = Chaining(next[i], current);

//            return current;
//        }

//        public Chain Wait(float sec)
//        {
//            return Chaining(SimpleWait(sec), this);
//        }

//        public Chain Parallel(params IEnumerator[] next)
//        {
//            Chain current = null;

//            current = ParallelChaining(next, this);

//            return current;
//        }

//        public Chain Parallel(params Func<IEnumerator>[] next)
//        {
//            Chain current = null;

//            current = ParallelChaining(next.Select(n => n()), this);

//            return current;
//        }

//        public Chain Call(Action func)
//        {
//            return Chaining(SimpleCall(func), this);
//        }

//        IEnumerator SimpleCall(Action func)
//        {
//            if (func != null)
//                func();
//            else
//                yield return null;
//        }

//        IEnumerator SimpleWait(float time)
//        {
//            float t = 0;
//            while (t < 1f)
//            {
//                t += Time.deltaTime / time;
//                yield return null;
//            }
//        }

//        public Chain Log(string log)
//        {
//            return Chaining(SimpleCall(() => Debug.Log(log)), this);
//        }
//    }

//    public static Chain Start
//    {
//        get
//        {
//            return Chaining(null);
//        }
//    }

//    static Chain Chaining(IEnumerator routine, Chain wait = null)
//    {
//        Chain c = new Chain();
//        Instance.StartCoroutine(WaitChain(routine, c, wait));
//        return c;
//    }

//    static IEnumerator WaitChain(IEnumerator routine, Chain current, Chain wait = null)
//    {
//        if (wait != null)
//            yield return wait;

//        if (routine != null)
//            yield return Instance.StartCoroutine(routine);
//        current.isPlay = false;
//    }

//    static Chain ParallelChaining(IEnumerable<IEnumerator> routine, Chain waits)
//    {
//        Chain c = new Chain();
//        Instance.StartCoroutine(ParallelWaitChain(routine, c, waits));
//        return c;
//    }

//    static IEnumerator ParallelWaitChain(IEnumerable<IEnumerator> routines, Chain current, Chain wait = null)
//    {
//        if (wait != null)
//            yield return wait;

//        int complete = 0;
//        int play = 0;
//        if (routines != null)
//            foreach (var routine in routines)
//            {
//                play++;
//                Instance.StartCoroutine(StartCoroutineCallback(routine, () => complete++));
//            }

//        while (complete < play)
//            yield return null;

//        current.isPlay = false;
//    }

//    static IEnumerator StartCoroutineCallback(IEnumerator routine, Action callback)
//    {
//        yield return Instance.StartCoroutine(routine);
//        if (callback != null)
//            callback();
//    }
//}