using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossJumpDirection
{
    Up,
    Right,
    Left
}

public partial class BossComponent
{
    public class BossStateJump : BossState
    {
        private BossJumpDirection direction = BossJumpDirection.Up;
        private float distance = 0f;
        private Vector2 startPosition;

        // -----------------------------------------------------------------------------------------
        public BossStateJump(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Jump;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            startPosition = boss.position;
            
            // Set Motion
            if (direction == BossJumpDirection.Right)
            {
                boss.velocity = new Vector2(2f, 1f).normalized * Constants.jumpSpeed;
                boss.acceleration = Vector2.zero;
            
                distance = Mathf.Min(
                    Mathf.Abs(boss.playerPosition.x - boss.position.x) - Constants.bossAttackRange,
                    Constants.horizontalJumpDistance);
                distance /= 2f;
            }
            else if (direction == BossJumpDirection.Left)
            {
                boss.velocity = new Vector2(-2f, 1f).normalized * Constants.jumpSpeed;
                boss.acceleration = Vector2.zero;

                distance = Mathf.Min(
                    Mathf.Abs(boss.playerPosition.x - boss.position.x) - Constants.bossAttackRange,
                    Constants.horizontalJumpDistance);
                distance /= 2f;
            }
            else    // Up
            {
                boss.velocity = Vector2.up * Constants.jumpSpeed;

                distance = Constants.verticalJumpDistance;
            }

            // Set Animation
            boss.PlayAnimation(BossAnimation.Jump);
        }

        // -----------------------------------------------------------------------------------------
        public void SetDirection(BossJumpDirection dir)
        {
            direction = dir;
        }

        // -----------------------------------------------------------------------------------------
        protected override BossState MaybeUpdateByPosition()
        {
            if (direction == BossJumpDirection.Right ||
                direction == BossJumpDirection.Left)
            {
                if (Mathf.Abs(boss.position.x - startPosition.x) >= distance)
                {
                    return fallState;
                }
            }
            else    // Up
            {
                if (Mathf.Abs(boss.position.y - startPosition.y) >= distance)
                {
                    return fallState;
                }
            }

            return null;
        }
    }
}