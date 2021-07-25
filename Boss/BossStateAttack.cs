using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStateAttack : BossState
    {
        private const float durationSec = 0.5f;

        private bool attackRight = true;
        private float enterTime;

        // -----------------------------------------------------------------------------------------
        public BossStateAttack(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Attack;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            enterTime = Time.time;
        
            // Set Animation
            boss.PlayAnimation(BossAnimation.Attack);
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
                return idleState;
            }
            return null;
        }
    }
}