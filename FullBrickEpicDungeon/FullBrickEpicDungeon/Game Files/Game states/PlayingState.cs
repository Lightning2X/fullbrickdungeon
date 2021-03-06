﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

class PlayingState : IGameLoopObject
{
    // level list
    protected Level[] levelArray;
    private TimeSpan levelTimer;
    // current level that is being used
    protected int currentLevelIndex;
    // amount of levels in the game
    protected const int numberOfLevels = 10;
    protected bool cheats;

    /// <summary>
    /// A state that defines the playing state of the game
    /// </summary>
    public PlayingState()
    {
        currentLevelIndex = 1;
        levelArray = new Level[numberOfLevels + 1]; //11 levels
    }

    /// <summary>
    /// Handles input, making it able for the player to pause the game
    /// </summary>
    public void HandleInput(InputHelper inputHelper)
    {
        CurrentLevel.HandleInput(inputHelper);
        
        if (inputHelper.KeyPressed(Keys.Space) || inputHelper.KeyPressed(Keys.Escape) || inputHelper.AnyPlayerPressed(Buttons.Start))
        {
            if(FullBrickEpicDungeon.DungeonCrawler.SFX)
                GameEnvironment.AssetManager.PlaySound("Assets/SFX/pause");

            GameEnvironment.GameStateManager.SwitchTo("pauseState");
        }

        if (inputHelper.KeyPressed(Keys.OemTilde) || inputHelper.ButtonPressed(1, Buttons.LeftShoulder))
        {
            cheats = !cheats;
        }
        if (cheats)
        {
            CheatsHandler(inputHelper);
        }
        
    }

    /// <summary>
    /// A cheat menu that allows the player to switch levels instantly
    /// </summary>
    private void CheatsHandler(InputHelper inputHelper)
    {
        if((inputHelper.KeyPressed(Keys.F1) || inputHelper.ButtonPressed(1, Buttons.DPadLeft)) && currentLevelIndex > 1)
        {
            currentLevelIndex--;
            this.Reset();
        }
        else if ((inputHelper.KeyPressed(Keys.F2) || inputHelper.ButtonPressed(1, Buttons.DPadRight)) && currentLevelIndex < numberOfLevels)
        {
            currentLevelIndex++;
            this.Reset();
        }
    }

    public void Initialize()
    {
        FullBrickEpicDungeon.DungeonCrawler.mouseVisible = false;
        MediaPlayer.Stop();
        if(FullBrickEpicDungeon.DungeonCrawler.music)
            GameEnvironment.AssetManager.PlayMusic("Assets/Music/ingame");
    }


    public void Reset()
    {
        levelTimer = TimeSpan.Zero;
        Level newlevel = new Level(currentLevelIndex);
        levelArray[currentLevelIndex] = newlevel;
    }

    public void Update(GameTime gameTime)
    {
        levelTimer += gameTime.ElapsedGameTime;
        CurrentLevel.Update(gameTime);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        CurrentLevel.Draw(gameTime, spriteBatch);
    }



    public void GoToNextLevel()
    {
        CurrentLevel.Reset();
        if (currentLevelIndex >= levelArray.Length - 1)
        {
            ResetLevelIndex();
            // if all the levels are over switch to the title state
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
        else
        {
            currentLevelIndex++;
        }
    }

    public void ResetLevelIndex()
    {
        currentLevelIndex = 1;
    }

    // returns the current level being used in the state
    public Level CurrentLevel
    {
        get { return levelArray[currentLevelIndex]; }
    }

    /// <summary>
    /// Property used to tell the levelfinished state how long the players took over the current level
    /// </summary>
    public TimeSpan LevelTime
    {
        get { return levelTimer; }
        set { levelTimer = value; }
    }

    public int LevelIndex
    {
        get { return currentLevelIndex; }
    }

    public int TotalLevels
    {
        get { return numberOfLevels; }
    }

}
