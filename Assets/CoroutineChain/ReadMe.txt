CoroutineChain(0.1.1)

This asset consists of a single class wrapped around the CoroutineStart method.
 To invoke a coroutine using this asset, you can use

CoroutineChain.Start
.Play (Coroutine1 ());

 If Coroutine1 is over
If you need a callback or want to play another coroutine, you can continue to chain it.

CoroutineChain.Start
.Play (Coroutine1 ())
.Play (Coroutine2 ())
.Call (() => Debug.Log ("CompleteChain");

 It same as below.

CoroutineChain.Start
.Sequential(Coroutine1(), Coroutine2())
.Call (() => Debug.Log ("CompleteChain");

 another option is parallel. It play coroutines same time.

CoroutineChain.Start
.Call (() => Debug.Log ("PlaySameTime");
.Parallel(Coroutine1(), Coroutine2())
.Call (() => Debug.Log ("1,2 is over.");

Leave a message if you have feedback.

https://gist.github.com/geniikw/071463c491eee975c863a9163c9dcf69