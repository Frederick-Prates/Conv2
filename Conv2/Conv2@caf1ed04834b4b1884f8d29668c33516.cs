using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conv2
{
    public partial class Conv2 : Form
    {
        int min, max;
        public Conv2()
        {
            InitializeComponent();
            __GrafConfig();
            _Limpar(true, false);
        }

        private double[] _Funcao_xn(int tamanho)                        // Calcula função x[n];
        {
            int a=min;
            double[] _xn = new double[tamanho];
            for(int q = 0; q < tamanho; q++)
            {
                _xn[q] = Math.Sin(a+q);
            }
            return (_xn);
        }

        private double[] _Funcao_hn(int tamanho)                        // Calcula função h[n];
        {
            int a = min;
            double[] _hn = new double[tamanho];
            for (int q = 0; q < tamanho; q++)
            {
                _hn[q] = Math.Cos(a+q);
            }
            return (_hn);
        }

        public int _Mede()
        {
            return (Math.Abs(min) + max + 1);
        }

        public void _Limpar(bool _inicializado, bool _limpa_grafico)
        {
            // dos gráficos, variáveis e 
            if (_limpa_grafico)         // Limpa apenas o gráfico               // caixas de texto de entrada
            {                                                                   // __Limpar recebe dois parametros
                chart1.Series["Series1"].Points.Clear();                        // booleanos:
                chart1.Series["Series2"].Points.Clear();                        // _inicializado: é usado quando
                chart2.Series["Series1"].Points.Clear();                        // deseja-se resetar variáveis, cx txt e o 
                chart2.Series["Series2"].Points.Clear();                        // grafico atual para o grafico "zerado".
                chart3.Series["Series1"].Points.Clear();                        // _limpa_grafico: é usado para limpar
                chart3.Series["Series2"].Points.Clear();                        // o gráfico sem resetar os valores das
                chart4.Series["Series1"].Points.Clear();                        // variáveis e cx txt.
                chart4.Series["Series2"].Points.Clear();
                chart5.Series["Series1"].Points.Clear();
                chart6.Series["Series1"].Points.Clear();
                chart7.Series["Series1"].Points.Clear();
            }
            else                                                                // Limpa variáveis e caixas de txt
            {
                textBox1.Text = "";
                _ate.Text = "";
                max = 2;
                min = -2;
                double[] xn = new double[5];
                double[] hn = new double[5];
                if (_inicializado)
                {
                    _Convoluir(xn, hn);
                }                       
            }
    }

        public void _Convoluir(double[] __xn,double[] __hn)
        {
            _Limpar(false, true);
            int a;
            a = min;
            int _yn_size=__xn.Length+__hn.Length-1;
            double[] __yn = new double[_yn_size];
            for (int i = 0; i < __xn.Length; i++)
            {
                for (int j = 0; j < __hn.Length; j++)
                {
                    __yn[i + j] += __xn[i] * __hn[j];                    
                }
            }
            for (int i = 0;i<_yn_size;i++)
            {
                chart1.Series["Series1"].Points.AddXY(i + (2 * a), __yn[i]);     //Plota o ponto y[n]
                chart1.Series["Series2"].Points.AddXY(i + (2 * a), __yn[i]);     //Plota a coluna y[n]
                chart7.Series["Series1"].Points.AddXY(i + (2 * a), __yn[i]);
                if (i == 0)
                {
                    textBox1.Text = ("{ (" + __yn[i]);
                }
                if (i == (_yn_size-1))
                {
                    textBox1.Text = (textBox1.Text + "), (" + __yn[i] + ") }");
                }
                if ((i != 0) && (i != (_yn_size - 1)))
                {
                    textBox1.Text = (textBox1.Text + "), (" + __yn[i]);
                }
            }
            for (int i = 0; i < __xn.Length; i++)
            {
                chart2.Series["Series1"].Points.AddXY(i + a, __xn[i]);                      //Plota o ponto x[n]
                chart2.Series["Series2"].Points.AddXY(i + a, __xn[i]);                      //Plota o coluna x[n]
                chart5.Series["Series1"].Points.AddXY(i + a, __xn[i]);                      //Plota o ponto x(t)
                chart6.Series["Series1"].Points.AddXY(i + a, __hn[i]);                      //Plota o ponto h(t)
                chart3.Series["Series1"].Points.AddXY(i + a, __hn[(__hn.Length - 1) - i]);  //Plota o ponto  h[-k]
                chart3.Series["Series2"].Points.AddXY(i + a, __hn[(__hn.Length - 1) - i]);  //Plota o coluna h[-k]
                chart4.Series["Series1"].Points.AddXY(i + a, __hn[i]);                      //Plota o ponto  h[n]
                chart4.Series["Series2"].Points.AddXY(i + a, __hn[i]);                      //Plota o coluna h[n]  
            }
        }

        public void __GrafConfig()                                  // Configurações dos gráficos
        {   // chart1 é o gráfico de y[n]
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "N";      // Nomeia eixo X como N
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y[n]";   // Nomeia eixo Y como Y[n]
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;     // Força a exibição de x em escala de 1 em 1
            chart1.Series["Series2"].LabelFormat = "{0:0.####}";    // Configura numero max de casas decimais no rótulo do ponto.
            chart1.Series["Series2"]["PixelPointWidth"] = "2";      // Configura a largura da coluna em pixels
            chart1.Series["Series2"].Color = Color.PowderBlue;      // Configura a cor da coluna
            chart1.ChartAreas["ChartArea1"].AxisY.Crossing = 0;     // Configura o eixo y = 0 como a origem de todos os gráficos plotados
            // chart2 é o gráfico de x[n]
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "N";      // Para os outros gráficos a configuração é a mesma
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "X[n]";   // alternando apenas rótulos e cores.               
            chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart2.Series["Series2"].LabelFormat = "{0:0.####}";
            chart2.Series["Series2"]["PixelPointWidth"] = "2";
            chart2.Series["Series2"].Color = Color.Purple;
            chart2.ChartAreas["ChartArea1"].AxisY.Crossing = 0;
            // chart3 é o gráfico de h[-k] ou h[n] espelhado
            chart3.ChartAreas["ChartArea1"].AxisX.Title = "K";
            chart3.ChartAreas["ChartArea1"].AxisY.Title = "H[-k]";
            chart3.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart3.Series["Series2"].LabelFormat = "{0:0.####}";
            chart3.Series["Series2"]["PixelPointWidth"] = "2";
            chart3.Series["Series2"].Color = Color.Crimson;
            chart3.ChartAreas["ChartArea1"].AxisY.Crossing = 0;
            // chart4 é o gráfico de h[n]
            chart4.ChartAreas["ChartArea1"].AxisX.Title = "N";
            chart4.ChartAreas["ChartArea1"].AxisY.Title = "H[n]";
            chart4.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart4.Series["Series2"].LabelFormat = "{0:0.####}";
            chart4.Series["Series2"]["PixelPointWidth"] = "2";
            chart4.Series["Series2"].Color = Color.OrangeRed;
            chart4.ChartAreas["ChartArea1"].AxisY.Crossing = 0;
            // chart4 é o gráfico de x(t)
            chart5.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chart5.ChartAreas["ChartArea1"].AxisY.Title = "X[t]";
            chart5.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart5.Series["Series1"].Color = Color.DarkGoldenrod;
            // chart6 é o gráfico de h(t)
            chart6.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chart6.ChartAreas["ChartArea1"].AxisY.Title = "H[t]";
            chart6.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart6.Series["Series1"].Color = Color.DarkGray;
            // chart7 é o gráfico de y(t)
            chart7.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chart7.ChartAreas["ChartArea1"].AxisY.Title = "Y(t)";
            chart7.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart7.Series["Series1"].Color = Color.PaleVioletRed;
        }

        private void _ate_TextChanged(object sender, EventArgs e)
        {
            if ((_ate.Text == "-") || (_ate.Text == "")) ;           // Tratamento de erro
            else
            {
                max = int.Parse(_ate.Text);
                min = -max;
            }
            if (max < 0)
            {
                _ate.Text = "";
                max = 0;
                // MessageBox.Show("No campo <T = > deve ser colocado <<inteiro positivo>>.", "Aviso");
            }
        }

        public void _conv_Click(object sender, EventArgs e)
        {
            int tamanho = _Mede();
            double[] xn = new double[tamanho];
            double[] hn = new double[tamanho];
            xn = _Funcao_xn(tamanho);
            hn = _Funcao_hn(tamanho);
            _Convoluir(xn,hn);
        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void _lim_Click(object sender, EventArgs e)
        {
            _Limpar(true,false);
        }
    }
}
