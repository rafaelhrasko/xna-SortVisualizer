/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
public abstract class RandomizavelFactory<T> where T : Randomizavel {
    public abstract void getAllInstance(Conjunto<T> conjunto);
}
