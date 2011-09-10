/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
using System.Collections.Generic;
using System.Threading;
using System;
public class Conjunto<T>:IContadorComparacoes where T : ElementoOrdenavel
{

    private List<OrdenacaoListener<T>> troc = new List<OrdenacaoListener<T>>();
    List<Contador<T>> contadores = new List<Contador<T>>();
    List<Destaque> pivos = new List<Destaque>();
    //OrdenacaoListener<T> stop,step;

    bool bln_iniciou = false;
    long trocas = 0;
    double duracao = 0;
    private List<T> elementos = new List<T>();
    int notificacoes = 0;

    public Conjunto()
    {
        addTrocaListener(new OutputOrdenacaoListener<T>());

    }

    public void incluir(T ord)
    {
        elementos.Add(ord);
    }

    public int tamanho()
    {
        return elementos.Count;
    }

    public void show()
    {
        Util.escrever(elementos.ToString());
        //System.out.println(ar);
    }

    public void addTrocaListener(OrdenacaoListener<T> troc)
    {
        //high.execute(this,"addTrocaListener",new Object[]{troc});
        this.getTroc().Add(troc);
    }

    Thread tcontrole = null;
    //Runnable rodando = null;

    DateTime inicio;

    public void ordenar(AlgoritmoDeOrdenacao<T> alg, int delay)
    {
        bln_iniciou = true;
        //troc=alg.troc;

        if (delay > 0)
        {
            incluirDelayTrocaListener(delay);
        }

        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> troc = this.getTroc()[i];
            troc.receberConjuntoInicial(this);
        }

        inicio = DateTime.UtcNow;
        trocas = 0;

