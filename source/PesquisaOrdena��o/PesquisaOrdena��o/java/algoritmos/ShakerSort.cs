
public class ShakerSort<T> : AlgoritmoDeOrdenacao<T> where T : ElementoOrdenavel
{

	override public void ordenar(Conjunto<T> c) {
		Contador<T> esq = new Contador<T>(c, "esq", 1);
		Contador<T> dir = new Contador<T>(c, "dir", c.tamanho() - 1);
		Contador<T> i = new Contador<T>(c, "i", 0);
		Contador<T> j = new Contador<T>(c, "j", dir.getVal());
		T temp;

		do{
			for(i.setVal(dir.getVal()); i.getVal() >= esq.getVal(); i.dec()){
				if (c.get(i.getVal() - 1).MaiorQue(c.get(i.getVal()))){
					temp = c.get(i.getVal());
					c.set(i.getVal(), c.get(i.getVal() - 1));
					c.set(i.getVal() - 1, temp);
					j.setVal(i.getVal());

				}
			}
			
			esq.setVal(j.getVal() + 1);
			
			for(i.setVal(esq.getVal()); i.getVal() <= dir.getVal(); i.inc()){
				if(c.get(i.getVal() - 1).MaiorQue(c.get(i.getVal()))){
					temp = c.get(i.getVal());
					c.set(i.getVal(), c.get(i.getVal() - 1));
					c.set(i.getVal() - 1, temp);
					j.setVal(i.getVal());
					
				}
			}

			dir.setVal(j.getVal() - 1);
			
		}while(esq.getVal() <= dir.getVal());

	}

}
