/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System;
public class BOBOSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel{

    Random r = new Random(DateTime.UtcNow.Millisecond);
    public BOBOSort() {
    }

    public override void ordenar(Conjunto<T> c) {
        while (!c.ordenado()) {
            int a=r.Next(c.tamanho());
            int b=r.Next(c.tamanho());
            c.troca(a,b);
        }
   }

    public String toString() {
        return "BOBOSort";
    }

}
