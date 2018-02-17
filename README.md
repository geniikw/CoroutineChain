# CoroutineChain
Unity3d, Coroutine, scripting

It is a small asset that makes it possible to call Unity's coroutines while chaining them.

```csharp
void Start(){

  this.StartChain() // or CoroutineChain.Start.Play ...
      .Play(Coroutine())
      .Sequencial(A(),B(),C()) // play one by one.
      .Parallel(A(),B(),C()) // play same time.
      .Wait(1f)
      .Log("Complete!");
      .Call(()=>Callback());
      
}
```

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4BMRDBLE79K4)
