using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStateType
{
    Idle,
    Jump,
    Fall,
    PreAttack,
    Attack
}

public partial class BossComponent
{
    public partial class BossState
    {
        public BossStateType stateType;
        protected BossComponent boss;

        // Static states
        protected static BossStateIdle idleState;
        protected static BossStateJump jumpState;
        protected static BossStateFall fallState;
        protected static BossStatePreAttack preAttackState;
        protected static BossStateAttack attackState;
    
        // -----------------------------------------------------------------------------------------
        public BossState(BossComponent boss)
        {
            this.boss = boss;

            // Idle
            if (this is BossStateIdle)
            {
                idleState = (BossStateIdle)this;
            }
            else if (idleState == null)
            {
                idleState = new BossStateIdle(boss);
            }

            // Jump
            if (this is BossStateJump)
            {
                jumpState = (BossStateJump)this;
            }
            else if (jumpState == null)
            {
                jumpState = new BossStateJump(boss);
            }

            // Fall
            if (this is BossStateFall)
            {
                fallState = (BossStateFall)this;
            }
            else if (fallState == null)
            {
                fallState = new BossStateFall(boss);
            }

            // Pre Attack
            if (this is BossStatePreAttack)
            {
                preAttackState = (BossStatePreAttack)this;
            }
            else if (preAttackState == null)
            {
                preAttackState = new BossStatePreAttack(boss);
            }

            // Attack
            if (this is BossStateAttack)
            {
                attackState = (BossStateAttack)this;
            }
            else if (attackState == null)
            {
                attackState = new BossStateAttack(boss);
            }
        }

        // -----------------------------------------------------------------------------------------
        public virtual void Enter()
        {
            
        }

        // -----------------------------------------------------------------------------------------
        public BossState FixedUpdate()
        {
            UpdateVelocity();

            Vector2 offset = new Vector2(0f, boss.bodySize.y * boss.scale.y / 2f);
            Color color = new Color(0f, 1f, 0f);
            Debug.DrawLine(
                boss.position - offset - new Vector2(boss.bodySize.x * boss.scale.x * 0.35f, 0f),
                boss.position - offset + new Vector2(boss.bodySize.x * boss.scale.x * 0.35f, 0f),
                color);

            BossState stateUpdatedByCollision = MaybeUpdateByCollision();
            if (stateUpdatedByCollision != null)
            {
                return stateUpdatedByCollision;
            }

            BossState stateUpdatedByPosition = MaybeUpdateByPosition();
            if (stateUpdatedByPosition != null)
            {
                return stateUpdatedByPosition;
            }

            BossState stateUpdatedByTime = MaybeUpdateByTime();
            if (stateUpdatedByTime != null)
            {
                return stateUpdatedByTime;
            }

            BossState stateUpdatedByPlayer = MaybeUpdateByPlayer();
            if (stateUpdatedByPlayer != null)
            {
                return stateUpdatedByPlayer;
            }
            
            UpdatePosition();

            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected virtual BossState MaybeUpdateByCollision()
        {
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected virtual BossState MaybeUpdateByPosition()
        {
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected virtual BossState MaybeUpdateByTime()
        {
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected virtual BossState MaybeUpdateByPlayer()
        {
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected virtual void UpdateVelocity()
        {
            boss.velocity += boss.acceleration * Time.deltaTime;
            boss.movingStep = boss.velocity.magnitude * Time.deltaTime;
        }

        // -----------------------------------------------------------------------------------------
        protected void UpdatePosition()
        {
            boss.position += boss.velocity.normalized * boss.movingStep;
        }

        // -----------------------------------------------------------------------------------------
        protected void CheckGrounded()
        {
            Vector2 offset = new Vector2(0f, boss.bodySize.y * boss.scale.y / 2f);
            RaycastHit2D[] groundColliders = Physics2D.BoxCastAll(
                boss.position - offset, // origin
                new Vector2(boss.bodySize.x * boss.scale.x * 0.7f, 0.01f),
                0f,
                Vector2.down,
                boss.movingStep
            );

            foreach (RaycastHit2D hit in groundColliders)
            {
                if (hit.transform.gameObject.tag == "blocker")
                {
                    boss.movingStep = Mathf.Min(boss.movingStep, hit.distance);
                    boss.grounded = true;
                    return;
                }
            }
            boss.grounded = false;

        }
    }
}
