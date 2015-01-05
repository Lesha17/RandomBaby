using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Random_Baby
{
    public partial class Form1 : Form
    {
       static int CountOfParts = 20;
       static int IterationsCount = 20;
       public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
            chart1.Series.Add("Это сюрприз!");
            chart1.Series["Это сюрприз!"].ChartType = SeriesChartType.Column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Читаем
                int x0 = Convert.ToInt32(textBox1.Text);
                int a = Convert.ToInt32(textBox2.Text);
                int b = Convert.ToInt32(textBox3.Text);
                int c = Convert.ToInt32(textBox4.Text);

                //Генерим
                MyRandom rnd = new MyRandom(x0, a, b, c);
                int x = x0;
                for (int i = 1; i <= c; ++i)
                    x = rnd.next();

                //Длина периода
                int length = 1;
                for (; x != rnd.next(); ++length) ;

                label5.Text = "Длина периода: " + length.ToString();

                //Непериодическая часть
                MyRandom rndx = new MyRandom(x0, a, b, c);
                MyRandom rndy = new MyRandom(x0, a, b, c);

                int y = x0;
                x = x0;
                for (int i = 1; i <= length; ++i)
                    x = rndx.next();


                int lnp = 0;
                for (; y != x; y = rndy.next(), x = rndx.next(), ++lnp) ;

                label6.Text = "Непериодическая часть: " + lnp.ToString();

                //На конец качество
                int[] quality = new int[CountOfParts];
                for (int i = 0; i < CountOfParts; ++i)
                    quality[i] = 0;
                rnd = new MyRandom(x0, a, b, c);

                quality[ (x0* CountOfParts) / c]++;
                for (int i = 1; i < IterationsCount*CountOfParts; ++i)
                    quality[(rnd.next() * CountOfParts) / c]++;

                int s = 0;
                for (int i = 0; i < CountOfParts; ++i)
                    s += quality[i];

                Console.WriteLine(s);

                    chart1.Series.Clear();

                chart1.Series.Add("Рандом");
                chart1.Series["Рандом"].ChartType = SeriesChartType.Column;
                for (int i = 0; i < CountOfParts; ++i)
                {
                    chart1.Series["Рандом"].Points.AddY(quality[i]);    //Заполнение Chart элементами
                }

                //Считаем качество
                double qual = 0;
                for (int i = 0; i < CountOfParts; ++i)
                    qual += 1.0*(quality[i] - IterationsCount) * (quality[i] - IterationsCount) / (1.0*CountOfParts*CountOfParts);

                label7.Text = "Качество: "+qual.ToString();

                //Я так хочу
                if(checkBox1.Checked)
                {
                    rnd = new MyRandom(x0, a, b, c);
                    x = x0;
                    for (int i = 1; i <= c; ++i)
                    {

                        Console.Write("(" + a + "*" + x + " + " + b + " ) mod " + c + " = ");
                        x = rnd.next();

                        Console.WriteLine(x);
                    }
                    Console.WriteLine();
                    Console.WriteLine("**Конец**");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            catch
            {
                MessageBox.Show("Введите корректные данные");
            }

        }
    }
}
