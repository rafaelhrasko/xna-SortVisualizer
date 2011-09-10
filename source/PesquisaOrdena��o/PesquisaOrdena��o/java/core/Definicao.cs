/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


/**
 *
 * @author fredrischter
 */
using System;
public class Definicao<T> where T : ElementoOrdenavel
{

    public int posicao;
    public ElementoOrdenavel valDef;
    public String foto;
    public Definicao(Conjunto<T> conj,int posicao, ElementoOrdenavel incluido) {
        this.posicao=posicao;
        this.valDef=incluido;
        foto=conj.toString();
    }

    public String toString() {
        return "[Definicao de "+valDef+" na posicao "+posicao+"]";
    }
}