        this.alg = alg;
        tcontrole = new Thread(new ThreadStart(this.executarPesquisa));
        tcontrole.Start();

    }
    
    AlgoritmoDeOrdenacao<T> alg=null;

    private void executarPesquisa()
    {
        if (alg == null)
        {
            Util.escrever("Algoritmo não foi definido?");
            return;
        }
        alg.ordenar(this);
        duracao = (DateTime.UtcNow - inicio).TotalMilliseconds;
        foreach (Object troc in this.getTroc())
        {
            ((OrdenacaoListener<T>)troc).receberConjuntoFinal(this);
        }
    }

    private void incluirDelayTrocaListener(int delay)
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is DelayOrdenacaoListener<T>)
            {
                return;
            }
        }
        addTrocaListener(new DelayOrdenacaoListener<T>(delay));
    }

    public bool ordenado()
    {
        for (int i = 0; i < tamanho(); i++)
        {
            if (i > 0)
            {
                if (elementos[i - 1].MaiorQue(elementos[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void troca(int a, int b)
    {
        Troca<T> ultimaTroca = new Troca<T>(this, a, b);
        trocas += 3;

        T ta = elementos[a];
        T tb = elementos[b];

        elementos.RemoveAt(a);
        elementos.Insert(a, tb);

        elementos.RemoveAt(b);
        elementos.Insert(b, ta);

        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> troc = this.getTroc()[i];
            troc.receberTrocaPara(this, ultimaTroca);
        }
    }

    public T get(int i)
    {
        return elementos[i];
    }

    public long getIteracoes()
    {
        return trocas;
    }

    public void set(int i, T get)
    {
        Definicao<T> def = new Definicao<T>(this, i, get);

        elementos.RemoveAt(i);
        elementos.Insert(i, get);

        for (int it = 0; it < this.getTroc().Count; it++)
        {
            OrdenacaoListener<T> troc = this.getTroc()[it];
            troc.receberDefinicao(this, def);
        }

        trocas++;

    }

    public long getTrocas()
    {
        return trocas;
    }

    public long getComparacoes()
    {
        return comparacoes;
    }

    public double getDuracao()
    {
        return duracao;
    }


    public String toString()
    {
        try
        {
            return "Conjunto: " + elementos;
        }
        catch (Exception e)
        {
            return "Conjunto: "+e.Message;
        }
    }

    public void incluirContador(Contador<T> cont)
    {
        Contador<T> igual = null;
        for (int i = 0; i < contadores.Count; i++)
        {
            Contador<T> cont2 = contadores[i];
            if (cont2.nome.Equals(cont.nome))
            {
                igual = cont2;
            }
        }
        if (igual != null)
        {
            cont.contador_abaixo = igual;
            contadores.Remove(igual);
        }
        contadores.Add(cont);

        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> troc = this.getTroc()[i];
            troc.redefinirListaContadores(contadores);
        }
    }

    public void excluirContador(Contador<T> cont)
    {
        contadores.Remove(cont);
        if (cont.abaixo() != null)
        {
            contadores.Add(cont.abaixo());
        }
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> troc = this.getTroc()[i];
            troc.redefinirListaContadores(contadores);
        }
    }

    public void notificarAlteracaoContador(Contador<T> cont)
    {
        bool notificacaoSucesso = false;
        do
        {
            try
            {
                for (int i = 0; i < this.getTroc().Count; i++)
                {
                    OrdenacaoListener<T> troc = this.getTroc()[i];
                    troc.receberAlteracaoContador(cont, notificacoes);
                }
                notificacaoSucesso = true;
            }
            catch (Exception e)
            {
                System.Console.Write(e.StackTrace);
                Util.escrever("revendo notificação");
                try
                {
                    Thread.Sleep(20);
                }
                catch (Exception ex)
                {
                    System.Console.Write(ex.StackTrace);
                }
            }
        } while (!notificacaoSucesso);

        notificacoes++;
    }

    public void troca(Contador<T> i, Contador<T> j)
    {
        troca(i.getVal(), j.getVal());
    }

    public T get(Contador<T> i)
    {
        return get(i.getVal());
    }

    public void removeAllTrocaListeners()
    {
        getTroc().Clear();
    }

    public bool iniciou()
    {
        return bln_iniciou;
    }

    public void removeStopTrocaListener()
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is StopOrdenacaoListener<T>)
            {
                StopOrdenacaoListener<T> st = (StopOrdenacaoListener<T>)t;
                st.breake();
                getTroc().Remove(t);
                i = 0;
            }
        }
    }

    public void removeStepTrocaListener()
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is StepOrdenacaoListener<T>)
            {
                StepOrdenacaoListener<T> st = (StepOrdenacaoListener<T>)t;
                st.breake();
                getTroc().Remove(t);
                i = 0;
            }
        }
    }

    public void addStopListener()
    {
        addTrocaListener(new StopOrdenacaoListener<T>());
    }

    public void addStepListener()
    {
        addTrocaListener(new StepOrdenacaoListener<T>());
    }

    public bool possuiStepListener()
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is StepOrdenacaoListener<T>)
            {
                return true;
            }
        }
        return false;
    }

    public bool possuiStopListener()
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is StopOrdenacaoListener<T>)
            {
                return true;
            }
        }
        return false;
    }

    public void step()
    {
        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            if (t is StepOrdenacaoListener<T>)
            {
                StepOrdenacaoListener<T> st = (StepOrdenacaoListener<T>)t;
                st.step();
            }
        }
    }

    public void finalizar()
    {
        if (tcontrole != null)
        {
            tcontrole.Abort();
        }
    }

    public List<OrdenacaoListener<T>> getTroc()
    {
        return troc;
    }

    public void incluirDestaque(ElementoOrdenavel elemento, String st)
    {
        Destaque pivo = new Destaque(elemento, st);

        Destaque igual = null;
        for (int i = 0; i < pivos.Count; i++)
        {
            Destaque cont2 = pivos[i];
            if (cont2.nome.Equals(st))
            {
                igual = cont2;
            }
        }
        if (igual != null)
        {
            pivo.abaixo = igual;
            pivos.Remove(igual);
        }

        pivos.Add(pivo);

        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            t.receberDestaques(pivos);
        }
    }

    public void excluirDestaque(String pivo)
    {
        Destaque este = null;
        for (int i = 0; i < pivos.Count; i++)
            if (pivos[i].nome.Equals(pivo))
                este = pivos[i];

        if (este == null) return;

        pivos.Remove(este);
        if (este.abaixo != null)
        {
            pivos.Add(este.abaixo);
        }

        for (int i = 0; i < this.getTroc().Count; i++)
        {
            OrdenacaoListener<T> t = this.getTroc()[i];
            t.receberDestaques(pivos);
        }
    }

    public T[] getElementos()
    {
        return elementos.ToArray();
    }
}
