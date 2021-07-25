using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStateIdle : BossState
    {
        // -----------------------------------------------------------------------------------------
        public BossStateIdle(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Idle;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            // Set Animation
            boss.PlayAnimation(BossAnimation.Idle);
        }

        // -----------------------------------------------------------------------------------------
        protected override BossState MaybeUpdateByPlayer()
        {
            bool playerIsCloseEnough = true;

            if (playerIsCloseEnough)
            {
                preAttackState.PreSet(true);
                return preAttackState;
            }

            return null;
        }

    }
}
