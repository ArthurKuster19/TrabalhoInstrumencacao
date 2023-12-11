using System;
using System.Drawing;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Trab2Supervisorios
{
    public partial class Form2 : Form
    {
        SerialPort serialPort;
        DateTime startTime = DateTime.Now;
        public Form2()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.PortName = textBox6.Text; // nome da porta
            serialPort.BaudRate = 9600;
            serialPort.DataReceived += SerialPort_DataReceived;
            InitializeChart();
        }

        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
        private void InitializeChart()
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Vazão");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
           
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dataString = serialPort.ReadLine();
            var retorno = ExtrairNumeroDecimal(dataString);
           
            if (double.TryParse(retorno, out double dataValue))
            {
                TimeSpan time = DateTime.Now - startTime;
                var tempoFormatado = time.TotalSeconds.ToString("0.0") + "s";
                Invoke(new Action(() =>
                {
                    chart1.Series["Vazão"].Points.AddXY(tempoFormatado, dataValue);
                }));
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine(textBox3.Text);
                serialPort.DataReceived += SerialPort_DataReceived;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            serialPort.Open();
            button1.Enabled = false;
        }

        public static string ExtrairNumeroDecimal(string texto)
        {
            // Regex para encontrar um número decimal
            Regex regex = new Regex(@"\d+\.\d+");
            Match match = regex.Match(texto);

            return match.Success ? match.Value : null;
        }
    }
    }


    

