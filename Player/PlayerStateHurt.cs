using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerComponent
{
    public class PlayerStateHurt : PlayerState
    {
        // -----------------------------------------------------------------------------------------
        public PlayerStateHurt(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.Hurt;
        }
    }
}
