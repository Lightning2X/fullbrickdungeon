﻿using Microsoft.Xna.Framework;

abstract class AImonster : Monster
{
    protected Vector2 previousPos;
    protected BaseAI AI;
    /// <summary>
    /// Class that defines a Monster with Artificial intelligence (for more info on the AI, see the BaseAI class)
    /// </summary>
    /// <param name="movementSpeed">How fast the AI of this monster moves</param>
    /// <param name="currentLevel">The Current level this monster is in</param>
    /// <param name="id">ID of the monster</param>
    public AImonster(float movementSpeed, Level currentLevel, string id) : base(id, currentLevel)
    {
        // Make a new AI
        AI = new BaseAI(this, movementSpeed, currentLevel);
    }

    /// <summary>
    /// Updates the AI Monster
    /// </summary>
    /// <param name="gameTime">Current game time</param>
    public override void Update(GameTime gameTime)
    {
        previousPos = position;
        // Updates the AI
        AI.Update(gameTime);
        // Checks which animation the Monster should be playing
        if (AI.IsAttacking)
        {
            PlayAnimation("attack");
            if (previousPos.X < position.X)
                this.Mirror = true;
            else if (previousPos.X > position.X)
                this.Mirror = false;
        }
        else if (previousPos.Y > position.Y)
        {
            PlayAnimation("walk_back");
            if (previousPos.X < position.X)
                this.Mirror = false;
            else if (previousPos.X > position.X)
                this.Mirror = true;
        }
        else if (previousPos.X < position.X)
        {
            PlayAnimation("walk");
            this.Mirror = true;
        }
        else if (previousPos.X > position.X)
        {
            PlayAnimation("walk");
            this.Mirror = false;
        }
        else
            PlayAnimation("idle");

        base.Update(gameTime);
    }
    
}
