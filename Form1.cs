using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_1
{
    public partial class Form1 : Form
    {
        bool isCTRL = false;
        Bitmap bitmap;
        Graphics gr;
        public Form1()
        {
            this.KeyPreview = true;
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            isCTRL = false;
            pictureBox1.Image = GetBitmap();
        }

        MyStorage storage = new MyStorage();
        public class AllObj
        {
            int x, y;

            public AllObj data;
            public AllObj Data
            {
                get { return data; }
                set { this.data = value; }
            }
            public AllObj next;
            public AllObj Next
            {
                get { return next; }
                set { this.next = value; }
            }
            public virtual void print()
            {

            }
            public virtual int GetX()
            {
                return 0;
            }
            public virtual int GetY()
            {
                return 0;
            }
            public virtual int GetR()
            {
                return 0;
            }
            public virtual bool GetSel()
            {
                return false;
            }
            public virtual int check(int _x, int _y)
            { return 0; }
            public virtual void checkclick(int _x, int _y, Graphics gr, bool Isctrl)
            {

            }
            public virtual void drawCircle(Graphics gr, Pen pen1)
            {


            }
            public virtual void drawCircle2(Graphics gr, Pen pen1)
            {

            }
            public virtual void OverSel()
            {

            }
            ~AllObj()
            {
                Console.WriteLine(string.Format("AllObj::~AllObj()"));
            }
        };

        public class Circle : AllObj
        {
            private
                int x, y;
            int rC = 40;

            public bool Selected = false;

            public Circle()
            {
                x = 0;
                y = 0;
                rC = 40;

            }
            public Circle(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
            public override int GetX()
            {
                return this.x;
            }
            public override int GetY()
            {
                return this.y;
            }
            public override int GetR()
            {
                return this.rC;
            }
            public override bool GetSel()
            {
                return this.Selected;
            }
            public override void drawCircle(Graphics gr, Pen pen1)
            {
                this.Selected = false;
                gr.DrawEllipse(pen1, (x - rC), (y - rC), 2 * rC, 2 * rC);

            }
            public override void drawCircle2(Graphics gr, Pen pen1)
            {
                this.Selected = true;
                gr.DrawEllipse(pen1, (x - rC), (y - rC), 2 * rC, 2 * rC);

            }
            public override void checkclick(int _x, int _y, Graphics gr, bool Isctrl)
            {
                if (Isctrl == true)
                {
                    if ((_x <= x + rC) && (_x >= x - rC) && (_y <= y + rC) && (_y >= y - rC))
                    {
                        this.Selected = true;
                        gr.DrawEllipse(new Pen(Color.Red), (x - rC), (y - rC), 2 * rC, 2 * rC);
                    }
                }
                else
                {
                    if ((_x <= x + rC) && (_x >= x - rC) && (_y <= y + rC) && (_y >= y - rC))
                    {
                        this.Selected = true;
                        gr.DrawEllipse(new Pen(Color.Red), (x - rC), (y - rC), 2 * rC, 2 * rC);
                    }
                    else
                    {
                        this.Selected = false;
                        gr.DrawEllipse(new Pen(Color.Black), (x - rC), (y - rC), 2 * rC, 2 * rC);
                    }
                }
            }
            public override int check(int _x, int _y)
            {

                if ((_x <= x + rC) && (_x >= x - rC) && (_y <= y + rC) && (_y >= y - rC))
                {
                    return 1;
                }
                else
                    return 0;

            }
            public override void OverSel()
            {
                Selected = false;
            }
            public override void print()
            {
                Console.Write(x); Console.Write(" ");
                Console.Write(y); Console.Write(" ");
                Console.WriteLine(y);
            }
            ~Circle()
            {
                Console.WriteLine(string.Format("Circle::~Circle()"));
            }
        };

        public class MyStorage
        {
            int count = -1;
            int l = 80;
            AllObj[] arr = new AllObj[80];
            public void AddObject(ref AllObj[] arr, int n)
            {
                Array.Resize<AllObj>(ref arr, n + 3);
            }
            public void SetObject(int i, AllObj data)
            {
                if (i >= l)
                {
                    l = i;
                    AddObject(ref arr, i);
                }
                count++;
                arr[i] = data;
            }
            public AllObj GetObject(int i)
            {
                if (i >= l)
                {
                    l = i;
                    AddObject(ref arr, i);
                }
                if (arr[i] == null)
                {
                    return null;
                }
                else
                    return arr[i];
            }
            public int GetCount()
            {
                return count;
            }
            public void Del(int i)
            {
                if (i >= l)
                {
                    l = i;
                    AddObject(ref arr, i);
                }
                for (int j = i; j < count; j++)
                {
                    arr[j] = arr[j + 1];
                }
                arr[count] = null;
                count--;
            }
            public void printmy(int i)
            {
                arr[i].print();
            }
        };

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int p = 0;
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < storage.GetCount() + 1; i++)
                {
                    if ((storage.GetObject(i) != null) && (storage.GetObject(i).check(e.X, e.Y) == 1)) //если объект существует
                    {
                        p = 1;
                    }
                }
                if (p == 0)
                {
                    storage.SetObject(storage.GetCount() + 1, new Circle(e.X, e.Y));
                    for (int i = 0; i < storage.GetCount(); i++)
                    {
                        if (storage.GetObject(i) != null)
                            storage.GetObject(i).drawCircle(gr, new Pen(Color.Black));
                    }
                    storage.GetObject(storage.GetCount()).drawCircle2(gr, new Pen(Color.Red));
                    pictureBox1.Image = GetBitmap();
                }
                else
                {
                    for (int i = 0; i < storage.GetCount() + 1; i++)
                    {
                        if (storage.GetObject(i) != null)
                        {
                            storage.GetObject(i).checkclick(e.X, e.Y, gr, isCTRL);
                        }
                    }
                    pictureBox1.Image = GetBitmap();
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                isCTRL = false;
                for (int j = 0; j <= storage.GetCount() + 1; j++)
                {
                    if (storage.GetObject(j) != null)
                    {
                        storage.GetObject(j).OverSel();
                        storage.GetObject(j).drawCircle(gr, new Pen(Color.Black));
                    }
                }
                pictureBox1.Image = GetBitmap();
            }
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                isCTRL = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                for (int j = 0; j <= storage.GetCount() + 1; j++)
                {
                    if ((storage.GetObject(j) != null) && (storage.GetObject(j).GetSel() == true))
                    {
                        storage.Del(j);
                        j--;
                    }
                }
                gr.Clear(Color.White);
                for (int j = 0; j <= storage.GetCount() + 1; j++)
                {
                    if (storage.GetObject(j) != null)
                    {
                        storage.GetObject(j).drawCircle(gr, new Pen(Color.Black));
                    }
                }
                pictureBox1.Image = GetBitmap();
            }

        }
    }
}
