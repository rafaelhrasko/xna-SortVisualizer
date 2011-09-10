/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System;
public class Numero : Randomizavel {

    int valor;
    public Numero(int valor, IContadorComparacoes c)
        :base(c)
    {
        this.valor=valor;
    }

    override protected bool iMaiorQue(ElementoOrdenavel o) {
        return valor>((Numero)o).valor;
    }

    override protected bool iMenorQue(ElementoOrdenavel o)
    {
        return valor<((Numero)o).valor;
    }

    override public String toString() {
        return ""+valor;
    }

}
