using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerComponent
{
    public class PlayerStateDump : PlayerState
    {
        bool wasGrounded = false;
        
        // -----------------------------------------------------------------------------------------
        public PlayerStateDump(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.Dump;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            wasGrounded = player.grounded;

            // Set motion
            player.velocity = Vector2.down.normalized * Constants.dumpSpeed;
            player.acceleration = Vector2.zero;

            // Set Animation
            player.PlayAnimation(PlayerAnimation.Fall);
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByInput()
        {
            if (InputHandler.inputDirection == InputDirection.None ||
                (InputHandler.inputDirection == InputDirection.East &&
                InputHandler.prevDirection == InputDirection.South) ||
                (InputHandler.inputDirection == InputDirection.West &&
                InputHandler.prevDirection == InputDirection.South))
            {
                return freeState;
            }
            
            float angle = (InputHandler.inputAngle > 0 ? 180f : -180f) - InputHandler.inputAngle;
            player.velocity = new Vector2(
                angle * Constants.angleToSpeed,
                player.velocity.y);
            player.PlayAnimation(PlayerAnimation.Fall);

            return null;
        }
        
        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByCollision()
        {
            CheckGrounded(false);
            if (wasGrounded && !player.grounded)
            {
                wasGrounded = false;
            }

            if (!wasGrounded && player.grounded)
            {
                CheckGrounded(true);
                return freeState;
            }

            return null;
        }
    }
}
