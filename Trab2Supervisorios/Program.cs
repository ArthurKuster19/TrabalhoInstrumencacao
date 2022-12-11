using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TitaniumAS.Opc.Client;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace Trab2Supervisorios
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Bootstrap.Initialize();

            //Classificar  



            // Carregando Xml *Vai mudar

            MyXLMHandler myXLM = new MyXLMHandler();
            string msg = "Mensagem a ser impressa no console.";

            string font_type = myXLM.readNode("fonte");
            string msg_type = myXLM.readNode("tipo_msg");

            if (font_type == "caixa_alta")
            {
                msg = msg.ToUpper();
            }

            msg = msg_type + " - " + msg;

            Console.WriteLine(msg);
            Console.ReadKey();


            // Comunicação Opc
            TitaniumAS.Opc.Client.Bootstrap.Initialize();

            Uri url = UrlBuilder.Build("Matrikon.OPC.Simulation.1");// ServerOPC
            //
            using (var server = new OpcDaServer(url))
            {

                server.Connect();

            }

        }

        //classe configura Xml
        class MyXLMHandler
        {
            XmlDocument doc = new XmlDocument();

            public MyXLMHandler()
            {
                doc.Load(@"config.xml");
            }

            public string readNode(string node)
            {
                var value = doc.GetElementsByTagName(node).Item(0).InnerText;
                return value;
            }

        }

        //classe le Xml

        public class ListCofingPadrao
        {
            public string tag { get; set; }
            public string valor { get; set; }
            public string offset { get; set; }
        }

        static void BrowseChildren(IOpcDaBrowser browser, string itemId = null, int indent = 0)
            {
               
                OpcDaBrowseElement[] elements = browser.GetElements(itemId);

              
                foreach (OpcDaBrowseElement element in elements)
                {
                    
                    Console.Write(new String(' ', indent));
                    Console.WriteLine(element);

                    if (!element.HasChildren)
                        continue;

                  
                    BrowseChildren(browser, element.ItemId, indent + 2);
                }
            }







        //*REDEFINIR MODO DE CONFIGURAÇÃO DEPOIS


            //var definition1 = new OpcDaItemDefinition
            //{
            //    ItemId = "Random.Int2",
            //    IsActive = true
            //};
            //var definition2 = new OpcDaItemDefinition
            //{
            //    ItemId = "Bucket Brigade.Int4",
            //    IsActive = true
            //};
        

        }
    }

