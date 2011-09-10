#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace PO
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry speedMenuEntry;
        MenuEntry screenResolutionMenuEntry;
        MenuEntry fullScreenMenuEntry;
        MenuEntry stepSpeedMenuEntry;

        enum Speeds
        {
            Lentissimo,
            Lento,
            Normal,
            Rapido,
            Impressionante,
            Teleporte
        }

        static Speeds currentSpeed = Speeds.Lento;

        static int[] screenWidth = { 1024, 1280, 1280, 1280, 1280 };
        static int[] screenHeight = { 768, 600, 720, 768, 800 };

        static int currentResolution = 0;

        static int[] algorithmSpeed = { 1, 10, 100, 500, 1000, 3000 };

        static int currentAlgorithmSpeed = 0;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            speedMenuEntry = new MenuEntry(string.Empty);
            screenResolutionMenuEntry = new MenuEntry(string.Empty);
            fullScreenMenuEntry = new MenuEntry(string.Empty);
            stepSpeedMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry saveMenuEntry = new MenuEntry("Salvar e voltar");
            MenuEntry exitMenuEntry = new MenuEntry("Voltar sem salvar");

            // Hook up menu event handlers.
            speedMenuEntry.Selected += SpeedMenuEntrySelected;
            screenResolutionMenuEntry.Selected += LanguageMenuEntrySelected;
            fullScreenMenuEntry.Selected += FullScreenMenuEntrySelected;
            stepSpeedMenuEntry.Selected += StepSpeedMenuEntrySelected;
            saveMenuEntry.Selected += SaveAndExit;
            exitMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(speedMenuEntry);
            MenuEntries.Add(screenResolutionMenuEntry);
            MenuEntries.Add(fullScreenMenuEntry);
            MenuEntries.Add(stepSpeedMenuEntry);
            MenuEntries.Add(saveMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            speedMenuEntry.Text = "Velocidade das Animacoes: " + currentSpeed;
            screenResolutionMenuEntry.Text = "Resolution: " + screenWidth[currentResolution] + " x " + screenHeight[currentResolution];
            fullScreenMenuEntry.Text = "FullScreen: " + (Options.FullScreen ? "Sim" : "Nao");
            stepSpeedMenuEntry.Text = "Velocidade de cada passo: " + algorithmSpeed[currentAlgorithmSpeed] + "ms";
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void SpeedMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentSpeed++;

            if (currentSpeed > Speeds.Teleporte)
                currentSpeed = 0;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentResolution = (currentResolution + 1) % screenHeight.Length;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FullScreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Options.FullScreen = !Options.FullScreen;

            SetMenuEntryText();
        }

        void StepSpeedMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentAlgorithmSpeed = (currentAlgorithmSpeed + 1) % algorithmSpeed.Length;

            Options.intervalo = algorithmSpeed[currentAlgorithmSpeed];

            SetMenuEntryText();
        }

        void SaveAndExit(object sender, PlayerIndexEventArgs e)
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)
                (ScreenManager.Game.Services.GetService(typeof(GraphicsDeviceManager)));
            if (Options.FullScreen)
            {
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = Options.ScreenWidth;
                graphics.PreferredBackBufferHeight = Options.ScreenHeight;
            }

            graphics.ApplyChanges();

            switchSpeeds();

            OnCancel(e.PlayerIndex);

        }

        void switchSpeeds()
        {
            switch (currentSpeed)
            {
                case Speeds.Lentissimo:
                    {
                        Options.acceleration = 0.1f;
                        Options.maxVelocity = 6;
                        Options.easingDistance = 1;
                        break;
                    }
                case Speeds.Lento:
                    {
                        Options.acceleration = 0.5f;
                        Options.maxVelocity = 30;
                        Options.easingDistance = 5;
                        break;
                    }
                case Speeds.Normal:
                    {
                        Options.acceleration = 1f;
                        Options.maxVelocity = 30;
                        Options.easingDistance = 10;
                        break;
                    }
                case Speeds.Rapido:
                    {
                        Options.acceleration = 2f;
                        Options.maxVelocity = 40;
                        Options.easingDistance = 15;
                        break;
                    }
                case Speeds.Impressionante:
                    {
                        Options.acceleration = 2.5f;
                        Options.maxVelocity = 60;
                        Options.easingDistance = 60;
                        break;
                    }
                case Speeds.Teleporte:
                    {
                        Options.acceleration = Options.ScreenWidth;
                        Options.maxVelocity = Options.ScreenWidth;
                        Options.easingDistance = 1;
                        break;
                    }
            }
        }

        #endregion
    }
}
