/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System;
public class Troca<T> where T : ElementoOrdenavel
{

    public int origem, destino;
    public ElementoOrdenavel valOrigem, valDestino;
    public String foto;

    public Troca(Conjunto<T> c, int origem, int destino)
    {
        this.origem=origem;
        this.destino=destino;
        this.valOrigem=c.get(origem);
        this.valDestino=c.get(destino);
        foto=c.toString();
    }

    public String toString() {
        return "[Troca "+origem+","+destino+", "+valOrigem+" por "+valDestino+"]";
    }
}

