/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System.Collections.Generic;
using System.Threading;
using System;
public class DelayOrdenacaoListener<T> : OrdenacaoListener<T> where T:ElementoOrdenavel {

    int tempo_delay;
    public DelayOrdenacaoListener(int delay) {
        this.tempo_delay = delay;
    }


    override public void receberConjuntoInicial(Conjunto<T> c)
    {
        delay(tempo_delay);
    }


    override public void receberConjuntoFinal(Conjunto<T> c)
    {
        delay(tempo_delay);
    }


    override public void receberTrocaPara(Conjunto<T> c, Troca<T> troc)
    {
        delay(tempo_delay);
    }


    override public void receberDefinicao(Conjunto<T> c, Definicao<T> definicao)
    {
        delay(tempo_delay);
    }


    override public void receberAlteracaoContador(Contador<T> cont)
    {
        delay(tempo_delay);
    }


    override public void receberContadores(List<Contador<T>> contadores)
    {
        delay(tempo_delay);
    }


    override public void receberDestaques(List<Destaque> pivos)
    {
        delay(tempo_delay);
    }

    private void delay(int delay) {
        try {
            Thread.Sleep(delay);
        } catch (Exception ex) {
            System.Console.Write(ex.StackTrace);
        }
    }

}
