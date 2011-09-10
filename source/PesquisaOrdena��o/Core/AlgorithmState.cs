using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace PO
{
    class AlgorithmState : OrdenacaoListener<Randomizavel>
    {

        public TesteDeOrdenacao testeOrdenacao;        
        public Vector2[] positions;
        public Vector2 tempPosition;
        public List<Contador<Randomizavel>> contadores;
        public List<Destaque> destaques;
        public int barSize { get; set; }

        public long Comparacoes
        {
            get
            {
                return testeOrdenacao.con.getComparacoes();
            }
        }

        public long Trocas
        {
            get
            {
                return testeOrdenacao.con.getTrocas();
            }
        }

        Avisador<Randomizavel> avisador;

        public AlgorithmState(Avisador<Randomizavel> _avisador)
        {
            avisador = _avisador;
            criaTeste();
        }

        public Bar[] GetBars()
        {
            return this.getBarArray(testeOrdenacao.con.getElementos());
        }

        private void criaTeste()
        {
            AlgoritmoDeOrdenacao<Randomizavel> algorithm = new BOBOSort<Randomizavel>();

            switch (Options.AlgorithmType)
            {
                case AlgorithmType.Bolha:{algorithm = new BubbleSort<Randomizavel>();break;}
                case AlgorithmType.Insercao: { algorithm = new InsertionSort<Randomizavel>(); break; }
                case AlgorithmType.Selecao: { algorithm = new SelectionSort<Randomizavel>(); break; }
                case AlgorithmType.Shell: { algorithm = new ShellSort<Randomizavel>(); break; }
                case AlgorithmType.Quick: { algorithm = new QuickSort<Randomizavel>(); break; }
                case AlgorithmType.Heap: { algorithm = new HeapSort<Randomizavel>(); break; }
            }

            testeOrdenacao = new TesteDeOrdenacao(algorithm, new AllFactory(this, Options.DataEntrySizes[Options.DataEntrySize]),avisador);
            testeOrdenacao.addTrocaListener(this);
            //testeOrdenacao.disparar(Options.intervalo);
        }

        /*
        public static List<Listener> listeners = new List<Listener>();

        public static void AddListener(Listener item)
        {
            listeners.Add(item);
        }

        public static void RequestSwap(Bar who,int here)
        {
            if (!auxFull)
            {
                aux = who;
                auxFull = true;
            }
            else
            {
                int from = bars.Length;
                for (int i = 0; i < bars.Length; i++)
                {
                    if (bars[i] == who)
                    {
                        from = i;
                        break;
                    }
                }
                if (from != bars.Length)
                {
                    bars[from] = aux;
                    bars[here] = who;
                    auxFull = false;
                }
            }
        }
        
        public static bool isInitialized { get { return ((positions != null) &&(bars != null) && (listeners != null)); } }
        */
        public void Reset()
        {
            

            //listeners.Clear();
            

            
            //listeners = null;

        }


        override public void receberConjuntoInicial(Conjunto<Randomizavel> c)
        {
            /*
            bars = getBarArray(c.getElementos());
            //Util.escrever("\nConjunto inicial: " + c);
             */ 
        }

        private Bar[] getBarArray(Randomizavel[] randomizavel)
        {
            Bar[] bar = new Bar[randomizavel.Length];
            for (int i=0;i<randomizavel.Length;i++)
            {
                bar[i] = (Bar)randomizavel[i];
            }
            return bar;
        }
        
        override public void receberConjuntoFinal(Conjunto<Randomizavel> c)
        {
            /*
            bars = getBarArray(c.getElementos());
            //Util.escrever("\nConjunto final: " + c);
             */ 
        }


        override public void receberTrocaPara(Conjunto<Randomizavel> c, Troca<Randomizavel> troc)
        {
            Bar one = (Bar)troc.valOrigem;
            Bar two = (Bar)troc.valDestino;
            one.MoveToPosition(positions[troc.destino]);
            two.MoveToPosition(positions[troc.origem]);            
            //Util.escrever("\nEfetuando troca (" + troc + ") no conjunto : \n  " + troc.foto);
            //Util.escrever("Resultado                              : \n  " + c);
        }

        override public void receberAlteracaoContador(Contador<Randomizavel> cont)
        {
            //Util.escrever("\nContador movimentando: " + cont);
        }

        override public void receberContadores(List<Contador<Randomizavel>> contadores)
        {
            //Util.escrever("\nContadores: " + contadores);
            this.contadores = contadores;
        }

        override public void receberDefinicao(Conjunto<Randomizavel> c, Definicao<Randomizavel> definicao)
        {
            Bar vai = (Bar)definicao.valDef;
            vai.MoveToPosition(positions[definicao.posicao]);
        }


        override public void receberDestaques(List<Destaque> pivos)
        {
            this.destaques = pivos;
        }


        internal void finalizar()
        {
            testeOrdenacao.finalizar();
        }

        internal void teclaSpace()
        {
            if (!testeOrdenacao.rodando())
            {
                testeOrdenacao.disparar(Options.intervalo);
            }

            testeOrdenacao.passo();

        }

        internal void teclaEnd()
        {
            if (!testeOrdenacao.rodando())
            {
                testeOrdenacao.disparar(Options.intervalo);
            }
            else
            {
                testeOrdenacao.parar();
            }
        }
    }
}
