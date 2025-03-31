using System.Collections;
using UnityEngine;
public class PlayerMover
{
    private const float MOVE_SPEED = 5f;
    private const float JUMP_FORCE = 5f;
    private const float JUMP_HEIGHT = 2f;
    private const int MIN_POINT_VALUE = 0;
    private const int MAX_POINT_VALUE = 2;
    private const float START_Y = 0.5f;
    private readonly float[] _lanePositions = { -1.7f, 0f, 1.7f };
    private Player _player;
    private PlayerInput _playerInput;
    private Coroutine _movementCoroutine;
    private int _currentLane = 1;
    private bool _isJumping = false;
    private float _jumpProgress = 0f;

    public PlayerMover(Player player, PlayerInput playerInput)
    {
        _player = player;
        _playerInput = playerInput;
    }

    public void Init()
    {
        _player.transform.position = new Vector3(_player.transform.position.x, START_Y, _lanePositions[_currentLane]);
        _playerInput.OnSwipe += ChooseDirection;
        _playerInput.OnJumpSwipe += HandleJump;
    }

    public void UnSubscribe()
    {
        _playerInput.OnSwipe -= ChooseDirection;
        _playerInput.OnJumpSwipe -= HandleJump;
        _currentLane = 1;
        StartMovement();
    }

    private void ChooseDirection(Vector2 direction)
    {
        if (direction.x > 0 && _currentLane > MIN_POINT_VALUE)
        {
            _currentLane--;
        }
        else if (direction.x < 0 && _currentLane < MAX_POINT_VALUE)
        {
            _currentLane++;
        }
        StartMovement();
    }

    private void HandleJump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _jumpProgress = 0f;
        }
    }

    private void StartMovement()
    {
        if (_movementCoroutine != null)
            _player.StopCoroutine(_movementCoroutine);
        _movementCoroutine = _player.StartCoroutine(UpdateMovement());
    }

    private IEnumerator UpdateMovement()
    {
        Vector3 startPosition = _player.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, START_Y, _lanePositions[_currentLane]);

        while (true)
        {
            float moveStep = MOVE_SPEED * Time.deltaTime;
            _player.transform.position = Vector3.Lerp(_player.transform.position, targetPosition, moveStep);

            if (_isJumping)
            {
                _jumpProgress += JUMP_FORCE * Time.deltaTime*0.5f;
                float jumpOffset = Mathf.Sin(_jumpProgress * Mathf.PI) * JUMP_HEIGHT;
                _player.transform.position = new Vector3(
                    _player.transform.position.x,
                    START_Y + jumpOffset,
                    _player.transform.position.z);
                if (_jumpProgress >= 1f)
                {
                    _isJumping = false;
                }
            }

            yield return null;
        }
    }
}
