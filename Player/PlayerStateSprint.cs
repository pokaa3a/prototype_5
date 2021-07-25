using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerComponent
{
    public class PlayerStateSprint : PlayerState
    {
        protected const float longestDistance = 2f;
        protected const float sprintSpeed = 3f;
        protected Vector2 sprintDirection;
        protected float startPositionX;

        // -----------------------------------------------------------------------------------------
        public PlayerStateSprint(PlayerComponent player) : base(player) {}
    
        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            // Set Motion
            startPositionX = player.position.x;
            sprintDirection = this is PlayerStateSprintLeft ? Vector2.left : Vector2.right;
            player.velocity = sprintDirection * sprintSpeed;
            player.acceleration = Vector2.zero;
            
            // Set Animation
            player.PlayAnimation(PlayerAnimation.Sprint);
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByInput()
        {
            if (InputHandler.inputDirection == InputDirection.None)
            {
                return freeState;
            }
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByPosition()
        {
            if (Mathf.Abs(player.position.x - startPositionX) + player.movingStep >= longestDistance)
            {
                player.movingStep = Mathf.Min(player.movingStep, 
                    longestDistance - Mathf.Abs(player.position.x - startPositionX));
                return freeState;
            }
            return null;
        }
    }

}

public partial class PlayerComponent
{
    public class PlayerStateSprintRight : PlayerStateSprint
    {
        // -----------------------------------------------------------------------------------------
        public PlayerStateSprintRight(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.SprintRight;
        }
    }
}

public partial class PlayerComponent
{
    public class PlayerStateSprintLeft : PlayerStateSprint
    {
        // -----------------------------------------------------------------------------------------
        public PlayerStateSprintLeft(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.SprintLeft;
        }
    }
}
