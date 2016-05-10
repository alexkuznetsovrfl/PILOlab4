using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab444
{
    public class Stak : Stack<Element>
    {
        
    }

    public class Tree
    {

        public Element korenb;   
        public Tree Lvpot = null;
        public Tree Prpot = null;
        // вставка
        public void Insert(Element value) //добавляет элемент в свободного потомка
        {
            if (this.korenb == null)
                this.korenb = value;
        }

        public void Del(Tree z) { z.korenb = null; z.Prpot = null; z.Lvpot = null; }   
        
       
        // проверка пустоты
        public bool IsEmpty()
        {
            if (this.korenb == null)
                return true;
            else
                return false;
        }


    }
    public class Element 
    {
        public void Init(string y, int i) { znach = y; typ = i; }
        public int typ;
        public string znach;
    }

   

}
