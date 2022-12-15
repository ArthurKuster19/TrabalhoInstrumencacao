using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Newtonsoft.Json;
using TitaniumAS.Opc.Client.Common;
using static Trab2Supervisorios.Program;

namespace Trab2Supervisorios
{
    public partial class Form2 : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int contadortotal;
        public int contadortotalopac;
        public int contadortotalotransp;
        public int contadorparcialopac;
        public int contadorparcialtransp;

        public static string todasTags = ConfigurationSettings.AppSettings["TAGS"];

        public bool formopcStart;
        public bool formopcEmergencia;
        public bool formopcOpaca;
        public bool formopcTransp;

        public int formopcNopca;
		public int formopcNtransp;

        public bool formopcErro;
		public bool formopcReset;
		public bool formopcOcupada;
        public Form2()
        {
            InitializeComponent();
            

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        public void ProcessaPeça(int entrada, int tipo)
        {
            int[] separator = new int [1];
            int[] contadorTipo = new int [1];
            int[] contadorQtd = new int [1];

            separator[0] = entrada;
            separator[1] = tipo;
           
            

            if (separator [0] == 1) 
            {
                log.Info("PEÇA OPACA");
                richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA OPACA";
                contadorTipo [0] = contadorTipo[0] + 1;
                contadorparcialopac = contadorTipo[0];
                contadortotalopac = contadorTipo[0];
                textBox1.Text = contadorparcialopac.ToString();
                textBox3.Text = contadortotalopac.ToString();

            }
            if (separator[0] == 2)
            {
                log.Info("PEÇA TRANSPARENTE");
                richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA TRANSPARENTE";
                contadorTipo[1] = contadorTipo[1] + 1;
                contadorparcialtransp = contadorTipo[1];
                contadortotalotransp = contadorTipo[1];
                textBox2.Text = contadorparcialtransp.ToString();
                textBox4.Text = contadortotalotransp.ToString();

            }

            if (separator[1] == 1)
            {
                log.Info("PECA ENTRANDO");
                richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA ENTRANDO";
                contadorQtd[0] = contadorQtd[0] + 1;

            }
            if (separator[1] == 2)
            {
                log.Info("PEÇA SAINDO");
                richTextBox1.Text = richTextBox1.Text + "\n" + ">>PEÇA SAINDO";
                contadorQtd[1] = contadorQtd[1] + 1;
                contadortotal = contadorQtd[1];

            }
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

        private void button2_Click_1(object sender, EventArgs e )
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

          //  TitaniumAS.Opc.Client.Bootstrap.Initialize();

            Uri url = UrlBuilder.Build("ArthurServerOpc"); // ServerOPC

            //ProcessaPeça();


            //Ler Variaveis e Atribuir Valor padrão de acordo com o Xml

            List<ListCofingPadrao> list = JsonConvert.DeserializeObject<List<ListCofingPadrao>>(todasTags);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (var item in list)
            {
                
                if(item.tag == "opcStart")
                {
                    formopcStart = bool.Parse(item.valor);
                }
                if (item.tag == "opcEmergencia")
                {
                    formopcEmergencia = bool.Parse(item.valor);
                }
                if (item.tag == "opcOpaca")
                {
                    formopcOpaca = bool.Parse(item.valor);
                }
                if (item.tag == "opcNopac")
                {
                    formopcNopca = Int16.Parse(item.valor);
                }
                if (item.tag == "opcTransp")
                {
                    formopcTransp = bool.Parse(item.valor);
                }
                if (item.tag == "opcNtransp")
                {
                    formopcNtransp = Int16.Parse(item.valor);
                }
                if (item.tag == "opcErro")
                {
                    formopcErro = bool.Parse(item.valor);
                }
                if (item.tag == "opcReset")
                {
                    formopcReset = bool.Parse(item.valor);
                }
                if (item.tag == "opcOcupada")
                {
                    formopcOcupada = bool.Parse(item.valor);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

