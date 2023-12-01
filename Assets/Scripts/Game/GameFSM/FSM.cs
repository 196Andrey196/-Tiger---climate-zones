using System;
using System.Collections.Generic;

public class FSM
{
    private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();
    public Dictionary<Type, FSMState> states { get { return _states; } }
    private FSMState _currentState;
    public FSMState currentState { get { return _currentState; } }
    public void AddState(FSMState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSMState
    {
        var type = typeof(T);
        if (_currentState != null && _currentState.GetType() == type) return;
        if (_states.TryGetValue(type, out var newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

    }
    public void Update()
    {
        _currentState?.Update();
    }


}
