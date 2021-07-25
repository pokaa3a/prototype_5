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
    }
}