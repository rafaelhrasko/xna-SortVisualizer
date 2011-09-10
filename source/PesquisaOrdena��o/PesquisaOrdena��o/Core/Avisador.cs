using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PO
{
    public class Avisador<T> : OrdenacaoListener<T> where T : ElementoOrdenavel
    {
        Signboard board;

        public Avisador(Signboard _board)
        {
            board = _board;
        }

        public override void receberConjuntoInicial(Conjunto<T> c)
        {
            
        }

        public override void receberConjuntoFinal(Conjunto<T> c)
        {
            board.End();
        }

        public override void receberTrocaPara(Conjunto<T> c, Troca<T> troc)
        {            
            board.Enqueue("Trocou " + troc.origem + " com " + troc.destino);
        }

        public override void receberDefinicao(Conjunto<T> c, Definicao<T> definicao)
        {
            board.Enqueue("Definiu " + definicao.posicao + " com o valor " + definicao.valDef);            
        }

        public override void receberDestaques(List<Destaque> pivos)
        {
            if (pivos.Count > 0)
            {
                board.Enqueue("Pivo é " + pivos[0]);
            }
        }

        public override void receberAlteracaoContador(Contador<T> cont)
        {
            board.Enqueue("Contador " + cont.nome + " vai para " + cont.valor);
        }

        public override void receberContadores(List<Contador<T>> contadores)
        {
            
        }
    }
}
