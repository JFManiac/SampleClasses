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
        
        //public static SQLiteConnection dbCreate(ref string dbName)
        //{
            //connectionCache dbCache = new connectionCache();
            //SQLiteConnection dbConn = new SQLiteConnection();

            //if (dbConnection == "")
            //{
            //    throw new Exception("No connection!");
            //    return null;
            //}

            //dbConn = dbCache.GetDataConnectionFromCache(ref dbConnection);

           // return dbCreate(ref dbName);
        //}

        public static SQLiteConnection dbCreate(ref string dbName)
        {
            SQLiteConnection dbConnection;
            try
            {
                SQLiteConnection.CreateFile(dbName);
            }
            catch (Exception ex)
            {
                throw new Exception("Create database failed!", ex);
                return null;
            }

            dbConnection = new SQLiteConnection("Data Source=" + dbName + ";Version=3;");
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
                return null;
            }

            return dbConnection;

        }

        public static void dbInsert()
        {

        }

       public dbMethods()
        {
            // Constructor

        }

    }



}
