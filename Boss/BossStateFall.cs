using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStateFall : BossState
    {
        // -----------------------------------------------------------------------------------------
        public BossStateFall(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Fall;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            // Set Animation
            boss.PlayAnimation(BossAnimation.Fall);

            // Set Motion
            boss.velocity = new Vector2(boss.velocity.x, 0f);
            boss.acceleration = new Vector2(0, -Constants.gravityAcceleration);
        }

        // -----------------------------------------------------------------------------------------
        protected override void UpdateVelocity()
        {
            base.UpdateVelocity();
            if (boss.velocity.y < 0 &&
                boss.velocity.magnitude > Constants.maxFallingSpeed)
            {
                boss.velocity = boss.velocity.normalized * Constants.maxFallingSpeed;
            }
        }

        // -----------------------------------------------------------------------------------------
        protected override BossState MaybeUpdateByCollision()
        {
            CheckGrounded();
            if (boss.grounded)
            {
                return idleState;
            }
            return null;
        }
    }
}