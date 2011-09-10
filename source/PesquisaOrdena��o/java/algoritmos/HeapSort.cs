/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 * Esta classe implementa o algoritmo HeapSort
 * @author fredrischter
 */
using System;
public class HeapSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel {

    // Executa a ordenação
    override public void ordenar(Conjunto<T> c)
    {
        // Cria um Contador<T> para monitorar o valor
        Contador<T> dir = new Contador<T>(c,"dir",c.tamanho() - 1);

        // outro
        Contador<T> esq = new Contador<T>(c,"esq",(dir.getVal() - 1) / 2);

        while (esq.getVal() >= 0) {
            refazHeap(esq.getVal(), c.tamanho()-1, c);
            esq.dec();
        }

        while (dir.getVal() > 0) {
            c.troca(0, dir.getVal());
            dir.dec();
            refazHeap(0, dir.getVal(), c);
        }
    }

    private void refazHeap(int esq, int dir, Conjunto<T> c) {
        Contador<T> i = new Contador<T>(c,"i",esq);
        Contador<T> maiorFolha = new Contador<T>(c,"maiorFolha",2 * i.getVal() + 1);

        // Pega um elemento do conjunto.
        // ElementoOrdenavel é um elemento qualquer.
        T raiz = c.get(i.getVal());
        
        bool heap = false;
        while ((maiorFolha.getVal() <= dir) && (!heap)) {
            if (maiorFolha.getVal() < dir) {
                if (c.get(maiorFolha.getVal()).MenorQue(
                        c.get(maiorFolha.getVal() + 1))) {
                    maiorFolha.inc();
                }
            }
            if (raiz.MenorQue(c.get(maiorFolha.getVal()))) {
                c.set(i.getVal(), c.get(maiorFolha.getVal()));
                i.setVal(maiorFolha.getVal());
                maiorFolha.setVal(2 * i.getVal() + 1);
            } else {
                heap = true;
            }
        }
        c.set(i.getVal(), raiz);
    }

    public String toString() {
        return "HeapSort";
    }
}



