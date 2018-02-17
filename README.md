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

[AssetStore](https://www.assetstore.unity3d.com/kr/#!/content/109785)
