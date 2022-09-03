using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Player))]

public class Talk : MonoBehaviour {

    private static int _state = 0;
    
    private static bool _talking = false;
    private static bool _waitingForAction = false;
    private static bool _suspended = false;
    private static bool _choosing = false;
    private static bool _exitTalk = false;
    
    private bool _canTalk = false;
    private bool _goSkiing = false;

    private float _hintInvisibleTime = 0f;
    private readonly float _hintAppearTime = 1.5f;

    [SerializeField] private TextAppear _text;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _choice;
    [SerializeField] private GameObject _talkHint;
    [SerializeField] private GameObject _skiingHint;
    [SerializeField] private GameObject _cantSkiingHint;

    private Talkable _currentTalkable;

    public static void Suspend() {
        _suspended = true;
    }
    public static void Continue() {
        _suspended = false;
    }
    
    public static bool Choosing {
		get {
            return _choosing;
		} 
        set {
            _choosing = value;
		}
    }

    private void Next() {
        _waitingForAction = true;
        TextAppear.Appearing = true;
        _text.Text(_currentTalkable.Next());
    }

    private void Action() {
        _waitingForAction = false;
        _currentTalkable.Action();
        if (_choosing) {
            _choice.SetActive(false);
            _choosing = false;
            _state = 0;
            _text.Yes();
        }
        if (_exitTalk) {
            _exitTalk = false;
            if (Player.IsWalking()) {
                Player.CanMove = true;
            }
            _talking = false;
            _dialogueBox.SetActive(false);
            _text.Clear();
        }
    }

    private void Update() {
        if (_talking) {
            if (!TextAppear.Appearing && !_suspended) {
                if (!_waitingForAction) {
                    Next();
                } else if (InputReader.GetActionKey()) {
                    Action();
                }
            } else if (_choosing && _suspended) {
                _choice.SetActive(true);
                if (InputReader.GetInputVector().x == -1) {
                    _state = 0;
                    _text.Yes();
                } else if (InputReader.GetInputVector().x == 1) {
                    _state = 1;
                    _text.No();
                }
                if (InputReader.GetActionKey()) {
                    switch (_state) {
                        case 0:
                            _currentTalkable.Choice(true);
                            break;
                        case 1:
                            _currentTalkable.Choice(false);
                            break;
                        default: break;
                    }
                    Action();
                }
            }
        } else if (_canTalk) {
            if (InputReader.GetActionKey()) {
                HideTalkHint();
                _hintInvisibleTime = 0f;
                EnterTalkWithCurrent();
            }
            _hintInvisibleTime += Time.deltaTime;
            if(_hintInvisibleTime > _hintAppearTime) {
                ShowTalkHint();
            }
        } else if (_goSkiing) {
            if (InputReader.GetActionKey()) {
                HideSkiingHint();
                TownLevelManager.LoadNextScene();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        Talkable talkable;
        if (collision.gameObject.TryGetComponent<Talkable>(out talkable)) {
            _currentTalkable = talkable;
            _canTalk = true;
            ShowTalkHint();
        }
        if (collision.tag == "GoSkiing") {
            if (Stages.Mike >= 4 && Stages.Goose >= 4) {
                _goSkiing = true;
                ShowSkiingHint();
            } else {
                ShowCantSkiingHint();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Talkable talkable;
        if (collision.gameObject.TryGetComponent<Talkable>(out talkable)) {
            _currentTalkable = talkable;
            _canTalk = false;
            HideTalkHint();
        }
        if (collision.tag == "GoSkiing") {
            _goSkiing = false;
            HideSkiingHint();
            HideCantSkiingHint();
        }
    }

    public static void ExitTalk() {
        _exitTalk = true;
    }

    public void EnterTalk(Talkable talkable) {
        _currentTalkable = talkable;
        EnterTalkWithCurrent();
    }

    public void EnterTalkWithCurrent() {
        _dialogueBox.SetActive(true);
        Player.CanMove = false;
        _talking = true;
        if (Player.IsWalking()) {
            Player.Animator.SetInteger("RunState", 0);
            Player.Rigidbody.velocity = Vector2.zero;
        }
    }

    private void ShowTalkHint() {
        _talkHint.SetActive(true);
	}

    private void HideTalkHint() {
        _talkHint.SetActive(false);
    }

    private void ShowSkiingHint() {
        _skiingHint.SetActive(true);
    }

    private void HideSkiingHint() {
        _skiingHint.SetActive(false);
    }

    private void ShowCantSkiingHint() {
        _cantSkiingHint.SetActive(true);
    }

    private void HideCantSkiingHint() {
        _cantSkiingHint.SetActive(false);
    }
}
