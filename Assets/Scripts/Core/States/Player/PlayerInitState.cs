public class PlayerInitState : AState
{
    private readonly PlayerAnimationPlayer _playerAnimationPlayer;

    public PlayerInitState(PlayerAnimationPlayer playerAnimationPlayer)
    {
        _playerAnimationPlayer = playerAnimationPlayer;
    }

    public override void Enter()
    {
        _playerAnimationPlayer.PlayStart();
    }
    public override void Exit() { }

}
