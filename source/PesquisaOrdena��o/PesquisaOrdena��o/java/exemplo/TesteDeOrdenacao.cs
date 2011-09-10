/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author fredrischter
 */
public class TesteDeOrdenacao {

    AlgoritmoDeOrdenacao<Randomizavel> alg;
    public Conjunto<Randomizavel> con = new Conjunto<Randomizavel>();

    

    public TesteDeOrdenacao(AlgoritmoDeOrdenacao<Randomizavel> alg, RandomizavelFactory<Randomizavel> bobo, PO.Avisador<Randomizavel> avisador )
    {
        this.alg = alg;

        con.addTrocaListener(avisador);

        bobo.getAllInstance(con);
        
        
        /*
        NumeroRandomFactory nf = new NumeroRandomFactory(100);
        for (int i = 0; i < numeroElementos; i++) {
            con.incluir(nf.getRandomInstance());
        }
         */

    }

    public void disparar(int i) {
        if (!con.iniciou()) {
            con.ordenar(alg, i);
        } else {
            con.removeStepTrocaListener();
            con.removeStopTrocaListener();
        }
    }

    public void addTrocaListener(OrdenacaoListener<Randomizavel> trocaListener) {
        con.addTrocaListener(trocaListener);
    }

    public void parar() {
        con.addStepListener();
        con.addStopListener();
    }

    public void passo() {
        con.removeStopTrocaListener();
        if (!con.possuiStepListener()) {
            con.addStepListener();
        }
        con.step();
    }

    public void finalizar() {
        con.finalizar();
    }

    public bool rodando()
    {
        return (!con.possuiStopListener() && con.iniciou());
    }
}
