using UnityEngine;

[DisallowMultipleComponent]

public static class InputReader {

    [SerializeField]
    private static KeyCode
        _upKey = KeyCode.W;
    [SerializeField]
    private static KeyCode
        _downKey = KeyCode.S;
    [SerializeField]
    private static KeyCode
        _leftKey = KeyCode.A;
    [SerializeField]
    private static KeyCode
        _rightKey = KeyCode.D;

    [SerializeField]
    private static KeyCode
        _actionKey = KeyCode.E;

    private static bool _up = false;
    private static bool _down = false;
    private static bool _left = false;
    private static bool _right = false;

    private static Vector2Int _inputVector;

    public static void Start() {
        _inputVector = Vector2Int.zero;
    }

    public static void Update() {

        if (Input.GetKeyDown(_upKey)) {
            _up = true;
            _inputVector.y = 1;
        }
        if (Input.GetKeyUp(_upKey)) {
            _up = false;
            if (_inputVector.y == 1) {
                if (_down) {
                    _inputVector.y = -1;
                } else {
                    _inputVector.y = 0;
                }
            }
        }

        if (Input.GetKeyDown(_downKey)) {
            _down = true;
            _inputVector.y = -1;
        }
        if (Input.GetKeyUp(_downKey)) {
            _down = false;
            if (_inputVector.y == -1) {
                if (_up) {
                    _inputVector.y = 1;
                } else {
                    _inputVector.y = 0;
                }
            }
        }

        if (Input.GetKeyDown(_leftKey)) {
            _left = true;
            _inputVector.x = -1;
        }
        if (Input.GetKeyUp(_leftKey)) {
            _left = false;
            if (_inputVector.x == -1) {
                if (_right) {
                    _inputVector.x = 1;
                } else {
                    _inputVector.x = 0;
                }
            }
        }

        if (Input.GetKeyDown(_rightKey)) {
            _right = true;
            _inputVector.x = 1;
        }
        if (Input.GetKeyUp(_rightKey)) {
            _right = false;
            if (_inputVector.x == 1) {
                if (_left) {
                    _inputVector.x = -1;
                } else {
                    _inputVector.x = 0;
                }
            }
        }
    }

    public static Vector2Int GetInputVector() {
        return _inputVector;
    }

    public static bool GetActionKey() {
        return Input.GetKeyDown(_actionKey);
    }
}