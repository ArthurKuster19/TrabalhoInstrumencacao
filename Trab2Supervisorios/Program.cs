using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        //[STAThread]
        static void Main()
        {
            TitaniumAS.Opc.Client.Bootstrap.Initialize();
           
            // Comunicação Opc

            Uri url = UrlBuilder.Build("ArthurServerOpc.Grupo1");// ServerOPC


            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                OpcDaGroup group = server.AddGroup("MyGroup");
                group.IsActive = true;

                var itemBool = new OpcDaItemDefinition
                {
                    ItemId = "Random.Boolean",
                    IsActive = true
                };

                OpcDaItemDefinition[] opcDaItems = { itemBool };
                OpcDaItemResult[] results = group.AddItems(opcDaItems);

                foreach (OpcDaItemResult result in results)
                {
                    if (result.Error.Failed)
                        Console.WriteLine("Error adding items: {0}", result.Error);
                }

                while (true)
                {
                    OpcDaItemValue[] values = group.Read(group.Items, OpcDaDataSource.Device);
                    Console.WriteLine("Value is {0}", Convert.ToString(values[0].Value));
                    Thread.Sleep(3000);
                }

                //var browser = new OpcDaBrowserAuto(server);
                //BrowseChildren(browser);

            }

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Bootstrap.Initialize();
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
        //

        //static void BrowseChildren(IOpcDaBrowser browser, string itemId = null, int indent = 0)
        //    {

        //        OpcDaBrowseElement[] elements = browser.GetElements(itemId);


        //        foreach (OpcDaBrowseElement element in elements)
        //        {

        //            Console.Write(new String(' ', indent));
        //            Console.WriteLine(element);

        //            if (!element.HasChildren)
        //                continue;


        //            //BrowseChildren(browser, element.ItemId, indent + 2);
        //        }
        //    }

        //}
    }
}
