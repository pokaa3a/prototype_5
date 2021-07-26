using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStateIdle : BossState
    {
        private const float durationSec = 1f;
        private float enterTime;

        // -----------------------------------------------------------------------------------------
        public BossStateIdle(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Idle;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            enterTime = Time.time;
            
            // Set Animation
            boss.PlayAnimation(BossAnimation.Idle);

            // Set Motion
            boss.velocity = Vector2.zero;
            boss.acceleration = Vector2.zero;
        }

        // -----------------------------------------------------------------------------------------
        protected override BossState MaybeUpdateByPlayer()
        {
            if (Time.time >= enterTime + durationSec)
            {
                // TODO: Better condition of making boss's decision
                
                // Vertical
                if (boss.playerPosition.y > boss.position.y + 0.5f)
                {
                    jumpState.SetDirection(BossJumpDirection.Up);
                    return jumpState;
                }
                if (boss.playerPosition.y < boss.position.y - 0.5f)
                {
                    return fallState;
                }

                // Horizontal
                if (boss.playerPosition.x > boss.position.x + Constants.bossAttackRange)
                {
                    jumpState.SetDirection(BossJumpDirection.Right);
                    return jumpState;
                }
                if (boss.playerPosition.x < boss.position.x - Constants.bossAttackRange)
                {
                    jumpState.SetDirection(BossJumpDirection.Left);
                    return jumpState;
                }

                // Attackable
                if (boss.playerPosition.x > boss.position.x)
                {
                    preAttackState.PreSet(true);
                    return preAttackState;
                }
                else
                {
                    preAttackState.PreSet(false);
                    return preAttackState;
                }
            }
            return null;
        }

    }
}
