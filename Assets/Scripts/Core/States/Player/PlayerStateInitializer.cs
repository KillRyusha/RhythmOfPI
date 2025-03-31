using System;
using System.Collections.Generic;

public class PlayerStateInitializer : IStateInitializer
{
    private Player _player;
    private PlayerAnimationPlayer _playerAnimationPlayer;
    private StateMachine _owner;

    public PlayerStateInitializer(StateMachine owner, PlayerAnimationPlayer playerAnimationPlayer, Player player)
    {
        _owner = owner;
        _playerAnimationPlayer = playerAnimationPlayer;
        _player = player;
    }

    public Dictionary<Type, AState> Initialize()
    {
        return new Dictionary<Type, AState>()
        {
            [typeof(PlayerInitState)] = new PlayerInitState(_playerAnimationPlayer),
            [typeof(PlayerInGameState)] = new PlayerInGameState(_playerAnimationPlayer, _player),
            [typeof(PlayerEndState)] = new PlayerEndState(_playerAnimationPlayer),
        };
    }
}