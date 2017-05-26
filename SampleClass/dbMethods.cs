using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SampleClass1
{
    public class dbMethods
    {
        //
        

        public static Boolean dbCreate(ref string dbConnection, ref string dbName)
        {
            connectionCache dbCache = new connectionCache();
            SQLiteConnection dbConn = new SQLiteConnection();

            if (dbConnection == "")
            {
                throw new Exception("No connection!");
                return false;
            }

            dbConn = dbCache.GetDataConnectionFromCache(ref dbConnection);

            return dbCreate(ref dbConn, ref dbName);
        }

        public static Boolean dbCreate(ref SQLiteConnection dbConnection, ref string dbName)
        {
            try
            {
                SQLiteConnection.CreateFile("Contacts.db");
            }
            catch (Exception ex)
            {
                throw new Exception("Create database failed!", ex);
                return false;
            }

            dbConnection = new SQLiteConnection("Data Source=Contacts.db;Version=3;");
            dbConnection.Open();

            String sqlString = "create table names (name varchar(30), age int)";

            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Execute failed!", ex);
                return false;
            }

            return true;

        }

        public static void dbInsert()
        {

        }

    }



}
