using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleClass1;
using System.Data.SQLite;

namespace SampleConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
    
            Console.WriteLine("Opening dbConnection and creating database.");
            string dbName = Console.ReadLine();

            try
            {
                SQLiteConnection dbConnection = dbMethods.dbCreate(ref dbName);
                Console.WriteLine("Success.");
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Failed!");
                Console.WriteLine(ex.Message + "  " + ex.StackTrace);
                Console.ReadLine();
                
            }
        }
    }
}
