using UnityEngine;

public class PlayerAnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int START = Animator.StringToHash("Start");
    private readonly int RUN = Animator.StringToHash("Run");
    private readonly int DOWN = Animator.StringToHash("Down");
    private readonly int JUMP = Animator.StringToHash("Jump");
    private readonly int FINISH = Animator.StringToHash("Finish");

    public void PlayStart() => _animator.SetTrigger(START);
    public void PlayRun() => _animator.SetTrigger(RUN);
    public void PlayGoDown() => _animator.SetTrigger(DOWN);
    public void PlayJump() => _animator.SetTrigger(JUMP);
    public void PlayFinish() => _animator.SetTrigger(FINISH);

}