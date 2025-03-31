public class InitState : AState
{
    private PlatformPlacer _platformPlacer;
    private readonly LevelSetUpper _levelSetUpper;

    public InitState(PlatformPlacer platformPlacer, LevelSetUpper levelSetUpper)
    {
        _platformPlacer = platformPlacer;
        _levelSetUpper = levelSetUpper;
        
    }

    public override void Enter()
    {

        _platformPlacer.Initialize();
    }
    public override void Exit()
    {
        _levelSetUpper.StartService();
    }
}
