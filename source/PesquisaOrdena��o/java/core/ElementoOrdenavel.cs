/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System;
public abstract class ElementoOrdenavel {

    IContadorComparacoes contador;

    protected abstract bool iMaiorQue(ElementoOrdenavel o);
    protected abstract bool iMenorQue(ElementoOrdenavel o);

    public bool MaiorQue(ElementoOrdenavel o)
    {
        contador.comparacoes++;
        return iMaiorQue(o);
    }
    public bool MenorQue(ElementoOrdenavel o)
    {
        contador.comparacoes++;
        return iMenorQue(o);
    }

    //public void definePivo(String string) {
    //    throw new UnsupportedOperationException("Not yet implemented");
    //}

    public ElementoOrdenavel(IContadorComparacoes c)
    {
        contador = c;
    }

    virtual public String toString()
    {
        return "ElementoOrdenavel indefinido";
    }
}
