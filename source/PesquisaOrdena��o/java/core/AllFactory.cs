using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PO
{
    class AllFactory: RandomizavelFactory<Randomizavel>
    {
        AlgorithmState algState;
        int entries;

        public AllFactory(AlgorithmState algState, int entries)
        {
            this.algState = algState;
            this.entries = entries;
        }

        public override void getAllInstance(Conjunto<Randomizavel> conjunto)
        {
            //Estima o tamanho das barras
            algState.barSize = (Options.ScreenWidth) / entries;
            algState.tempPosition = new Vector2(Options.ScreenWidth - algState.barSize, Options.barPositionY);
            //Recalcula o tamanho das barras, reservando espaço para o temporario
            algState.barSize = (Options.ScreenWidth - (int)(1.1f*algState.barSize)) / entries;

            Bar[] bars = new Bar[entries];
            algState.positions = new Vector2[entries];

            for (int i = 0; i < entries; i++)
            {
                algState.positions[i] = new Vector2(i * algState.barSize, Options.barPositionY);
            }


            switch (Options.DataEntryType)
            {
                case DataEntryType.Aleatorio:
                    {
                        for (int i = 0; i < entries; i++)
                        {
                            bars[i] = new Bar(algState.positions[i], algState.barSize, Program.Random.Next(Options.maxValue), conjunto);
                        }
                        break;
                    }
                case DataEntryType.Ordenado:
                    {
                        int increment = (int)(Options.maxValue * (1.0f / (float)entries));
                        bars[0] = new Bar(
                                algState.positions[0],
                                algState.barSize,
                                increment,
                                conjunto);
                        for (int i = 1; i < entries; i++)
                        {
                            bars[i] = new Bar(
                                algState.positions[i],
                                algState.barSize,
                                increment + bars[i - 1].value,
                                conjunto);
                        }
                        break;
                    }
                case DataEntryType.Invertido:
                    {
                        int increment = (int)(Options.maxValue * (1f / (float)entries));
                        bars[0] = new Bar(
                                algState.positions[0],
                                algState.barSize,
                                Options.maxValue,
                                conjunto);
                        for (int i = 1; i < entries; i++)
                        {
                            bars[i] = new Bar(
                                algState.positions[i],
                                algState.barSize,
                                bars[i - 1].value - increment,
                                conjunto);
                        }
                        break;
                    }

            }

            for (int i = 0; i < bars.Length; i++)
            {
                conjunto.incluir(bars[i]);
            }
        }

    }
}
