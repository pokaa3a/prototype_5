using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Free state can be either statically standing on the floor, or be
// freely falling down.

public partial class PlayerComponent
{
    public class PlayerStateFree : PlayerState
    {
        // -----------------------------------------------------------------------------------------
        public PlayerStateFree(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.Free;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            // Set Animation
            if (player.grounded)
            {
                player.PlayAnimation(PlayerAnimation.Idle);
            }
            else
            {
                player.PlayAnimation(PlayerAnimation.Fall);
            }

            // Set Motion
            player.velocity = new Vector2(0f, player.velocity.y);
            player.acceleration = new Vector2(0, -Constants.gravityAcceleration);
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByInput()
        {
            if (InputHandler.inputDirection == InputDirection.North &&
                InputHandler.prevDirection == InputDirection.None)
            {
                return jumpState;
            }
            else if (InputHandler.inputDirection == InputDirection.East &&
                InputHandler.prevDirection == InputDirection.None)
            {
                return sprintRightState;
            }
            else if (InputHandler.inputDirection == InputDirection.West &&
                InputHandler.prevDirection == InputDirection.None)
            {
                return sprintLeftState;
            }
            else if (InputHandler.inputDirection == InputDirection.South &&
                InputHandler.prevDirection == InputDirection.None)
            {
                return dumpState;
            }

            if (!player.grounded)
            {
                player.velocity = new Vector2(
                    InputHandler.inputAngle * Constants.angleToSpeed,
                    player.velocity.y);
                player.PlayAnimation(PlayerAnimation.Fall);
            }

            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByCollision()
        {
            CheckGrounded();
            if (player.grounded)
            {
                player.PlayAnimation(PlayerAnimation.Idle);
            }
            else
            {
                player.PlayAnimation(PlayerAnimation.Fall);
            }
            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected override void UpdateVelocity()
        {
            if (player.grounded)
            {
                player.velocity = Vector2.zero;
            }
            else
            {
                base.UpdateVelocity();
                if (Vector2.Dot(player.velocity, Vector2.down) > 0 &&
                    player.velocity.magnitude > Constants.maxFallingSpeed)
                {
                    player.velocity = player.velocity.normalized * Constants.maxFallingSpeed;
                }
            }
        }
    }
}
