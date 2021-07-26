using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BossAnimation
{
    Idle,
    Jump,
    Fall,
    PreAttack,
    Attack
}

public partial class BossComponent : MonoBehaviour
{
    public Transform playerTransform;
    protected Vector2 playerPosition
    {
        get => new Vector2(playerTransform.position.x, playerTransform.position.y);
    }
    
    // Unity components
    Animator bossAnimator;
    string currentAnimation;
    
    private BossState state;
    
    // Body size
    float _bodySizeX;
    float _bodySizeY;
    public Vector2 bodySize { get => new Vector2(_bodySizeX, _bodySizeY); }

    // Scale
    public Vector2 scale { get => (Vector2)transform.localScale; }

    // Position
    public Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    // Grounded
    private bool _grounded = false;
    public bool grounded
    {
        get => _grounded;
        private set => _grounded = value;
    }

    // Velocity
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

    // Facing direction
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

    // Acceleration
    private float _accelerationX = 0f;
    private float _accelerationY = 0f;
    public Vector2 acceleration
    {
        get => new Vector2(_accelerationX, _accelerationY);
        private set
        {
            _accelerationX = value.x;
            _accelerationY = value.y;
        }
    }

    // Moving Step
    private float movingStep = 0f;

    //----------------------------------------------------------------------------------------------
    void Awake()
    {
        // Animator
        bossAnimator = gameObject.GetComponent<Animator>();
        Assert.IsNotNull(bossAnimator);

        // BodySize
        var colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        _bodySizeX = colliderSize.x;
        _bodySizeY = colliderSize.y;

        state = new BossStateFall(this);
        state.Enter();


        // Draw debug box
    }

    //----------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        BossState nextState = state.FixedUpdate();
        if (nextState != null)
        {
            state = nextState;
            state.Enter();
        }
    }

    //----------------------------------------------------------------------------------------------
    protected virtual void PlayAnimation(BossAnimation anim)
    {
        string nextAnimation = "";
        switch (anim)
        {
            case BossAnimation.Idle:
                nextAnimation = "idle_" + (faceRight ? "r" : "l");
                break;
            case BossAnimation.Jump:
                nextAnimation = "jump_" + (faceRight ? "r" : "l");
                break;
            case BossAnimation.Fall:
                nextAnimation = "fall_" + (faceRight ? "r" : "l");
                break;
            case BossAnimation.PreAttack:
                nextAnimation = "pre_attack_" + (faceRight ? "r" : "l");
                break;
            case BossAnimation.Attack:
                nextAnimation = "attack_" + (faceRight ? "r" : "l");
                break;
            default:
                break;
        }

        if (nextAnimation != currentAnimation)
        {
            bossAnimator.SetTrigger(nextAnimation);
        }
        currentAnimation = nextAnimation;
    }
}
