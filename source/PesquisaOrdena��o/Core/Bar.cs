using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace PO
{
    public enum BarState
    {
        Stopped,
        StartMoving,
        Moving,
        Stopping,
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bar:Randomizavel
    {
        BarState state;
        public Vector2 position;
        Vector2 targetPosition;
        //Vector2 origin;
        //int targetVectorPosition;
        float velocity;
        bool goingRight;
        float offset;

        public Color myColor;

        readonly float acceleration;
        readonly int maxVelocity;

        readonly int maxWidth;
        int innerWidth;

        public Rectangle border;
        public Rectangle inner;

        public readonly int value;

        int height;

        override protected bool iMenorQue(ElementoOrdenavel b)
        {
            return (value < ((Bar)b).value);
        }

        override protected bool iMaiorQue(ElementoOrdenavel b)
        {
            return (value > ((Bar)b).value);
        }

        public Bar(Vector2 position, int maxWidth, int value, IContadorComparacoes c)
            :base(c)
        {
            state = BarState.Stopped;
            this.position = position;
            byte red = (byte)Program.Random.Next(245);
            byte green = (byte)Program.Random.Next(245);
            byte blue = (byte)Program.Random.Next(245);
            myColor = new Color(red, green, blue, Options.unselectedBarAlpha);
            acceleration = Options.acceleration;
            maxVelocity = Options.maxVelocity;
            this.maxWidth = maxWidth;
            innerWidth = maxWidth - 2 * (Options.barBorderWidth);

            height = (int)(((float)value / (float)Options.maxValue) * Options.ScreenHeight * Options.safeBarHeight);
            offset = (float)Math.Round((((Options.maxValue - value) / (float)Options.maxValue) * Options.ScreenHeight * Options.safeBarHeight),0,MidpointRounding.AwayFromZero);

            border = new Rectangle(
                (int)position.X,
                (int)(position.Y + offset), 
                maxWidth,
                height);
            inner = new Rectangle(
                (int)position.X + Options.barBorderWidth,
                (int)(position.Y + Options.barBorderWidth + offset),
                innerWidth,
                height - 2 * Options.barBorderWidth);

            this.value = value;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public  void Initialize()
        {
            // TODO: Add your initialization code here

            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {
            switch (state)
            {
                case BarState.StartMoving: StartMovement(); break;
                case BarState.Moving: Move(); break;
                case BarState.Stopping: StopMovement(); break;
            }

            border = new Rectangle(
            (int)position.X,
            (int)(position.Y + offset),
            maxWidth,
            border.Height);

            inner = new Rectangle(
                (int)position.X + Options.barBorderWidth,
                (int)(position.Y + Options.barBorderWidth + offset),
                innerWidth,
                inner.Height);

        }

        void StartMovement()
        {
            myColor.A = (byte)255;
            state = BarState.Moving;
        }

        void Move()
        {
            if (Vector2.Distance(position, targetPosition) > Options.easingDistance)
            {                
                if (velocity < maxVelocity)
                {
                    velocity += acceleration;
                }
            }
            else
            {
                if (velocity > acceleration)
                {
                    velocity -= acceleration;
                }
                else
                {
                    velocity = acceleration;
                }
            }

            if (goingRight)
            {
                if (position.X < targetPosition.X)
                {
                    position.X += velocity;
                }
                else
                {
                    state = BarState.Stopping;
                }

            }
            else
            {
                if (targetPosition.X < position.X)
                {
                    position.X -= velocity;
                }
                else
                {
                    state = BarState.Stopping;
                }
            }
        }

        void StopMovement()
        {
            position = targetPosition;
            targetPosition = Vector2.Zero;
            myColor.A = Options.unselectedBarAlpha;
            velocity = 0;
            state = BarState.Stopped;
        }

        public void MoveToPosition(Vector2 target)
        {
            targetPosition = target;
            if (targetPosition.X > position.X)
            {
                goingRight = true;
            }
            else
            {
                goingRight = false;
            }

            state = BarState.StartMoving;
        }

        public bool isStopped
        {
            get { return state == BarState.Stopped; }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, border, Color.White);
            spriteBatch.Draw(texture, inner, myColor);
            //spriteBatch.Draw(texture, border, null, Color.White, 0, origin, SpriteEffects.None, 0);
        }

    }
}