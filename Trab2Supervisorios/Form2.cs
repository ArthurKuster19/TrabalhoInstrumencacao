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


        bool estadoLiga = false;
        bool estadoEmergencia = false;
        public int N_Opaque = 0;
        public int N_Transp = 0;
        public int N_Opaque_Ant = 0;
        public int N_Transp_Ant = 0;
        public int aux_opaque = 0;
        public int aux_transp = 0;
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
            int[] separator = new int[1];
            int[] contadorTipo = new int[1];
            int[] contadorQtd = new int[1];

            //separator[0] = entrada;
            //separator[1] = tipo;


            if (OPC_Communication.Opaque == true)
            {
                log.Info("PEÇA OPACA");
                richTextBox1.Invoke((MethodInvoker)delegate {richTextBox1.Text += "\n" + ">>PEÇA OPACA"; });
                
                contadortotalopac = OPC_Communication.Number_Opaque + contadortotalopac;
                textBox1.Invoke((MethodInvoker)delegate { textBox1.Text = OPC_Communication.Number_Opaque.ToString(); });
                textBox3.Invoke((MethodInvoker)delegate { textBox4.Text = contadortotalopac.ToString(); });
          

            }
            if (OPC_Communication.Transparent == true)
            {
                log.Info("PEÇA TRANSPARENTE");
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "\n" + ">>PEÇA TRANSPARENTE"; });
                contadorparcialtransp = contadorparcialtransp + 1 ;
                contadortotalotransp = contadortotalotransp + 1;

                textBox2.Invoke((MethodInvoker)delegate { textBox2.Text = OPC_Communication.Number_Transparent.ToString(); });
                textBox4.Invoke((MethodInvoker)delegate { textBox3.Text = contadortotalotransp.ToString(); });

            }
            int somador = OPC_Communication.Number_Transparent + OPC_Communication.Number_Opaque;
            textBox6.Invoke((MethodInvoker)delegate { textBox6.Text = somador.ToString(); });

            //if (separator[1] == 1)
            //{
            //    log.Info("PECA ENTRANDO");
            //    richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA ENTRANDO";
            //    contadorQtd[0] = contadorQtd[0] + 1;

            //}
            //if (separator[1] == 2)
            //{
            //    log.Info("PEÇA SAINDO");
            //    richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA SAINDO";
            //    contadorQtd[1] = contadorQtd[1] + 1;
            //    contadortotal = contadorQtd[1];

            //}
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
            log.Info("CONTADOR PARCIAL LIMPO");
            richTextBox1.Text = richTextBox1.Text + "\n" + ">>CONTADOR PARCIAL LIMPO";
            contadorparcialtransp = 0;
            contadorparcialopac = 0;
            textBox1.Text = contadorparcialopac.ToString();
            textBox2.Text = contadorparcialtransp.ToString();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            log.Info("PARADA DE EMERGENCIA");
            richTextBox1.Text = richTextBox1.Text + "\n" + ">>PARADA DE EMERGENCIA";

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

           

            if (estadoLiga == false)
            {
                OPC_Communication.Start = true;
                OPC_Communication.WriteBlock();
                estadoLiga = true;
                log.Info("Esteira Ligada.");
            }
            else
            {
                OPC_Communication.Start = false;
                OPC_Communication.WriteBlock();
                estadoLiga = false;
                log.Info("Esteira Desligada.");
            }





            //  TitaniumAS.Opc.Client.Bootstrap.Initialize();


            //Uri url = UrlBuilder.Build("ArthurServerOpc.Grupo1");// ServerOPC


            //using (var server = new OpcDaServer(url))
            //{
            //    server.Connect();

            //    OpcDaGroup group = server.AddGroup("MyGroup");
            //    group.IsActive = true;

            //    var itemBool = new OpcDaItemDefinition
            //    {
            //        ItemId = "Random.Boolean",
            //        IsActive = true
            //    };

            //    OpcDaItemDefinition[] opcDaItems = { itemBool };
            //    OpcDaItemResult[] results = group.AddItems(opcDaItems);

            //    foreach (OpcDaItemResult result in results)
            //    {
            //        if (result.Error.Failed)
            //            Console.WriteLine("Error adding items: {0}", result.Error);
            //    }

            //    while (true)
            //    {
            //        OpcDaItemValue[] values = group.Read(group.Items, OpcDaDataSource.Device);
            //        Console.WriteLine("Value is {0}", Convert.ToString(values[0].Value));
            //        Thread.Sleep(3000);
            //    }


            //ProcessaPeça();
            //List<ListCofingPadrao>

            //Ler Variaveis e Atribuir Valor padrão de acordo com o Xml


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

    

