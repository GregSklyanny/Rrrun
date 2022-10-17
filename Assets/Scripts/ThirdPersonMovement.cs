using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private MovementCharacteristics _characteristics;
    [SerializeField] private Text _highscoretext;
    [SerializeField] private Text _scoretext;

    private float _vertical, _horizontal, _run, _jump;

    private readonly string STR_VERTICAL = "Vertical";
    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_RUN = "Run";
    private readonly string STR_JUMP = "Jump";
    private int _score = 0;
    private int _highscore = 0;

    private const float DISTANCE_OFFSET_CAMERA = 5f;

    private CharacterController _controller;
    private Animator _animator;

    private Vector3 _direction;

    private Quaternion _look;

    private Vector3 TargetRotate => _camera.right * DISTANCE_OFFSET_CAMERA;

    private bool Idle => _horizontal == 0.0f && _vertical == 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        Cursor.visible = _characteristics.VisibleCursor;

        if (PlayerPrefs.HasKey("highscore"))
        {
            _highscore = PlayerPrefs.GetInt("highscore");
            _highscoretext.text = $"Highscore: {_highscore}";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        _score++;
        _scoretext.text = $"Score: {_score}";
        // Rotate();
    }

    private void Movement() {

        
       

        if (_controller.isGrounded)
        {
            _horizontal = Input.GetAxis(STR_HORIZONTAL);
            //_vertical = Input.GetAxis(STR_VERTICAL);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _run = 1.0f;
            }
            else
            {
                _run = 0.0f;
            }

            _direction = transform.TransformDirection(_horizontal, 0, 1f).normalized;


            PlayAnimation();
            Jump();
        }

        _direction.y -= _characteristics.Gravity * Time.deltaTime;//
        float speed = _run * _characteristics.RunSpeed + _characteristics.MovementSpeed;
        Vector3 dir = _direction * speed * Time.deltaTime;// 

        dir.y = _direction.y;
        dir.z = -_characteristics.RightLeftMove* _horizontal * Time.deltaTime;//
        if (transform.position.z >= 11.7 && dir.z > 0)
            {
            dir.z = 0;
            }
        if (transform.position.z < -11.7 && dir.z < 0)
            {
            dir.z = 0;
        }
        _controller.Move(dir);

    }

    private void Rotate() {
        //return;
        if (Idle) return;
        Vector3 target = TargetRotate;
        target.y = 0;

        _look = Quaternion.LookRotation(target);
        float speed = _characteristics.AngularSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed);
    }

    protected void Jump() {
        if (Input.GetKeyDown(KeyCode.J)) {
            _animator.SetTrigger(STR_JUMP);
            _direction.y += _characteristics.JumpForce;
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Barrier") {
            if (_score > _highscore) PlayerPrefs.SetInt("highscore", _score);
            SceneManager.LoadScene("Mainmenu", LoadSceneMode.Single);
        }
    } 
    private void PlayAnimation() {

        float horizontal = _run * _horizontal + _horizontal;
        float vertical = _run * _vertical + _vertical;
        
       // _animator.SetFloat(STR_VERTICAL, vertical);
        _animator.SetFloat(STR_HORIZONTAL, horizontal);
    }
}
