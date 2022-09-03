using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour {

    // Game state -----------
    public enum GameState {
        Walk,
        Skiing,
        Ice
    }

    private static GameState _gameState = GameState.Walk;
    // -------------

    // Tangerines ----------
    private static int _tangerines = 0;

    public static void AddTangerines(int add) {
        _tangerines += add;
    }

    public static int Tangerines {
        get {
            return _tangerines;
        }
    }
    // --------------

    [SerializeField] private Transform _respawnPosition;
    [SerializeField] private float _maxSpeed = 1f;
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _maxSkiSpeed = 10f;
    [SerializeField] private float _skiAcceleration = 10f;
    [SerializeField] private float _skiDownAcceleration = 10f;
    [SerializeField] private float _crackAppearTime = 1f;
    [SerializeField] private float _crackSpeed = 0.5f;
    
    private float _crackingTime = 0f;
    private int _runState = 0;
    private float _angle = 0; 

    private Vector2 _velocity = Vector2.zero;
    private Vector2 _movement;

    private static Rigidbody2D _rigidbody;
    public static Rigidbody2D Rigidbody { get => _rigidbody; }

    private static Animator _animator;
    public static Animator Animator { get => _animator; }

    public static bool CanMove = true;
    private static bool _alive = true;

    private void Awake() { // Awake to let everything steal static stuff
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (_alive) { // Alive

            _movement = InputReader.GetInputVector(); // Read keyboard input

            if (_gameState == GameState.Skiing) { // We are skiing
                _velocity = _rigidbody.velocity;
                if (CanMove) {
                    _velocity.y += _skiDownAcceleration * Time.deltaTime;
                    if (_velocity.y > _maxSkiSpeed) {
                        _velocity.y = _maxSkiSpeed;
                    }
                } else {
                    _velocity.y -= _skiDownAcceleration * Time.deltaTime;
                    if(_velocity.y < 0) {
                        _velocity.y = 0;
                    }
                }

                if (_movement.x > 0 && CanMove) {
                    _velocity.x += _skiAcceleration * Time.deltaTime;
                    if (_velocity.x > _maxSkiSpeed) {
                        _velocity.x = _maxSkiSpeed;
                    }
                } else if (_movement.x < 0 && CanMove) {
                    _velocity.x -= _skiAcceleration * Time.deltaTime;
                    if (_velocity.x < -_maxSkiSpeed) {
                        _velocity.x = -_maxSkiSpeed;
                    }
                } else {
                    if (_velocity.x > 0) {
                        _velocity.x -= _skiAcceleration * Time.deltaTime * _velocity.normalized.x;
                        if (_velocity.x < 0) {
                            _velocity.x = 0;
                        }
                    } else if (_velocity.x < 0) {
                        _velocity.x += _skiAcceleration * Time.deltaTime * Mathf.Abs(_velocity.normalized.x);
                        if (_velocity.x > 0) {
                            _velocity.x = 0;
                        }
                    }
                }

                _rigidbody.velocity = _velocity;
                _angle = Mathf.Asin(_velocity.normalized.x)*360f/Mathf.PI;

                _animator.SetFloat("Angle", _angle);

            } else if (CanMove) { // We are free to move

                _movement.Normalize();

                // Do animator stuff --------------
                if (_movement.x == 0) {
                    if (_movement.y == 0) {
                        _runState = 0;
                    } else if (_movement.y < 0) {
                        _runState = 1;
                    } else if (_movement.y > 0) {
                        _runState = 2;
                    }
                } else if (_movement.x > 0) {
                    if (_movement.y > 0) {
                        _runState = 2;
                    } else if (_movement.y == 0) {
                        _runState = 3;
                    } else if (_movement.y < 0) {
                        _runState = 1;
                    }
                } else if (_movement.x < 0) {
                    if (_movement.y > 0) {
                        _runState = 2;
                    } else if (_movement.y == 0) {
                        _runState = 4;
                    } else if (_movement.y < 0) {
                        _runState = 1;
                    }
                }
                _animator.SetInteger("RunState", _runState);
                // --------------------

                if (_gameState == GameState.Walk) { // If we are walking we just walk
                    _rigidbody.velocity = _movement * _maxSpeed;
                } else if (_gameState == GameState.Ice) { // But if we are on ice it is a little more difficult
                    
                    _velocity = _rigidbody.velocity;

                    // Accelerate -----------
                    _velocity += _movement * _acceleration * Time.deltaTime;
                    //if (Mathf.Abs(_velocity.x) > _maxSpeed) {
                    //    _velocity.x = Mathf.Sign(_velocity.x) * _maxSpeed;
                    //}
                    //if (Mathf.Abs(_velocity.y) > _maxSpeed) {
                    //    _velocity.y = Mathf.Sign(_velocity.y) * _maxSpeed;
                    //}
                    if (_velocity.magnitude > _maxSpeed) {
                        _velocity = _velocity.normalized * _maxSpeed;
                    }
                    // --------------------

                    // Decelerate ------------------
                    if (_movement.x == 0) {
                        if (_velocity.x > 0) {
                            _velocity.x -= _acceleration * Time.deltaTime * _velocity.normalized.x;
                            if (_velocity.x < 0) {
                                _velocity.x = 0;
                            }
                        } else if (_velocity.x < 0) {
                            _velocity.x += _acceleration * Time.deltaTime * Mathf.Abs(_velocity.normalized.x);
                            if (_velocity.x > 0) {
                                _velocity.x = 0;
                            }
                        }
                    }
                    if (_movement.y == 0) {
                        if (_velocity.y > 0) {
                            _velocity.y -= _acceleration * Time.deltaTime * _velocity.normalized.y;
                            if (_velocity.y < 0) {
                                _velocity.y = 0;
                            }
                        } else if (_velocity.y < 0) {
                            _velocity.y += _acceleration * Time.deltaTime * Mathf.Abs(_velocity.normalized.y);
                            if (_velocity.y > 0) {
                                _velocity.y = 0;
                            }
                        }
                    }
                    // --------------------

                    // If we are too slow we die -------------
                    if (_velocity.magnitude < _crackSpeed) {
                        _crackingTime += Time.deltaTime;
                    } else {
                        _crackingTime = 0f;
                    }
                    if(_crackingTime > _crackAppearTime) {
                        _crackingTime = 0f;
                        transform.Find("Crack").gameObject.SetActive(true);
                        Die();
                    }
                    // ----------------------

                    _rigidbody.velocity = _velocity;
                }
            }
        } else { // Dead
            _rigidbody.velocity = Vector2.zero; // moven't
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Ice") {
            _gameState = GameState.Ice; // Ice is slippery
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ice") { // Ice is slippery
            _gameState = GameState.Ice;
        } else if (collision.tag == "Crack") { // Ice is dangerous
            Die();
        } else if (collision.tag == "Tree") { // You bumped in a tree ahah
            Die();
        } else if (collision.tag == "Tangerine") { // Yum
            AddTangerines(1);
            collision.gameObject.SetActive(false);
        } else if (collision.tag == "End") {
            CanMove = false;
            GetComponent<Talk>().EnterTalkWithCurrent();
            if (_gameState == GameState.Skiing) {
                AudioManager.instance.StopHillTheme();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Ice") {
            _gameState = GameState.Walk; // No more ice
        }
    }

    private void Die() {
        if (_alive) {
            if (_gameState == GameState.Skiing) {
                _animator.speed = 0f;
                CanMove = false;
            } else {
                _animator.SetInteger("RunState", 0);
            }
            _alive = false;
            _rigidbody.velocity = Vector2.zero;
            StartCoroutine(RespawnTimer());
        }
    }

    private void Respawn() {
        if (_gameState == GameState.Skiing) {
            _animator.speed = 1f;
            CanMove = true;
            SkiingManager.Reset();
            _tangerines = SkiingManager.Tangerines;
            _animator.SetFloat("Angle", 0);
        } else {
            IceLevelManager.Reset();
            _tangerines = IceLevelManager.Tangerines;
            _animator.Play("Idle front 0");
            transform.Find("Crack").gameObject.SetActive(false);
            _gameState = GameState.Walk;
        }
        _alive = true;
        transform.position = _respawnPosition.position;
    }

    private IEnumerator RespawnTimer() {
        for (int i = 0; i < 10; ++i) {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = (i % 2) > 0;
            yield return new WaitForSeconds(0.2f);
        }
        Respawn();
    }

    public static void SetGameStateToSkiing() {
        _gameState = GameState.Skiing;
    }

    public static void SetGameStateToIce() {
        _gameState = GameState.Ice;
    }

    public static void SetGameStateToWalk() {
        _gameState = GameState.Walk;
    }

    public static bool IsWalking() {
        return _gameState == GameState.Walk;
	}
}
