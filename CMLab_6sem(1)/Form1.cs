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

namespace CMLab_6sem_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double FI(double x,int k)
        {
            if (k == 0)
            {
                if ((x <= 0) && (x >= -1))
                    return Math.Pow(x, 3) + 3 * Math.Pow(x, 2);
                else
                    return -Math.Pow(x, 3) + 3 * Math.Pow(x, 2);
            }
            if (k == 1)
            {
                return Math.Log(x + 1) / (x + 1);
            }
            if (k == 2)
            {
                return Math.Log(x + 1) / (x + 1) + Math.Cos(10 * x);
            }
            if (k == 3)
            {
                return Math.Log(x + 1) / (x + 1) + Math.Cos(100 * x);
            }
            else return 0;
        }

        double pFI(double x,int k)
        {
            if (k == 0)
            {
                if ((x <= 0) && (x >= -1))
                    return 3 * Math.Pow(x, 2) + 6 * x;
                else if (x > 0)
                    return -3 * Math.Pow(x, 2) + 6 * x;
            }
            if (k == 1)
            {
                return (1 - Math.Log(x + 1)) / Math.Pow(x + 1, 2);
            }
            if (k == 2)
            {
                return (1 - Math.Log(x + 1)) / Math.Pow(x + 1, 2) - 10 * Math.Sin(10 * x);
            }
            if (k == 3)
            {
                return (1 - Math.Log(x + 1)) / Math.Pow(x + 1, 2) - 100 * Math.Sin(100 * x);
            }
            else
                return 0;
        }

        double p2FI(double x,int k)
        {
            if (k == 0)
            {
                if ((x <= 0) && (x >= -1))
                    return 6 * x + 6;
                else if (x > 0)
                    return -6 * x + 6;
            }
            if (k == 1)
            {
                return (2 * Math.Log(x + 1) - 3) / Math.Pow(x + 1, 3);
            }
            if (k == 2)
            {
                return (2 * Math.Log(x + 1) - 3) / Math.Pow(x + 1, 3) - 100 * Math.Cos(10 * x);
            }
            if (k == 3)
            {
                return (2 * Math.Log(x + 1) - 3) / Math.Pow(x + 1, 3) - 10000 * Math.Cos(100 * x);
            }
            else
                return 0;
        }

        double S(double[]X,double y,double[] a,double[] b,double[]c,double[]d,int n)
        {
            for(int i = 1; i <= n; i++)
            {
                if ((y >= X[i - 1])&& (y <= X[i]))
                {
                    return (a[i] + b[i] * (y - X[i]) + c[i] / 2 * Math.Pow((y - X[i]), 2) + d[i] / 6 * Math.Pow((y - X[i]), 3));
                }
           }
                    
            return 0;
        }

        double pS(double[]X,double y,double[]b,double[]c,double[]d,int n)
        {
            for (int i = 1; i <= n; i++)
            {
                if ((y >= X[i - 1]) && (y <= X[i]))
                {
                    return (b[i] + c[i] * (y - X[i]) + d[i] / 2 * Math.Pow((y - X[i]), 2));
                }
            }

            return 0.0;
        }

        double p2S(double[] X, double y, double[] c, double[] d, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                if ((y >= X[i - 1]) && (y <= X[i]))
                {
                    return (c[i] + d[i] * (y - X[i]));
                }
            }
            return 0.0;
        }

        void Progonka(int n, double[] a, double[] b, double[] d, double h, double[] c)
        {
            double[] alpha = new double[n + 1];
            double[] betta = new double[n + 1];
            alpha[1] = 0;
            betta[1] = c[0];
            for (int i = 1; i <= n - 1; i++)
            {
                alpha[i + 1] = (-1.0) * h / (alpha[i] * h + 4 * h);
                betta[i + 1] = (-6 * ((a[i + 1] - a[i]) / h - (a[i] - a[i - 1]) / h) + betta[i] * h) / (-4 * h - alpha[i] * h);
            }
            for (int i = n; i >= 1; i--)
            {
                c[i - 1] = alpha[i] * c[i] + betta[i];
            }
            for (int i = 1; i <= n; i++)
            {
                b[i] = (a[i] - a[i - 1]) / h + h * (2 * c[i] + c[i - 1]) / 6;
                d[i] = (c[i] - c[i - 1]) / h;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int k = 0;
            double p = 0, q = 0, y = 0, h = 0, y1 = 0, y2 = 0, y3 = 0, m4 = 0, m5 = 0, m6 = 0;
            double[] a = new double[n + 1];
            double[] b = new double[n + 1];
            double[] c = new double[n + 1];
            double[] d = new double[n + 1];
            double[] f = new double[n + 1];
            double[] X = new double[n + 1];

            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";

                if (comboBox1.Text == "Тестовая Задача")
                {
                    k = 0;
                    q = -1.0; p = 1.0;
                    c[0] = 0; c[n] = 0;
                }
                if (comboBox1.Text == "Основная 1")
                {
                    k = 1;
                    q = 0.2; p = 2.0;
                }
                if (comboBox1.Text == "Основная 2")
                {
                    k = 2;
                    q = 0.2; p = 2.0;
                }
                if (comboBox1.Text == "Основная 3")
                {
                    k = 3;
                    q = 0.2; p = 2.0;
                }
                if ((comboBox2.Text == "")&&(comboBox1.Text!="Тестовая Задача"))
                {
                    MessageBox.Show("Выберите Граничные Условия!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                double m1 = 0, m2 = 0;
                h = (double)(p - q) / n;

                for (int i = 0; i <= n; i++)
                {
                    X[i] = q + i * h;
                    a[i] = FI(X[i], k);
                }

                if (comboBox2.Text == "Совпадение вторых производных")
                {
                    c[0] = p2FI(q, k); c[n] = p2FI(p, k);m1 = p2FI(q, k);
                    m2 = p2FI(p, k);
                }
                if (comboBox2.Text == "Естественные граничные условия")
                {
                    c[0] = 0; c[n] = 0;m1 = 0;m2 = 0;
                }

                Progonka(n, a, b, d, h, c);

                dataGridView1.Rows.Clear();
                for (int i = 0; i < n; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = i + 1;
                    dataGridView1[1, i].Value = X[i];
                    dataGridView1[2, i].Value = X[i + 1];
                    dataGridView1[3, i].Value = a[i + 1];
                    dataGridView1[4, i].Value = b[i + 1];
                    dataGridView1[5, i].Value = c[i + 1];
                    dataGridView1[6, i].Value = d[i + 1];
                }

                y = q;
                dataGridView2.Rows.Clear();
                int j = 0;
                for (int i = 0; i <= 4 * n; i++)
                {
                    if (y <= p)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2[0, i].Value = y;
                        dataGridView2[1, i].Value = S(X, y, a, b, c, d, n);
                        dataGridView2[2, i].Value = FI(y, k);
                        dataGridView2[3, i].Value = Math.Abs(FI(y, k) - S(X, y, a, b, c, d, n));

                        if (m4 < Math.Abs(FI(y, k) - S(X, y, a, b, c, d, n)))
                        {
                            m4 = Math.Abs(FI(y, k) - S(X, y, a, b, c, d, n));
                            y1 = y;
                        }

                        dataGridView2[4, i].Value = pS(X, y, b, c, d, n);
                        dataGridView2[5, i].Value = pFI(y, k);
                        dataGridView2[6, i].Value = Math.Abs(pFI(y, k) - pS(X, y, b, c, d, n));

                        if (m5 < Math.Abs(pFI(y, k) - pS(X, y, b, c, d, n)))
                        {
                            m5 = Math.Abs(pFI(y, k) - pS(X, y, b, c, d, n));
                            y2 = y;
                        }

                        dataGridView2[7, i].Value = p2S(X, y, c, d, n);
                        dataGridView2[8, i].Value = p2FI(y, k);
                        dataGridView2[9, i].Value = Math.Abs(p2FI(y, k) - p2S(X, y, c, d, n));

                        if (m6 < Math.Abs(p2FI(y, k) - p2S(X, y, c, d, n)))
                        {
                            m6 = Math.Abs(p2FI(y, k) - p2S(X, y, c, d, n));
                            y3 = y;
                        }
                        j++;
                    }
                    y += h / 4.0;
                }

                label2.Text = "Основная сетка n = " + Convert.ToString(n);
                label3.Text = "Дополнительная сетка N = " + Convert.ToString(n * 4);
                label4.Text = "max|f(x)-S(x)|=" + Convert.ToString(m4) + ", при x=" + Convert.ToString(y1);
                label5.Text = "max|f'(x)-S'(x)|=" + Convert.ToString(m5) + ", при x=" + Convert.ToString(y2);
                label6.Text = "max|f''(x)-S''(x)|=" + Convert.ToString(m6) + ", при x=" + Convert.ToString(y3);

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.Series[2].Points.Clear();
                chart1.Series[1].Color = Color.Red;
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[2].Color = Color.Black;

                chart2.Series[0].Points.Clear();
                chart2.Series[1].Points.Clear();
                chart2.Series[2].Points.Clear();
                chart2.Series[1].Color = Color.Red;
                chart2.Series[0].Color = Color.Blue;
                chart2.Series[2].Color = Color.Black;

                chart3.Series[0].Points.Clear();
                chart3.Series[1].Points.Clear();
                chart3.Series[2].Points.Clear();
                chart3.Series[1].Color = Color.Red;
                chart3.Series[0].Color = Color.Blue;
                chart3.Series[2].Color = Color.Black;

                for (y=q; y < p; y+=0.01)
                {
                    chart1.Series[0].Points.AddXY(y, FI(y, k));
                    chart2.Series[0].Points.AddXY(y, pFI(y, k));
                    chart3.Series[0].Points.AddXY(y, p2FI(y, k));

                    chart1.Series[2].Points.AddXY(y, FI(y, k) - S(X, y, a, b, c, d, n));
                    chart2.Series[2].Points.AddXY(y, pFI(y, k) - pS(X, y, b, c, d, n));
                    chart3.Series[2].Points.AddXY(y, p2FI(y, k) - p2S(X, y, c, d, n));

                    chart1.Series[1].Points.AddXY(y, S(X, y, a, b, c, d, n));
                    chart2.Series[1].Points.AddXY(y, pS(X, y, b, c, d, n));
                    chart3.Series[1].Points.AddXY(y, p2S(X, y, c, d, n));
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
