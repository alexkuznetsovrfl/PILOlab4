using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab444
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string s;
        Tree u;
        Stak edx, eax;
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
             edx = new Stak();
             eax = new Stak();
             u = new Tree();
            int n = 0;
            int[] a = new int[100];
            s = textBox1.Text;
            for (int j = 0; j < s.Length; j++)
                if (s[j] == '(' || s[j] == ')' || s[j] == '+' || s[j] == '-' || s[j] == '*' || s[j] == '=')
                {
                    a[n] = j;
                    n++;
                }
            n--;
            string d = "=";
            Element pris = new Element();
            pris.znach=d;
            pris.typ=4;
            u.korenb = pris;
            u.Lvpot = new Tree();
            u.Prpot = new Tree();
            
            string g="";
            Element osn = new Element();
            for (int i = 0; i < a[0]; i++)
                g += s[i];
            osn.Init(g, 1);
            u.Lvpot.Insert(osn);
            rec(a[0] + 1, s.Length-1, s, u.Prpot);
            chek(u);
            //дублируется последний элемент, для оптимизации выталкиваем его
        }

        public int skobki(int i)
        {
            int sleva = 0, sprava = 0, z;
            for (z = 0; z < i; z++)
                if (s[z] == '(' || s[z] == ')')
                    sleva++;
            for (z = i + 1; z < s.Length; z++)
                if (s[z] == '(' || s[z] == ')')
                    sprava++;

            if (sprava % 2 == 0 & sleva % 2 == 0)
                return 1;
            else
                return 0;
        }

        public void rec(int inach, int ikon, string s1, Tree a)
        {
            bool flag = false;
            int i = ikon;
            do
            {
                if (s1[i] == '+' || s1[i] == '-') 
                    if (skobki(i) == 1 & i!=inach)
                    {
                        flag = true;
                        Element f = new Element();
                        f.Init(s1[i].ToString(),2);
                        a.Prpot=new Tree();
                        a.Lvpot = new Tree();
                        a.Insert(f); 
                        rec(inach, i-1,s1,a.Lvpot); 
                        rec(i+1, ikon, s1, a.Prpot) ;
                    
                    
                }
                i--;
            }
            while (i >=inach & flag == false);

            i = ikon;
            if (flag == false)
                do
                {
                    if (s1[i] == '*' & skobki(i) == 1)
                    {
                        flag = true;
                        Element f = new Element();
                        f.Init(s1[i].ToString(), 2);
                        a.Prpot = new Tree();
                        a.Lvpot = new Tree();
                        a.Insert(f); 
                        rec(inach, i - 1, s1, a.Lvpot);
                        rec(i + 1, ikon, s1, a.Prpot);
                       
                    }
                    i--;
                }
                while (i >= inach & flag == false);


            i = ikon;
            if (flag == false)
                do
                {
                    if (s1[i] == '*' || s1[i] == '+' || (s1[i] == '-' & i - inach > 1))
                    {
                        flag = true;
                        Element f = new Element();
                        f.Init(s1[i].ToString(), 2);
                        a.Prpot = new Tree();
                        a.Lvpot = new Tree();
                        a.Insert(f); 
                        if (inach<=i-1)
                            rec(inach, i - 1, s1, a.Lvpot);
                        rec(i + 1, ikon, s1, a.Prpot);
                    } i--;
                }
                while (i >= inach & flag == false);
            i = ikon;
            do
            {
                if (s1[i] == '-')
                {
                    flag = true;
                    Element f = new Element();
                    f.Init(s1[i].ToString(), 3);
                    a.Prpot = new Tree();
                    
                    a.Insert(f);
                    
                    rec(i + 1, ikon, s1, a.Prpot);

                }
                    
                i--;
            }
            while (i >= inach & flag == false);

            i = inach;
            if (flag == false) {
                string g = "";
                    Element osn = new Element();
                
                    for (int j = inach; j <= ikon; j++)
                        if (s1[j] != '(' & s1[j] != ')') 
                        g += s[j];
                    if (g != "")
                    {
                        osn.Init(g, 1);
                        a.Insert(osn);
                    }
                }
                   
            }

        void xcng(Stak a, Stak d) //обмен верхними в стеках
        {
            Stak c = new Stak();
            c.Push(a.First());
            a.Pop();
            c.Push(d.First());
            d.Pop();
            a.Push(c.First());
            c.Pop();
            d.Push(c.First());
            c.Pop();
            
        }
        Element A;

      
        Element j = new Element();
        void chek(Tree e) //заполненение стека
        {
            
           
            if (e.korenb.typ == 2)
            {
                chek(e.Lvpot);
                richTextBox1.Text += "push eax" + "\n";

                chek(e.Prpot);
                richTextBox1.Text += "pop edx" + "\n";

                edx.Push(eax.First());
                eax.Pop();

                richTextBox1.Text += "xcng eax,edx" + "\n";
                op(e.korenb);
            }

            if (e.korenb.typ == 1)
            {
                eax.Push(e.korenb);
                richTextBox1.Text += "move eax," + e.korenb.znach + "\n";
            }
                

            if (e.korenb.typ == 3)
            {

                chek(e.Prpot);
                richTextBox1.Text += "neg eax \n";

                
            }
            if (e.korenb.typ == 4)
            {
                chek(e.Prpot);
                A = eax.First();
                richTextBox1.Text += "move " + e.Lvpot.korenb.znach + ",eax \n";
            }
           
            

        }

       
        void op (Element ach)
        {
        string s1="+",s2="-",s3="*";
        if (ach.znach == s1) {richTextBox1.Text += "add eax,edx \n";eax.First().znach=eax.First().znach+"+"+edx.First().znach;}
        if (ach.znach == s2) {richTextBox1.Text += "sub eax,edx \n";eax.First().znach = eax.First().znach + "-" + edx.First().znach;}
        if (ach.znach == s3) { richTextBox1.Text += "imul eax,edx \n"; eax.First().znach = eax.First().znach + "*" + edx.First().znach; }
        }

        void opop(Element ach)
        {
            string s1 = "+", s2 = "-", s3 = "*";
            if (ach.znach == s1) richTextBox2.Text += "\n add eax,";
            if (ach.znach == s2) richTextBox2.Text += "\n sub eax, ";
            if (ach.znach == s3) richTextBox2.Text += "\n imul eax,";
        }

        void chekop(Tree e) //заполненение стека optimum
        {

            if ( e.Lvpot.korenb.typ == 1 & e.Prpot.korenb.typ == 1) 
            {
                richTextBox2.Text += "move eax," + e.Lvpot.korenb.znach; 
                opop(e.korenb);
                richTextBox2.Text += e.Prpot.korenb.znach; 
            }
            if ( e.Lvpot.korenb.typ == 2 & e.Prpot.korenb.typ == 1)
            {
                chekop(e.Lvpot);
                opop(e.korenb);
                richTextBox2.Text += e.Prpot.korenb.znach;
            }
            if ( e.Lvpot.korenb.typ == 1 & e.Prpot.korenb.typ == 2 & (e.korenb.znach == "*" || e.korenb.znach == "+"))
            {
                chekop(e.Prpot);
                opop(e.korenb);
                richTextBox2.Text += e.Lvpot.korenb.znach;
            }
            if ( e.Lvpot.korenb.typ == 1 & e.Prpot.korenb.typ == 2 & e.korenb.znach == "-") 
            {
                chekop(e.Prpot);
                richTextBox2.Text += "\n move edx," + e.Lvpot.korenb.znach;
                richTextBox2.Text += "\n xchg eax,edx";
                opop(e.korenb);
                richTextBox2.Text += "edx";
            }
             
            if ( e.Lvpot.korenb.typ == 2 & e.Prpot.korenb.typ == 2)
            {
                chekop(e.Prpot);
                richTextBox2.Text += "\n push eax\n";
                chekop(e.Lvpot);
                richTextBox2.Text += "\n pop edx";
                opop(e.korenb);
                richTextBox2.Text += "edx";
            } 
                
            

            if (e.korenb.typ == 1)
            {
                richTextBox2.Text += "move eax," + e.korenb.znach + "\n";
            }


            if (e.korenb.typ == 3)
            {

                chekop(e.Prpot);
                richTextBox2.Text += "neg eax \n";


            }
            if (e.korenb.typ == 4)
            {                
                chekop(e.Prpot);
                richTextBox2.Text += "\n move " + e.Lvpot.korenb.znach + ",eax \n";
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            eax=new Stak();
            edx = new Stak();
            richTextBox2.Clear();
            u = new Tree();
            int n = 0;
            int[] a = new int[100];
            s = textBox1.Text;
            for (int j = 0; j < s.Length; j++)
                if (s[j] == '(' || s[j] == ')' || s[j] == '+' || s[j] == '-' || s[j] == '*' || s[j] == '=')
                {
                    a[n] = j;
                    n++;
                }
            n--;
            string d = "=";
            Element pris = new Element();
            pris.znach = d;
            pris.typ = 4;
            u.korenb = pris;
            u.Lvpot = new Tree();
            u.Prpot = new Tree();

            string g = "";
            Element osn = new Element();
            for (int i = 0; i < a[0]; i++)
                g += s[i];
            osn.Init(g, 1);
            u.Lvpot.Insert(osn);
            rec(a[0] + 1, s.Length - 1, s, u.Prpot);
            
            chekop(u);
        }

    }
}
