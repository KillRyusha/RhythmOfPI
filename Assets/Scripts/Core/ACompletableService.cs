using System;

public abstract class ACompletableService
{
    private Action _onServiceComplete;
    protected bool _serviceCompleted;

    public virtual void OnComplete()
    {
        _onServiceComplete?.Invoke();
        _serviceCompleted = true;
    }

    public virtual void Subscribe(Action action)
    {
        _onServiceComplete += action;
    }
    public virtual void UnSubscribe(Action action)
    {
        _onServiceComplete -= action;
    }
}