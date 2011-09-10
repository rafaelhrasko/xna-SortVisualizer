/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


/**
 *
 * @author fredrischter
 */
using System;
public class QuickSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel {

    Conjunto<T> c;

    override public void ordenar(Conjunto<T> c)
    {
        this.c=c;
        ordenar(0,c.tamanho()-1);
    }

    public void ordenar(int esq,int dir) {

        Contador<T>i=new Contador<T>(c,"i",esq);
        Contador<T>j=new Contador<T>(c,"j",dir);
        
        T pivo;

        pivo=c.get((i.getVal()+j.getVal())/2);
        c.incluirDestaque(pivo,"pivo");

        do {
            while (c.get(i).MenorQue(pivo)) i.inc();
            while (c.get(j).MaiorQue(pivo)) j.dec();

            if (i.getVal()<=j.getVal()) {
                c.troca(i, j);
                i.inc();
                j.dec();
            }
        } while (i.getVal()<=j.getVal());

        if (esq<j.getVal()) ordenar(esq,j.getVal());
        if (dir>i.getVal()) ordenar(i.getVal(),dir);

        i.terminar();
        j.terminar();
        c.excluirDestaque("pivo");
    }

    public String toString() {
        return "QuickSort";
    }

}
