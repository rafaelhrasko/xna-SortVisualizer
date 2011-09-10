
using System.Collections.Generic;

class OutputOrdenacaoListener<T> : OrdenacaoListener<T> where T : ElementoOrdenavel
{

            override public void receberConjuntoInicial(Conjunto<T> c) {
                Util.escrever("\nConjunto inicial: " + c);
            }


            override public void receberConjuntoFinal(Conjunto<T> c)
            {
                Util.escrever("\nConjunto final: " + c);
            }


            override public void receberTrocaPara(Conjunto<T> c, Troca<T> troc)
            {
                Util.escrever("\nEfetuando troca (" + troc + ") no conjunto : \n  " + troc.foto);
                Util.escrever("Resultado                              : \n  " + c);
            }


            override public void receberAlteracaoContador(Contador<T> cont)
            {
                Util.escrever("\nContador movimentando: " + cont);
            }


            override public void receberContadores(List<Contador<T>> contadores)
            {
                Util.escrever("\nContadores: " + contadores);
            }


            override public void receberDefinicao(Conjunto<T> c, Definicao<T> definicao)
            {
                Util.escrever("\nEfetuando definicao (" + definicao + ") no conjunto \n  : " + definicao.foto);
                Util.escrever("Resultado                                       \n  : " + c);
            }


            override public void receberDestaques(List<Destaque> pivos)
            {
                Util.escrever("\nDefinindo os Pivos :  : " + pivos);
            }

}