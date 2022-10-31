using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace PD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            Console.WriteLine("Ievadiet profesijas nosaukumu");
            input = Console.ReadLine();
            string Url = "https://data.gov.lv/dati/lv/api/3/action/datastore_search?q=" + input + "&resource_id=0fb8f9ea-7acb-4c0a-8fd2-a5503d88e6e5";
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData(Url);
            string json = Encoding.UTF8.GetString(buffer);
            var resultObject = JObject.Parse(json);
            //Console.WriteLine(resultObject.ToString());
            string kods; //resultObject["result"]["records"][0]["Kods"].ToString();
            //Console.WriteLine(kods);
            //Console.ReadLine();
            string nosaukums, apraksts, vecaks;
            bool check = false;
            StringBuilder sb = new StringBuilder();
            foreach (var item in resultObject["result"]["records"])
            {
                if (item["Nosaukums"].ToString().ToLower().Contains(input) == true)
                {
                    kods = item["Kods"].ToString();
                    nosaukums = item["Nosaukums"].ToString();
                    apraksts = item["Apraksts"].ToString();
                    vecaks = item["Vecāks"].ToString();
                    sb.AppendLine(kods + ':' + nosaukums + ':' + apraksts + ':' + vecaks + ' ');
                    Console.WriteLine(sb);
                    Console.WriteLine();
                    check = true;
                }
            }
            if (check == false)
                Console.WriteLine("Nav atrasts");
            File.WriteAllText("C:/temp/prof.txt", sb.ToString());
            Console.WriteLine("Fails atrodas C:\\temp\\prof.txt");
            Console.ReadLine();
        }
    }
}
