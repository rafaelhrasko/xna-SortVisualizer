
/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


/**
 *
 * @author fredrischter
 */

using System;
public class Contador<T> where T : ElementoOrdenavel
{

    public Contador<T> contador_abaixo=null;
    Conjunto<T> conj;
    public int valor;
    public String nome;
    /**
     * Este objeto representa um contador que "andara" em um conjunto.
     * @param conj O conjunto onde este contador se aplica.
     * @param nom Nome do contador
     * @param i Valor inicial
     */
    public Contador(Conjunto<T> conj, String nom, int i)
    {
        this.conj=conj;
        nome=nom;
        valor=i;
        conj.incluirContador(this);
    }

    public int getVal() {
        return valor;
    }

    public int dec() {
        setVal(valor-1);
        return valor;
    }

    public int inc() {
        setVal(valor+1);
        return valor;
    }

    public void setVal(int i) {
        valor=i;
        conj.notificarAlteracaoContador(this);
    }

    public String toString() {
        String adc="";

        if (contador_abaixo != null)
            adc = contador_abaixo.toString();

        return "["+nome+" = "+valor+""+adc+"]";
    }

    public void terminar() {
        conj.excluirContador(this);
    }

    public Contador<T> abaixo()
    {
        return contador_abaixo;
    }

}
