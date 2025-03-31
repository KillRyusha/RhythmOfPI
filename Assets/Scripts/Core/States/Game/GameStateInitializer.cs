using System;
using System.Collections.Generic;

public class GameStateInitializer : IStateInitializer
{
    private StateMachine _owner;
    private PlatformPlacer _platformPlacer;
    private readonly GameSpeedService _gameSpeedService;
    private readonly LevelSetUpper _levelSetUpper;

    public GameStateInitializer(StateMachine owner, PlatformPlacer platformPlacer, GameSpeedService gameSpeedService, LevelSetUpper levelSetUpper)
    {
        _owner = owner;
        _platformPlacer = platformPlacer;
        _gameSpeedService = gameSpeedService;
        _levelSetUpper = levelSetUpper;
    }

    public Dictionary<Type, AState> Initialize()
    {
        return new Dictionary<Type, AState>()
        {
            [typeof(InitState)] = new InitState(_platformPlacer, _levelSetUpper),
            [typeof(GameState)] = new GameState(_platformPlacer, _gameSpeedService),
            [typeof(GameEndState)] = new GameEndState(),
        };
    }
}
