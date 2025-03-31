
public class PlayerEndState : AState
{
    private PlayerAnimationPlayer _playerAnimation;

    public PlayerEndState(PlayerAnimationPlayer playerAnimation)
    {
        _playerAnimation = playerAnimation;
    }

    public override void Enter()
    {
        _playerAnimation.PlayFinish();
    }
}
