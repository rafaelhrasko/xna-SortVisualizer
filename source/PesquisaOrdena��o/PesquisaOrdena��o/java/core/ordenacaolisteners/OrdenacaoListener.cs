/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System.Collections.Generic;
public abstract class OrdenacaoListener<T> where T : ElementoOrdenavel
{

    int ultNotificacao=-1;

    

    public abstract void receberConjuntoInicial(Conjunto<T> c);
    public abstract void receberConjuntoFinal(Conjunto<T> c);

    public abstract void receberTrocaPara(Conjunto<T> c,Troca<T> troc);
    public abstract void receberDefinicao(Conjunto<T> c,Definicao<T> definicao);

    public abstract void receberDestaques(List<Destaque> pivos);

    List<Contador<T>> contadores = null;

    public void redefinirListaContadores(List<Contador<T>> contadores)
    {
        this.contadores = contadores;
        receberContadores(contadores);
    }

    public abstract void receberAlteracaoContador(Contador<T> cont);
    public abstract void receberContadores(List<Contador<T>> contadores);

    public void receberAlteracaoContador(Contador<T> cont, int notificacoes)
    {
        if (ultNotificacao==notificacoes)
            return;
        receberAlteracaoContador(cont);
        notificacoes=ultNotificacao;
    }

}
