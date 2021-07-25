using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerComponent
{
    public class PlayerStateJump : PlayerState
    {
        private float startPositionY;

        // -----------------------------------------------------------------------------------------
        public PlayerStateJump(PlayerComponent player) : base(player)
        {
            stateType = PlayerStateType.Jump;
        }

        // -----------------------------------------------------------------------------------------
        public override void Enter()
        {
            // Set motion
            startPositionY = player.position.y;
            player.velocity = Vector2.up.normalized * Constants.jumpSpeed;
            player.acceleration = Vector2.zero;
            
            // Set Animation
            player.PlayAnimation(PlayerAnimation.Jump);
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByInput()
        {   
            if (InputHandler.inputDirection == InputDirection.None ||
                (InputHandler.inputDirection == InputDirection.East &&
                InputHandler.prevDirection == InputDirection.North) ||
                (InputHandler.inputDirection == InputDirection.West &&
                InputHandler.prevDirection == InputDirection.North))
            {
                return freeState;
            }

            player.velocity = new Vector2(
                InputHandler.inputAngle * Constants.angleToSpeed,
                player.velocity.y);
            player.PlayAnimation(PlayerAnimation.Jump);

            return null;
        }

        // -----------------------------------------------------------------------------------------
        protected override PlayerState MaybeUpdateByPosition()
        {
            if (Mathf.Abs(player.position.y - startPositionY) + player.movingStep >= 
                Constants.highestDistance)
            {
                player.movingStep = Mathf.Min(player.movingStep,
                    Constants.highestDistance - Mathf.Abs(player.position.y - startPositionY));
                return freeState;
            }
            return null;
        }
    }
}