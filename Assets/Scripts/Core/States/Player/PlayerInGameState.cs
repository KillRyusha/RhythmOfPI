public class PlayerInGameState : AState
{
    private readonly PlayerAnimationPlayer _playerAnimationPlayer;
    private readonly Player _player;
    private readonly PlayerInput _playerInput;
    private readonly PlayerMover _playerMover;
    public PlayerInGameState(PlayerAnimationPlayer playerAnimationPlayer, Player player)
    {
        _playerAnimationPlayer = playerAnimationPlayer;
        _player = player;
        _playerInput = new PlayerInput();
        _playerMover = new PlayerMover(_player,_playerInput);
    }

    public override void Enter()
    {
        _playerAnimationPlayer.PlayRun();
        _playerMover.Init();
    }
    public override void Update()
    {
        _playerInput.CheckInput();
    }
    public override void Exit() 
    {
        _playerMover.UnSubscribe();
    }

}
