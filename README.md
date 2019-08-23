# CoroutineChain
Unity3d, Coroutine, scripting

[AssetStore](https://assetstore.unity.com/packages/tools/input-management/coroutinechain-109785)

## Intro

- easy to read coroutine code.
- define callback simply
- call coroutine at outside of MonoBehavour.


In my exprience, generally coroutine code can be classified into two kinds.

One is a functional block. for example 

```csharp
IEnumerator OneFunction(){
  //do single task.
}
```

Other is a squencial code. It consist of functional block coroutines.
```csharp
IEnumerator Sequncial(){
  yield return StartCoroutine(A());
  yield return StartCoroutine(B());
  yield return StartCoroutine(C());
}
```

It is a small asset that makes it possible to call Unity's coroutines while chaining them.

so you don't need to write seuqencial block. just chain it in call block.

```csharp
public void Start(){
    //same as above.
    CoroutineChain.Start
        .Play(A())
        .Play(B())
        .Play(C());
    //or
    CoroutineChain.Start
        .Sequencial(A(),B(),C());
}
```

[AssetStore](https://www.assetstore.unity3d.com/kr/#!/content/109785)

## Reference

### Basic
all block wait previous block. 
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

### Play(IEnumerator coroutine)
play one coroutine. it is same as StartCoroutine().
```csharp
void Start(){
    //Normal
    StartCoroutine(A());
    //CoroutineChain
    this.StartChain()
        .Play(A());
}
```

### Wait(float sec)
```csharp
void Start(){
    this.StartChain()
        .Wait(1)
        .Log("end");
    //1sec later debug log show out.
}
```

### Parallel(IEnumerator[] routines)
all coroutine start at same time
```csharp
void Start(){
    ///Normal.
    StartCoroutine(A());
    StartCoroutine(B());
    StartCoroutine(C());    
    
    //CoroutineChain, Less Character!
    this.StartChain()
        .Parallel(A(),B(),C());
}
```


### Sequential(IEnemerator[] routines)
it same as continuous Play block. 
```csharp
IEnumerator Start(){///CoroutineStartBlock.
    ///Normal.
    yield return StartCoroutine(A());
    yield return StartCoroutine(B());
    yield return StartCoroutine(C());    
    
    //CoroutineChain, Less Character!
    yield return this.StartChain()
        .Sequencial(A(),B(),C());
}
```
### Log(string log, ELogTtype type = ELogType.NORMAL)
log block is not coroutine. 
```csharp
IEnumerator Start(){///CoroutineStartBlock.
    ///Normal.
    yield return StartCoroutine(A());
    yield return StartCoroutine(A());
    yield return StartCoroutine(A());
    Debug.Log(A, B and C all end!!");
    
    //CoroutineChain
    yield return this.StartChain()
        .Sequencial(A(),B(),C())
        .Log("A, B and C all end!!");
}
```

### Call(Action)
you can simply setup callback.

this is Parallel block using Call() as coroutine callback.
```csharp
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
```

### WaitFor(Func<bool>)

wait for a specific condition.

```csharp
void Start(){
  CoroutineChain.Start
          .WaitFor(()=>m_timer > 3f)
          .Log("Complete");
}

float m_timer = 0f;

void Update(){
  m_timer += Time.deltaTime;
}

```


