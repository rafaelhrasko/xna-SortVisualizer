
public class ShellSort<T> : AlgoritmoDeOrdenacao<T> where T:ElementoOrdenavel {

	override public void ordenar(Conjunto<T> c) {
		Contador<T> i = new Contador<T>( c, "i", 0);
		Contador<T> j = new Contador<T>( c, "j", 0);
        Contador<T> jMenosH = new Contador<T>(c, "j-h", 0);
        Contador<T> h = new Contador<T>(c, "h", 1);
		T temp;

		do{
            h.setVal(3 * h.getVal() + 1);

        } while (h.getVal() < c.tamanho());
		
		do{
            h.setVal(h.getVal() / 3);

            for (i.setVal(h.getVal()); i.getVal() < c.tamanho(); i.inc()){
				temp = c.get(i.getVal());
				j.setVal(i.getVal());
                jMenosH.setVal(j.getVal() - h.getVal());
				
				while(c.get(jMenosH.getVal()).MaiorQue(temp)){
					c.set(j.getVal(), c.get(jMenosH.getVal()));
                    j.setVal(j.getVal() - h.getVal());
                    jMenosH.setVal(j.getVal() - h.getVal());

                    if (j.getVal() < h.getVal()){
						break;
						
					}
					
				}
				
				c.set(j.getVal(), temp);
				
			}

        } while (h.getVal() != 1);
		
	}

}
