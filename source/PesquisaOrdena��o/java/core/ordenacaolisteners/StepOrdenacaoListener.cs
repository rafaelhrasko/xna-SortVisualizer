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
public class StepOrdenacaoListener<T> : OrdenacaoListener<T> where T:ElementoOrdenavel {

    bool esperando=true;
    bool morrer=false;

    
    override public void receberConjuntoInicial(Conjunto<T> c) {
        esperar();
    }


    override public void receberConjuntoFinal(Conjunto<T> c)
    {
        esperar();
    }


    override public void receberTrocaPara(Conjunto<T> c, Troca<T> troc)
    {
        esperar();
    }


    override public void receberDefinicao(Conjunto<T> c, Definicao<T> definicao)
    {
        esperar();
    }


    override public void receberAlteracaoContador(Contador<T> cont)
    {
        esperar();
    }
    
    override public void receberContadores(List<Contador<T>> contadores)
    {
        esperar();
    }


    override public void receberDestaques(List<Destaque> pivos)
    {
        esperar();
    }

    public void esperar() {
        while(esperando) {
            try {
                if (morrer)
                    return;
                Thread.Sleep(10);
            } catch (Exception ex) {
                System.Console.Write(ex.StackTrace);
            }
        }
        esperando=true;
    }

    public void step() {
        esperando=false;
    }

    public void breake() {
        morrer=true;
        esperando=false;
    }

}
