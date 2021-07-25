using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossComponent
{
    public class BossStateJump : BossState
    {
        private float targetHeight = 0f;
        
        // -----------------------------------------------------------------------------------------
        public BossStateJump(BossComponent boss) : base(boss)
        {
            stateType = BossStateType.Jump;
        }

        // -----------------------------------------------------------------------------------------
        public void SetHeight(float h)
        {
            targetHeight = h;
        }
    }
}