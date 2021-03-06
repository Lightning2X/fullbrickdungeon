﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

class TitleMenuState : MenuState
{
    Button startButton, settingsButton, creditsButton, quitButton;
    Texture2D background;


    /// <summary>
    /// Class that defines the Title Menu of the game
    /// </summary>
    public TitleMenuState() : base()
    {
        // load the background
        background = GameEnvironment.AssetManager.GetSprite("Assets/Cutscenes/LarryShits");
    }


    protected override void FillButtonList()
    {
        // Make the button seperation less so all buttons fit on screen
        buttonSeparation = 200;
        //Load buttons for the title screen
        startButton = new Button("Assets/Sprites/Menu/StartButton");
        buttonList.Add(startButton);

        settingsButton = new Button("Assets/Sprites/Menu/SettingsButton");
        buttonList.Add(settingsButton);

        creditsButton = new Button("Assets/Credits/credits");
        buttonList.Add(creditsButton);

        quitButton = new Button("Assets/Sprites/Menu/exitgame");
        buttonList.Add(quitButton);

        

        base.FillButtonList();
    }

    public override void Initialize()
    {
        // play music if there is no music already playing and the global boolean is set to true
        if(MediaPlayer.State == MediaState.Stopped && FullBrickEpicDungeon.DungeonCrawler.music)
            GameEnvironment.AssetManager.PlayMusic("Assets/Music/menu");
        base.Initialize();
    }


    /// <summary>
    /// Draws the title menu
    /// </summary>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, Vector2.Zero, Color.White);

        base.Draw(gameTime, spriteBatch);
    }


    /// <summary>
    /// if the button is pressed (which is handled in MenuState) this method executes what happens
    /// </summary>
    protected override void ButtonPressedHandler()
    {
        //check for each button in the buttonlist if it is pressed.
        for (int buttonnr = 0; buttonnr < buttonList.Count; buttonnr++)
        {
            if (buttonList[buttonnr].Pressed)
            {
                GameEnvironment.AssetManager.PlaySound("Assets/SFX/button_click");

                switch (buttonnr)
                {
                    case 0: //Start button pressed
                        GameEnvironment.GameStateManager.SwitchTo("characterSelection");
                        break;
                    case 1: //Settings button pressed
                        GameEnvironment.GameStateManager.SwitchTo("settingsState");
                        break;
                    case 2:
                        GameEnvironment.GameStateManager.SwitchTo("creditsState");
                        break;
                    case 3:
                        FullBrickEpicDungeon.DungeonCrawler.exitGame = true;
                        break;
                    default: throw new IndexOutOfRangeException("Buttonbehaviour not defined. Buttonnumber in buttonList: " + buttonnr);

                }
            }
        }
    }
}