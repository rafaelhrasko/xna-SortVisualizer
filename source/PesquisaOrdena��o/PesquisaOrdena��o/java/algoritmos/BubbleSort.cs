
public class BubbleSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel
{
	override public void ordenar(Conjunto<T> c) {
		Contador<T> i = new Contador<T>( c, "i", 0);
		Contador<T> j = new Contador<T>( c, "j", 0);
		Contador<T> Lsup = new Contador<T>( c, "Lsup", 0);
		//T temp;

		Lsup.setVal(c.tamanho()-1);

		do{
			j.setVal(0);

			for(i.setVal(0); i.getVal() < Lsup.getVal(); i.inc()){
				if (c.get(i.getVal()).MaiorQue(c.get(i.getVal()+1))){
                    c.troca(i.getVal(), i.getVal() + 1);
					j.setVal(i.getVal());
					
				}
			}
			
			Lsup.setVal(j.getVal());

		}while(Lsup.getVal() >= 1);

	}

}
