using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Newtonsoft.Json;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using static Trab2Supervisorios.Program;

namespace Trab2Supervisorios
{
    public partial class Form2 : Form
    {

            OPCBlock OPC_Communication = new OPCBlock();
             static MonitoraTagOPC aciona = new MonitoraTagOPC();
             Task tarefaAciona = new Task(aciona.Loop);


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int contadortotal;
        public int contadortotalopac;
        public int contadortotalotransp;
        public int contadorparcialopac;
        public int contadorparcialtransp;

        public bool formopcStart;
        public bool formopcEmergencia;
        public bool formopcOpaca;
        public bool formopcTransp;

        public int formopcNopca;
        public int formopcNtransp;

        public bool formopcErro;
        public bool formopcReset;
        public bool formopcOcupada;

        int somador;
        public Form2()
        {
            InitializeComponent();

            aciona.DisparaSinalEvent += ProcessaPeça;
            tarefaAciona.Start();
        }

        
        private void Form2_Load(object sender, EventArgs e)
        {

        }
       private void ProcessaPeça(object sender, EventArgs e)
        {
            OPC_Communication.ReadBlock();
           
            if (OPC_Communication.Opaque == true)
            {
                log.Info("PEÇA OPACA");
                richTextBox1.Invoke((MethodInvoker)delegate {richTextBox1.Text += "\n" + ">>PEÇA OPACA"; });
              
                textBox1.Invoke((MethodInvoker)delegate { textBox1.Text = OPC_Communication.Number_Opaque.ToString(); });
                
                if ((contadortotalopac == 0) && (OPC_Communication.Number_Opaque == 1))
                {
                    contadortotalopac = 1;

                }
                if (contadortotalopac < (OPC_Communication.Number_Opaque))
                {
                    contadortotalopac = contadortotalopac + 1;

                }
                textBox4.Invoke((MethodInvoker)delegate { textBox4.Text = contadortotalopac.ToString(); });


            }
            if (OPC_Communication.Transparent == true)
            {
                log.Info("PEÇA TRANSPARENTE");
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>PEÇA TRANSPARENTE"; });
           
                textBox2.Invoke((MethodInvoker)delegate { textBox2.Text = OPC_Communication.Number_Transparent.ToString(); });
                
                if ((contadortotalotransp == 0) && (OPC_Communication.Number_Transparent == 1))
                {
                    contadortotalotransp = 1;

                }
                if (contadortotalotransp < (OPC_Communication.Number_Transparent))
                {
                    contadortotalotransp = contadortotalotransp + 1;

                }
                textBox4.Invoke((MethodInvoker)delegate { textBox3.Text = contadortotalotransp.ToString(); });
            }
            somador = OPC_Communication.Number_Transparent + OPC_Communication.Number_Opaque;
            textBox6.Invoke((MethodInvoker)delegate { textBox6.Text = somador.ToString(); });

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
            OPC_Communication.WriteBlock();
               OPC_Communication.Reset = true;
                   OPC_Communication.Number_Opaque =0;
                   OPC_Communication.Number_Transparent =0;

            log.Info("CONTADOR PARCIAL LIMPO");
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>CONTADOR PARCIAL LIMPO"; });
           
            textBox1.Text = contadorparcialopac.ToString();
            textBox2.Text = contadorparcialtransp.ToString();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            log.Info("PARADA DE EMERGENCIA");
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>PARADA DE EMERGENCIA"; });
           

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool estado = OPC_Communication.Start;
                 switch (estado)
                {
                case false:
                    OPC_Communication.Start = true;
                    OPC_Communication.WriteBlock();
                    log.Info("CONTAGEM INICADA");
                    richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>CONTAGEM INICADA"; });
                    button4.BackColor = Color.DarkRed;

                    break;
                case true:
                    OPC_Communication.Start = false;
                    OPC_Communication.WriteBlock();
                    log.Info("CONTAGEM PAUSADA");
                    richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>CONTAGEM PAUSADA"; });
                    button4.BackColor = Color.DarkGreen;
                    break;

                }

        }

    
    }
    }
    public class MonitoraTagOPC
    {
        public bool stop = false;

        public delegate void AcionaSaidaEventHandler(object sender, EventArgs eventArgs);

        public event AcionaSaidaEventHandler DisparaSinalEvent;

        public void Loop()
        {

            while (!stop)
            {
               
                Thread.Sleep(500);

                if (DisparaSinalEvent != null)
                {
                    DisparaSinalEvent(this, EventArgs.Empty);
                }
            }
        }
    }

    

