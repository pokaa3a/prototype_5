using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStatePreAttack : BossState
    {
        private const float durationSec = 1f;
        
        private bool attackRight = true;
        private float enterTime;

        // -----------------------------------------------------------------------------------------
        public BossStatePreAttack(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.PreAttack;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            enterTime = Time.time;

            // Set Animation
            boss.PlayAnimation(BossAnimation.PreAttack);
        }

        // -----------------------------------------------------------------------------------------
        public void PreSet(bool direction)
        {
            attackRight = direction;
        }

        // -----------------------------------------------------------------------------------------
        protected override BossState MaybeUpdateByTime()
        {
            if (Time.time >= enterTime + durationSec)
            {
                attackState.PreSet(attackRight);
                return attackState;
            }
            return null;
        }
    }
}
