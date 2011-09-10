using System.Collections.Generic;

namespace PO
{
    class StoppableSignBoard : Signboard
    {
        public bool release;

        public StoppableSignBoard(List<string> help, int height, Microsoft.Xna.Framework.Graphics.SpriteFont font, int _waitingTime, string _header)
            : base(help, height, font, _waitingTime,_header)
        {
            release = false;
        }

        protected override void Stop()
        {
            if ((--timer <= 0) || (release))
            {
                timer = 0;
                if (release)
                {
                    state = SignState.Exiting;
                }

                else if (waitingQueue.Count < 2)
                {
                    release = true;
                }
            }
        }

        protected override void Exit()
        {
            base.Exit();
            release = false;
        }

        public override void End()
        {
            base.End();
            release = true;
        }

        public void Update(AlgorithmState algState)
        {
            this.Update();
            this.header = "Comparacoes: " + algState.Comparacoes + " - Trocas: " + algState.Trocas;
            
            if ((Options.AlgorithmType == AlgorithmType.Shell)&&(algState.contadores != null))
            {
                int h = 0;
                foreach (Contador<Randomizavel> contadorRand in algState.contadores)
                {
                    if (contadorRand.nome == "h")
                    {
                        h = contadorRand.valor;
                    }
                }
                this.header += " - H = " + h.ToString();
            }
        }
    }
}
