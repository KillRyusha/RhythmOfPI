using UnityEngine;

public class PlayerInput
{
    private Vector2 _startTouchPosition, _endTouchPosition;
    private bool _isSwiping = false;

    private float _swipeThreshold = 50f;
    private float _verticalSwipeThreshold = 30f;
    public System.Action<Vector2> OnSwipe;
    public System.Action OnJumpSwipe;

    public void CheckInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleKeyboardInput();
        //HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleKeyboardInput()
    {
        DetectKeyInput(KeyCode.RightArrow, Vector2.right);
        DetectKeyInput(KeyCode.LeftArrow, Vector2.left);
        if (Input.GetAxis("Vertical") > 0)
        {
            OnJumpSwipe?.Invoke();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
            _isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && _isSwiping)
        {
            _endTouchPosition = Input.mousePosition;
            DetectSwipe();
            _isSwiping = false;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _endTouchPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        Vector2 delta = _endTouchPosition - _startTouchPosition;

        if (Mathf.Abs(delta.x) > _swipeThreshold)
        {
            Vector2 swipeDirection = delta.x > 0 ? Vector2.right : Vector2.left;
            OnSwipe?.Invoke(swipeDirection);
        }
        else if (delta.y > _verticalSwipeThreshold) 
        {
            OnJumpSwipe?.Invoke(); 
        }
    }

    private void DetectKeyInput(KeyCode key, Vector2 dir)
    {
        if (Input.GetKeyDown(key))
        {
            OnSwipe?.Invoke(dir);
        }
    }
}