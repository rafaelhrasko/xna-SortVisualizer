

public class SelectionSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel
{

	override public void ordenar(Conjunto<T> c) {
		Contador<T> i = new Contador<T>(c, "i", 0);
		Contador<T> j = new Contador<T>(c, "j", 0);
		Contador<T> min = new Contador<T>(c, "min", 0);		

		for (i.setVal(0); i.getVal() < c.tamanho()-1; i.inc()){
			min.setVal(i.getVal());

			for (j.setVal(i.getVal()+1); j.getVal() < c.tamanho(); j.inc()){

				if (c.get(j.getVal()).MenorQue(c.get(min.getVal()))){
					min.setVal(j.getVal());

				}
			}

            c.troca(min.getVal(),i.getVal());

		}

	}
}
