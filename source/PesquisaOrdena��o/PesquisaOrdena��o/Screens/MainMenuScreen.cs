#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace PO
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Fields
        MenuEntry algorithmMenuEntry;
        MenuEntry dataEntryMenuEntry;
        MenuEntry dataSizeMenuEntry;

        Signboard board;


        #endregion
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Main Menu")
        {          
            

            // Create our menu entries.
            MenuEntry playMenuEntry = new MenuEntry("Verificar o algoritmo");
            algorithmMenuEntry = new MenuEntry(string.Empty);
            dataEntryMenuEntry = new MenuEntry(string.Empty);
            dataSizeMenuEntry = new MenuEntry(string.Empty);
            MenuEntry optionsMenuEntry = new MenuEntry("Opcoes Graficas");
            MenuEntry exitMenuEntry = new MenuEntry("Sair");
            
            SetMenuEntryText();

            // Hook up menu event handlers.
            playMenuEntry.Selected += PlayMenuEntrySelected;
            algorithmMenuEntry.Selected += AlgorithmMenuEntrySelected;
            dataEntryMenuEntry.Selected += DataEntryMenuEntrySelected;
            dataSizeMenuEntry.Selected += DataSizeMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playMenuEntry);
            MenuEntries.Add(algorithmMenuEntry);
            MenuEntries.Add(dataEntryMenuEntry);
            MenuEntries.Add(dataSizeMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        public override void LoadContent()
        {
            base.LoadContent();

            Microsoft.Xna.Framework.Content.ContentManager content =
                new Microsoft.Xna.Framework.Content.ContentManager(ScreenManager.Game.Services, "Content");            
 
            board = new Signboard(Options.creditos, Options.ScreenHeight - 50, content.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>("signfont"),60,null);            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            board.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            board.Draw(ScreenManager.SpriteBatch);
        }

        #region Handle Input

        void SetMenuEntryText()
        {
            algorithmMenuEntry.Text = "Algoritmo: " + Options.AlgorithmType;
            dataEntryMenuEntry.Text = "Entrada de dados: " + Options.DataEntryType;
            dataSizeMenuEntry.Text = "Tamanho da entrada: " + Options.DataEntrySizes[Options.DataEntrySize];
        }

        /// <summary>
        /// Event handler for when the Play menu entry is selected.
        /// </summary>
        void PlayMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new AlgorithmScreen());
        }

        /// <summary>
        /// Event Handler paar a escolha do algoritmo
        /// </summary>
        void AlgorithmMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Options.AlgorithmType++;

            if (Options.AlgorithmType > AlgorithmType.Heap)
            {
                Options.AlgorithmType = AlgorithmType.Bolha;
            }

            SetMenuEntryText();
        }

        /// <summary>
        /// Event Handler para a escolha da entrada
        /// </summary>
        void DataEntryMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Options.DataEntryType++;

            if (Options.DataEntryType > DataEntryType.Invertido)
            {
                Options.DataEntryType = DataEntryType.Aleatorio;
            }

            SetMenuEntryText();
        }

        /// <summary>
        /// Event Handler paar a escolha do tamanho da entrada
        /// </summary>
        void DataSizeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Options.DataEntrySize++;

            if (Options.DataEntrySize >= Options.DataEntrySizes.Length)
            {
                Options.DataEntrySize = 0;
            }

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Tem certeza de que gostaria de sair?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
