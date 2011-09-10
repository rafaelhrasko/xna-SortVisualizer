/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System;
public class Destaque {
    public String nome;
    public ElementoOrdenavel el;
    public Destaque abaixo;
    
    public Destaque(ElementoOrdenavel el,String nome) {
        this.nome=nome;
        this.el=el;
    }

    public String toString() {
        String str="";
        if (abaixo!=null)
            str=" "+abaixo.toString();
        return "["+nome+" : "+el.toString()+str+"]";
    }
}
