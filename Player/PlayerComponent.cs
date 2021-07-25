using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/*
PlayerComponent holds the basic properties, such as position and velocities of Player object.
It behaves as the bridge between Player object and all other functions that likes to operate with
Player object.
*/

public enum PlayerAnimation
{
    Idle,
    Jump,
    Fall,
    Sprint,
    AttackUp,
    AttackSide,
    AttackDown,
    Hurt,
    Dead,
    Dump
}

public partial class PlayerComponent : MonoBehaviour
{
    public Text stateDebugText;
    
    // Unity components
    Animator playerAnimator;
    string currentAnimation;

    // === Player status ===
    // [Body size]
    float _bodySizeX;
    float _bodySizeY;
    public Vector2 bodySize { get => new Vector2(_bodySizeX, _bodySizeY); }

    // [Scale]
    public Vector2 scale { get => (Vector2)transform.localScale; }

    // [Position]
    public Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    // [Grounded]
    private bool _grounded = false;
    public bool grounded 
    {
        get => _grounded;
        private set => _grounded = value;
    }
    // [Facing direction]
    private bool _faceRight = true;
    private bool faceRight
    {
        get
        {
            if (Mathf.Abs(velocity.x) > 0)
            {
                if (velocity.x >= 0)
                {
                    _faceRight = true;
                }
                else
                {
                    _faceRight = false;
                }
            }
            return _faceRight;
        }
    }

    // [Velocity]
    private float _velocityX = 0f;
    private float _velocityY = 0f;
    public Vector2 velocity
    {
        get => new Vector2(_velocityX, _velocityY);
        private set
        {
            _velocityX = value.x;
            _velocityY = value.y;
        }
    }
    // [Acceleration]
    private float _accelerationX = 0f;
    private float _accelerationY = 0f;
    public Vector2 acceleration
    {
        get => new Vector2(_accelerationX, _accelerationY);
        private set { _accelerationX = value.x; _accelerationY = value.y; }
    }
    // [Moving step]
    private float movingStep = 0f;  // moving step in the current frame
    
    private PlayerState state;

    // Child classes
    // public partial class PlayerState {}

    // Class functions
    //----------------------------------------------------------------------------------------------
    void Awake()
    {
        // Animator
        playerAnimator = gameObject.GetComponent<Animator>();
        Assert.IsNotNull(playerAnimator);

        // BodySize
        var colliderSize = gameObject.GetComponent<CapsuleCollider2D>().size;
        _bodySizeX = colliderSize.x;
        _bodySizeY = colliderSize.y;

        state = new PlayerStateFree(this);
        state.Enter();
    }

    //----------------------------------------------------------------------------------------------
    void Update()
    {
        PlayerState nextState = state.Update();
        if (nextState != null)
        {
            state = nextState;
            state.Enter();
        }

        stateDebugText.text = $"{state.stateType}";
    }

    //----------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        PlayerState nextState = state.FixedUpdate();
        if (nextState != null)
        {
            state = nextState;
            state.Enter();
        }
    }

    //----------------------------------------------------------------------------------------------
    protected virtual void PlayAnimation(PlayerAnimation anim)
    {
        string nextAnimation = "";    
        switch (anim)
        {
            case PlayerAnimation.Idle:
                nextAnimation = "idle_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.Jump:
                nextAnimation = "jump_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.Fall:
                nextAnimation = "fall_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.Sprint:
                nextAnimation = "sprint_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.AttackSide:
                nextAnimation = "attack_side_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.AttackUp:
                nextAnimation = "attack_up_" + (faceRight ? "r" : "l");
                break;
            case PlayerAnimation.AttackDown:
                nextAnimation = "attack_down_" + (faceRight ? "r" : "l");
                break;
            default:
                break;
        }

        if (nextAnimation != currentAnimation)
        {
            playerAnimator.SetTrigger(nextAnimation);
        }
        currentAnimation = nextAnimation;
    }
}
