using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Free,
    Jump,
    SprintLeft,
    SprintRight,
    Hurt,
    Dump
}

public partial class PlayerComponent
{ 
    public partial class PlayerState
    {
        public PlayerStateType stateType;
        protected PlayerComponent player;

        // Static states
        protected static PlayerStateFree freeState;
        protected static PlayerStateSprint sprintRightState;
        protected static PlayerStateSprint sprintLeftState;
        protected static PlayerStateJump jumpState;
        protected static PlayerStateHurt hurtState;
        protected static PlayerStateDump dumpState;

        //------------------------------------------------------------------------------------------
        // Whenever any PlayerState object is initialized, all the static states are created.
        public PlayerState(PlayerComponent player)
        {
            this.player = player;

            // Free
            if (this is PlayerStateFree)
            {
                freeState = (PlayerStateFree)this;
            }
            else if (freeState == null)
            {
                freeState = new PlayerStateFree(player);
            }

            // Sprint Right
            if (this is PlayerStateSprintRight)
            {
                sprintRightState = (PlayerStateSprintRight)this;
            }
            else if (sprintRightState == null)
            {
                sprintRightState = new PlayerStateSprintRight(player);
            }

            // Sprint Left
            if (this is PlayerStateSprintLeft)
            {
                sprintLeftState = (PlayerStateSprintLeft)this;
            }
            else if (sprintLeftState == null)
            {
                sprintLeftState = new PlayerStateSprintLeft(player);
            }

            // Jump
            if (this is PlayerStateJump)
            {
                jumpState = (PlayerStateJump)this;
            }
            else if (jumpState == null)
            {
                jumpState = new PlayerStateJump(player);
            }

            // Hurt
            if (this is PlayerStateHurt)
            {
                hurtState = (PlayerStateHurt)this;
            }
            else if (hurtState == null)
            {
                hurtState = new PlayerStateHurt(player);
            }

            // Dump
            if (this is PlayerStateDump)
            {
                dumpState = (PlayerStateDump)this;
            }
            else if (dumpState == null)
            {
                dumpState = new PlayerStateDump(player);
            }
        }

        //------------------------------------------------------------------------------------------
        // This function will be called in each Update() function of PlayerComponent.
        public PlayerState Update()
        {
            PlayerState stateUpdatedByInput = MaybeUpdateByInput();
            if (stateUpdatedByInput != null)
            {
                return stateUpdatedByInput;
            }
            return null;
        }

        //------------------------------------------------------------------------------------------
        // This function will be called in each FixedUpdate() function of PlayerComponent.
        public PlayerState FixedUpdate()
        {
            UpdateVelocity();

            PlayerState stateUpdatedByCollision = MaybeUpdateByCollision();
            if (stateUpdatedByCollision != null)
            {
                return stateUpdatedByCollision;
            }

            PlayerState stateUpdatedByPosition  = MaybeUpdateByPosition();
            if (stateUpdatedByPosition != null)
            {
                return stateUpdatedByPosition;
            }

            UpdatePosition();

            return null;
        }
    
        //------------------------------------------------------------------------------------------
        protected virtual PlayerState MaybeUpdateByInput()
        {
            return null;
        }

        //------------------------------------------------------------------------------------------
        protected virtual PlayerState MaybeUpdateByCollision()
        {
            return null;
        }

        //------------------------------------------------------------------------------------------
        protected virtual PlayerState MaybeUpdateByPosition()
        {
            return null;
        }

        //------------------------------------------------------------------------------------------
        public virtual void Enter() {}

        //------------------------------------------------------------------------------------------
        protected virtual void UpdateVelocity()
        {
            player.velocity += player.acceleration * Time.deltaTime;
            player.movingStep = player.velocity.magnitude * Time.deltaTime;
        }

        //------------------------------------------------------------------------------------------
        // Q: Does this need to be an individual function?
        protected void UpdatePosition()
        {
            player.position += player.velocity.normalized * player.movingStep;
        }

        // -----------------------------------------------------------------------------------------
        protected void CheckGrounded(bool updateMovingStep = true)
        {
            Vector2 offset = new Vector2(0f, 0.28f);
            RaycastHit2D[] groundColliders = Physics2D.BoxCastAll(
                player.position - offset,  // origin
                new Vector2(player.bodySize.x * player.scale.x * 0.7f, 0.01f),    // box size
                0.0f,               // angle of the box
                Vector2.down,       // direction of the box
                player.movingStep   // maximum distance
            );

            foreach (RaycastHit2D hit in groundColliders)
            {
                if (hit.transform.gameObject.tag == "blocker")
                {
                    if (updateMovingStep)
                    {
                        player.movingStep = Mathf.Min(player.movingStep, hit.distance);
                    }
                    player.grounded = true;
                    return;
                }
            }
            player.grounded = false;
        }
    }
}
