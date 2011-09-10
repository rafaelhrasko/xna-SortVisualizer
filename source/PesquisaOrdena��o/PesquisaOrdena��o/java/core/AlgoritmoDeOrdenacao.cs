/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
public abstract class AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel
{
/*    TrocaListener troc=null;
    
    public void setTrocaListener(TrocaListener troc) {
        this.troc=troc;
    }
*/
    public abstract void ordenar(Conjunto<T> c);
}
