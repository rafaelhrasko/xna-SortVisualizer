
using System;
public class InsertionSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel
{

	override public void ordenar(Conjunto<T> c) {
		Contador<T>i = new Contador<T>(c, "i", 0);
		Contador<T>j = new Contador<T>(c, "j", 0);
		T temp;
				
		for (i.setVal(1); i.getVal() < c.tamanho(); i.inc() ){
			temp = c.get(i.getVal());
            c.incluirDestaque(temp, "pivo");
			j.setVal(i.getVal() - 1);
			
			while (j.getVal() >= 0 && temp.MenorQue(c.get(j.getVal()))){
				c.set(j.getVal()+1, c.get(j.getVal()));
				j.dec();				
			}
			c.set(j.getVal()+1, temp);
            c.excluirDestaque("pivo");
		}
		
	}
	
	public String toString() {
		return "InsertionSort";
	}

}
