using System;
using System.Collections.Generic;

public class StateMachine
{

    private Dictionary<Type, AState> _states;
    private AState _currentState;
    private AState _previousState;
    private readonly IStateInitializer _stateInitializer;


    public void SetStateInitializer(IStateInitializer stateInitializer)
    {
        _states = stateInitializer.Initialize();
    }

    public void ChangeState(Type stateType)
    {
        _currentState?.Exit();
        _previousState = _currentState;
        _currentState = _states[stateType];
        _currentState?.Enter();
    }

    public void UseStateUpdate()
    {
        _currentState?.Update();
    }

    public AState GetState() => _currentState;
    public AState GetPreviousState() => _previousState;

}
