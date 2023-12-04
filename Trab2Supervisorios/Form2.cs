using System;
using System.Drawing;
using System.IO.Ports;
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort.Open();
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            if (double.TryParse(dataString, out double dataValue))
            {
                TimeSpan time = DateTime.Now - startTime;
                Invoke(new Action(() =>
                {
                    chart1.Series["Vazão"].Points.AddXY(time.TotalSeconds, dataValue);
                }));
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine(textBox3.Text); 
            }
        }
    }
    }


    

