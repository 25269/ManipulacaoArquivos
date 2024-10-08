using System.IO;
using System.Globalization;
using ManipulacaoArquivosTexto.Entities;

namespace ManipulacaoArquivosTexto
{
    public class Program
    {
        static void Main(string[] args)
        {
            double total = 0;

            Console.Write("Enter file full path: ");
            string sourceFilePath = Console.ReadLine();

            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                string targetFolderPath = sourceFolderPath + @"\out";
                string targetFilePath = targetFolderPath + @"\summary.csv";

                Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach(string line in lines) 
                    {
                        string[] columns = line.Split(",");

                        string nameProduct = columns[0];
                        double value = double.Parse(columns[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(columns[2]);

                        Product product = new Product(nameProduct, value, quantity);

                        total = product.Total(product.Price, product.Quantity);

                        Console.WriteLine(nameProduct + "," + total.ToString("F2", CultureInfo.InvariantCulture));

                        sw.WriteLine(nameProduct + "," + total.ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}