using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO
{
    public class Signboard
    {
        protected enum SignState
        {
            Entering,
            Stopped,
            Exiting,
        }

        protected SignState state;
        protected Queue<string> waitingQueue;
        string sign;
        List<string> help;
        int counter;
        int waitingTime;
        protected int timer;
        Vector2 position;
        Vector2 headerPosition;
        SpriteFont font;
        protected string header;

        public Avisador<Randomizavel> avisador;

        public Signboard(List<string> help, int height, SpriteFont font, int _waitingTime, string _header)
        {
            header = _header;
            this.state = SignState.Entering;
            this.position = new Vector2(Options.ScreenWidth, height);
            this.headerPosition = new Vector2(0, position.Y - Options.signFontSize);
            this.help = help;
            counter = 0;
            sign = help[0];
            waitingQueue = new Queue<string>();
            avisador = new Avisador<Randomizavel>(this);
            waitingTime = _waitingTime;
            timer = waitingTime;
            this.font = font;
        }

        int signOffset
        {
            get
            {
                if (sign != null)
                {
                    return sign.Length * Options.signFontSize;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Update()
        {
            switch (state)
            {
                case SignState.Entering: Enter(); break;
                case SignState.Stopped: Stop(); break;
                case SignState.Exiting: Exit(); break;
            }
        }

        void Enter()
        {
            if (position.X > 0)
            {
                position.X -= Options.maxVelocity;
            }
            else
            {
                position.X = 0;
                timer = waitingTime;
                state = SignState.Stopped;
            }
        }

        protected virtual void Stop()
        {
            if (--timer <= 0)
            {
                state = SignState.Exiting;
            }
        }

        

        protected virtual void Exit()
        {
            if (position.X > -signOffset)
            {
                position.X -= Options.maxVelocity;
            }
            else
            {
                if (waitingQueue.Count != 0)
                {
                    sign = waitingQueue.Dequeue();
                    this.position.X = 0;
                }
                else
                {
                    if ((sign != null) && !(help.Contains(sign)))
                    {
                        waitingQueue.Enqueue(sign);

                        if (++counter >= help.Count)
                        {
                            counter = 0;
                        }
                        sign = help[counter];
                    }
                    else
                    {
                        if (++counter >= help.Count)
                        {
                            counter = 0;
                        }
                        sign = help[counter];
                    }
                }
                position.X = Options.ScreenWidth;
                timer = waitingTime;
                state = SignState.Entering;
            }            
        }

        public void Enqueue(string sign)
        {
            waitingQueue.Enqueue(sign);            
        }

        public void Clear()
        {
            waitingQueue.Clear();
        }

        public virtual void End()
        {
            help.Add("Acabou!");
            waitingTime = 120;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (header != null)
            {
                spriteBatch.DrawString(
                font,
                header,
                headerPosition,
                Color.Yellow);
            }

            spriteBatch.DrawString(
                font,
                sign,
                position,
                Color.Yellow);           

            spriteBatch.End();
        }

    }
}
