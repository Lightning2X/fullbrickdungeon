﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FullBrickEpicDungeon
{
    /// <summary>
    /// Main Game file of our game
    /// </summary>

    public class DungeonCrawler : GameEnvironment
    {

        public static bool SFX = true, music = true, mouseVisible = true, exitGame = false;
        //SpriteGameObject conversationFrame;
        public DungeonCrawler()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the screen and applies the correct resolution and adds all gamestates to the gamestate manager.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // set the appropiate graphics settings
            screen = new Point(1900, 1050);
            windowSize = new Point(1920, 1080);
            FullScreen = false;
            ApplyResolutionSettings();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the gamestates
            GameStateManager.AddGameState("titleMenu", new TitleMenuState());
            GameStateManager.AddGameState("settingsState", new SettingsState());
            GameStateManager.AddGameState("characterSelection", new CharacterSelection());
            GameStateManager.AddGameState("playingState", new PlayingState());
            GameStateManager.AddGameState("pauseState", new PauseState());
            GameStateManager.AddGameState("creditsState", new CreditsState());
            GameStateManager.AddGameState("levelFinishedState", new LevelFinishedState());
            GameStateManager.AddGameState("conversation", new ConversationState());
            GameStateManager.AddGameState("cutscene", new CutsceneState());
            gameStateManager.AddGameState("gameOverState", new GameOverState());
            GameStateManager.AddGameState("controlsInfoState", new ControlsInfoState());
            GameStateManager.SwitchTo("titleMenu");
        }

        /// <summary>
        /// Load the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Unloads Content
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update for the DungeonCrawler game, checks for mouse visiblity and has the ability to exit the game if the ESC key is pressed
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // If the global mousevisibility variable changed, update it accordingly.
            if (mouseVisible)
                IsMouseVisible = true;
            else
                IsMouseVisible = false;

            // If the exitgame boolean changes to true we immediately exit the game
            if (exitGame)
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        
    }
}
