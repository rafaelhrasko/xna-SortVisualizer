
#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
#endregion

namespace PO
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class AlgorithmScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont algorithmFont;
        SpriteFont signFont;

        StoppableSignBoard board;

        KeyboardState keyboardState, previousKeyboardState;

        Texture2D gradient, arrow;

        //Thread sortThread;

        bool run;

        AlgorithmState algState;

        Color[] listenersColors = { Color.Red, Color.Blue, Color.Yellow, Color.Green, Color.White };

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlgorithmScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            run = false;
            //switchDataEntry();

            

            //SortAlgorithm sort = new SortAlgorithm();
            /*
             * switch
             */

            //sortThread = new Thread(sort.Algorithm);

            //sortThread.Start();

        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            algorithmFont = content.Load<SpriteFont>("algorithmfont");
            signFont = content.Load<SpriteFont>("signfont");

            board = new StoppableSignBoard(Options.help, Options.signFontSize, signFont, 1200, "Algoritmo Iniciado");

            algState = new AlgorithmState(board.avisador);

            gradient = content.Load<Texture2D>("gradient");
            arrow = content.Load<Texture2D>("arrow");

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        void switchDataEntry()
        {
            /*
            int entries = Options.DataEntrySizes[Options.DataEntrySize];

            int barSize = (Options.ScreenWidth - 100) / entries;

            AlgorithmState.bars = new Bar[entries];
            AlgorithmState.positions = new Vector2[entries];

            for (int i = 0; i < entries; i++)
            {
                AlgorithmState.positions[i] = new Vector2(i * barSize + 1, 50);
            }


            switch (Options.DataEntryType)
            {
                case DataEntryType.Aleatorio:
                    {
                        for (int i = 0; i < entries; i++)
                        {
                            AlgorithmState.bars[i] = new Bar(AlgorithmState.positions[i], barSize, Program.Random.Next(Options.maxValue));
                        }
                        break;
                    }
                case DataEntryType.Ordenado:
                    {
                        int increment = (int)(Options.maxValue * (1.0f / (float)entries));
                        AlgorithmState.bars[0] = new Bar(
                                AlgorithmState.positions[0],
                                barSize,
                                increment);
                        for (int i = 1; i < entries; i++)
                        {
                            AlgorithmState.bars[i] = new Bar(
                                AlgorithmState.positions[i],
                                barSize,
                                increment + AlgorithmState.bars[i - 1].value);
                        }
                        break;
                    }
                case DataEntryType.Invertido:
                    {
                        int increment = (int)(Options.maxValue * (1.0f / (float)entries));
                        AlgorithmState.bars[0] = new Bar(
                                AlgorithmState.positions[0],
                                barSize,
                                Options.maxValue - increment);
                        for (int i = 1; i < entries; i++)
                        {
                            AlgorithmState.bars[i] = new Bar(
                                AlgorithmState.positions[i],
                                barSize,
                                AlgorithmState.bars[i - 1].value - increment);
                        }
                        break;
                    }
            }
             */ 
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
            //sortThread.Abort();
            algState.finalizar();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                board.Update(algState);
                if (run)
                {
                    board.Clear();                    
                }

                foreach (Bar bar in algState.GetBars())
                {
                    bar.Update();
                }
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            previousKeyboardState = keyboardState;
            keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                const string message = "Tem certeza que quer voltar para o menu principal?";

                MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

                confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

                ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            }
            else
            {
                if (previousKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                {

                    algState.teclaSpace();
                    board.release = true;
                }

                if (previousKeyboardState.IsKeyUp(Keys.Enter) && keyboardState.IsKeyDown(Keys.Enter))
                {
                    algState.teclaEnd();
                    run = !run;
                    board.Enqueue("Algoritmo " + Options.AlgorithmType + " rodando!");
                }
            }
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            board.Draw(spriteBatch);

            spriteBatch.Begin();
            
            foreach (Bar bar in algState.GetBars())
            {
                bar.Draw(spriteBatch, gradient);              
            }

            if (algState.contadores != null)
            {
                int i = 0;

                for (int j = 0; j < algState.contadores.Count; j++)
                {
                    if (algState.contadores[j].nome != "h")
                    {
                        Contador<Randomizavel> contadorRand = algState.contadores[j];
                        Color color = listenersColors[i];
                        Contador<Randomizavel> atual = contadorRand;
                        DrawListener(spriteBatch, atual, color);
                        if ((atual.contador_abaixo != null) && (color.A > (byte)120))
                        {
                            atual = atual.contador_abaixo;
                            color.A -= (byte)200;
                            DrawListener(spriteBatch, atual, color);
                        }
                        i++;
                    }
                }
            }

            if (algState.destaques != null)
            {
                foreach (Destaque destaque in algState.destaques)
                {
                    if (destaque.nome == "pivo")
                    {
                        Pivot pivot = new Pivot(((Bar)destaque.el).value);
                        pivot.Draw(spriteBatch, gradient);
                    }
                }
            }
            /*foreach (Listener  listener in AlgorithmState.listeners)
            {
                spriteBatch.Draw(arrow, listener.position, Color.Red);
                Vector2 textPosition = new Vector2(listener.position.X, listener.position.Y + 10);
                spriteBatch.DrawString(algorithmFont, listener.name, textPosition, Color.Red);
            }
            */
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }

        void DrawListener(SpriteBatch spriteBatch, Contador<Randomizavel> contadorRand, Color color)
        {
            if ((contadorRand.valor >= 0) && (contadorRand.valor < algState.positions.Length))
            {
                Vector2 pos = algState.positions[contadorRand.valor];

                pos.X += (float)algState.barSize / 2f;

                pos.Y += (Options.listenerOffset + Options.safeBarHeight) * Options.ScreenHeight;

                spriteBatch.Draw(arrow, pos, color);

                pos.Y += Options.listenerOffset * Options.ScreenHeight;

                spriteBatch.DrawString(algorithmFont, contadorRand.nome, pos, color);
            }
        }


        #endregion
    }
}
