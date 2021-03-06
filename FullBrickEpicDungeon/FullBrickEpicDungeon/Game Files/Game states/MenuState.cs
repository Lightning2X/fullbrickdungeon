﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

abstract class MenuState : IGameLoopObject
    {

    protected List<Button> buttonList = new List<Button>();

    protected SpriteGameObject marker;
    protected Vector2 offsetMarker;
    protected int buttonSeparation = 250;
    protected int buttonIndex = 0;

    //toetsenbord controls dictionary van player 0 en 1 in de dictionary hiervoor
    protected Dictionary<Keys, Keys> keyboardControls1;
    protected Dictionary<Keys, Keys> keyboardControls2;

    //matches the player number to the controls dictionary used.
    protected Dictionary<int, Dictionary<Keys, Keys>> keyboardcontrols = new Dictionary<int, Dictionary<Keys, Keys>>();




    /// <summary>
    /// Class that defines a Pause state
    /// </summary>
    public MenuState()
    {        
        marker = new SpriteGameObject("Assets/Sprites/Conversation Boxes/arrow", 1, "", 10, false);

        keyboardControls1 = GameEnvironment.SettingsHelper.GenerateKeyboardControls("Assets/Controls/player1controls.txt");
        keyboardControls2 = GameEnvironment.SettingsHelper.GenerateKeyboardControls("Assets/Controls/player2controls.txt");
        keyboardcontrols.Add(0, keyboardControls1);
        keyboardcontrols.Add(1, keyboardControls2);

        FillButtonList();


    }


    /// <summary>
    /// Gives positions to buttons and marker
    /// </summary>
    protected virtual void FillButtonList()
    {

        //set button positions
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].Position = new Vector2(GameEnvironment.Screen.X / 2 - buttonList[0].Width / 2, 250 + i * buttonSeparation);
        }

        offsetMarker = new Vector2(-marker.Width, buttonList[0].Height / 2 - marker.Height / 2);
        marker.Position = new Vector2(buttonList[0].Position.X + offsetMarker.X, buttonList[0].Position.Y + offsetMarker.Y);

    }

    public virtual void Update(GameTime gameTime)
    {
        
    }

    /// <summary>
    /// Draws all the buttons in a list
    /// </summary>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // draw the buttons
        foreach (Button button in buttonList)
        {
            button.Draw(gameTime, spriteBatch);
        }

        // draw the marker
        marker.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Handles input for the Menu Gamestates
    /// </summary>
    public virtual void HandleInput(InputHelper inputHelper)
    {
        foreach (Button button in buttonList)
        {
            button.HandleInput(inputHelper);
        }

        HandleMouseInput(inputHelper);
        HandleKeyboardInput(inputHelper);

        //each connected controller can control the menu
        for (int controllernumber = 1; controllernumber <= 4; controllernumber++)
        {
            if (inputHelper.ControllerConnected(controllernumber))
            {
                HandleXboxInput(inputHelper, controllernumber);
            }
        }

    }


    /// <summary>
    /// Handles XboxInput for Menu Gamestates
    /// </summary>
    protected virtual void HandleXboxInput(InputHelper inputHelper, int controllernumber)
    {
        //moves the marker up or down depending on the key that was pressed, the Dpad or the Thumbstick.
        if (inputHelper.ButtonPressed(controllernumber, Buttons.DPadDown) || inputHelper.MenuDirection(controllernumber, false, true).Y < 0)
        {
            if (buttonIndex < buttonList.Count - 1)
            {
                buttonIndex++;
                marker.Position = new Vector2(buttonList[buttonIndex].Position.X + offsetMarker.X, buttonList[buttonIndex].Position.Y + offsetMarker.Y);
            }
        }
        else if (inputHelper.ButtonPressed(controllernumber, Buttons.DPadUp) || inputHelper.MenuDirection(controllernumber, false, true).Y > 0)
        {
            if (buttonIndex > 0)
            {
                buttonIndex--;
                marker.Position = new Vector2(buttonList[buttonIndex].Position.X + offsetMarker.X, buttonList[buttonIndex].Position.Y + offsetMarker.Y);
            }
        }
        if (inputHelper.ButtonPressed(controllernumber, Buttons.A))
        {
            PressButton();
        }
    }

    protected virtual void HandleKeyboardInput(InputHelper inputHelper)
    {
        //moves the marker up or down depending on the key that was pressed, the Dpad or the Thumbstick.
        if (inputHelper.KeyPressed(Keys.Down) || inputHelper.KeyPressed(Keys.S))
        {
            if (buttonIndex < buttonList.Count - 1)
            {
                buttonIndex++;
                marker.Position = new Vector2(buttonList[buttonIndex].Position.X + offsetMarker.X, buttonList[buttonIndex].Position.Y + offsetMarker.Y);
            }

        }
        else if (inputHelper.KeyPressed(Keys.Up) || inputHelper.KeyPressed(Keys.W))
        {
            if (buttonIndex > 0)
            {
                buttonIndex--;
                marker.Position = new Vector2(buttonList[buttonIndex].Position.X + offsetMarker.X, buttonList[buttonIndex].Position.Y + offsetMarker.Y);
            }
        }
        if (inputHelper.KeyPressed(Keys.Space) || inputHelper.KeyPressed(Keys.Enter))
        {
            PressButton();
        }
    }


    protected virtual void HandleMouseInput(InputHelper inputHelper)
    {
        ButtonPressedHandler();
    }


    /// <summary>
    /// Sets a button.Pressed is true if the button has been selected by the marker and A (Xbox), Space or Enter is pressed.
    /// </summary>
    protected virtual void PressButton()
    {
        for (int index = 0; index < buttonList.Count; index++)
        {
            if (marker.Position == buttonList[index].Position + offsetMarker)
            {
                buttonList[index].Pressed = true;
                ButtonPressedHandler();
            }
        }
    }

    /// <summary>
    /// sets button excecution. differs per menu
    /// </summary>
    protected virtual void ButtonPressedHandler()
    {
       //sets button excecution. differs per menu
    }

    public virtual void Initialize()
    {
            FullBrickEpicDungeon.DungeonCrawler.mouseVisible = true;
    }

    public virtual void Reset()
    {
    }

}




