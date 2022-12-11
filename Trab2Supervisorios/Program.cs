using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            
            
            // Comunicação Opc
            TitaniumAS.Opc.Client.Bootstrap.Initialize();

            Uri url = UrlBuilder.Build("Matrikon.OPC.Simulation.1");
            //
            using (var server = new OpcDaServer(url))
            {
                
                server.Connect();
               
            }
           
            void BrowseChildren(IOpcDaBrowser browser, string itemId = null, int indent = 0)
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

            var definition1 = new OpcDaItemDefinition
            {
                ItemId = "Random.Int2",
                IsActive = true
            };
            var definition2 = new OpcDaItemDefinition
            {
                ItemId = "Bucket Brigade.Int4",
                IsActive = true
            };
            //OpcDaItemDefinition[] definitions = { definition1, definition2 };
            //    OpcDaItemResult[] results = group.AddItems(definitions);

            // Handle adding results.
            //foreach (OpcDaItemResult result in results)
            //{
            //    if (result.Error.Failed)
            //        Console.WriteLine("Error adding items: {0}", result.Error);
            //}




        }
    }
}
