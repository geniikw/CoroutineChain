# CoroutineChain
Unity3d, Coroutine, scripting

In my exprience, Generally coroutine code can be classified into two kinds.

One is functional one block. for example 

```csharp
IEnumerator OneFunction(){
  //do single task.
}
```

Other is squencial code. It consist of functional block coroutines.
```csharp
IEnumerator Sequncial(){
  yield return StartCoroutine(A());
  yield return StartCoroutine(B());
  yield return StartCoroutine(C());
}
```

It is a small asset that makes it possible to call Unity's coroutines while chaining them.

so you don't need to write seuqencial block. just chain it in call black.

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
