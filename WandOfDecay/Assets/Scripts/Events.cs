using System;

public abstract class BaseEvent
{
    private event Action Changed;
    public void Invoke()
    {
        Changed?.Invoke();
    }
    public void Subscribe(Action action) 
    {
        Changed += action;
    }
    public void Unsubscribe(Action action)
    {
        Changed -= action;
    }
}

public abstract class BaseEvent<T>
{
    private event Action<T> Changed;
    public void Invoke(T value)
    {
        Changed?.Invoke(value);
    }
    public void Subscribe(Action<T> action)
    {
        Changed += action;
    }
    public void Unsubscribe(Action<T> action)
    {
        Changed -= action;
    }
}


public class OnTileCorrupted : BaseEvent { }
public class OnTreeCorrupted : BaseEvent { }
public class OnBushCorrupted : BaseEvent { }
public class OnGrassCorrupted : BaseEvent { }