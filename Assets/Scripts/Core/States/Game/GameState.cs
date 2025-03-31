
public class GameState : AState
{
    private PlatformPlacer _platformPlacer;
    private readonly GameSpeedService _gameSpeedService;

    public GameState(PlatformPlacer platformPlacer, GameSpeedService gameSpeedService)
    {
        _platformPlacer = platformPlacer;
        _gameSpeedService = gameSpeedService;
    }

    public override void Enter()
    {
        _gameSpeedService.IncreaseSpeed();
    }
    public override void Update()
    {
        base.Update();
        _platformPlacer.Update();
    }
    public override void Exit() 
    {
    }

   
}
