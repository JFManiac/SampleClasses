using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace SampleClass
{
    public class connectionCache
    {
        // Define variables
        Boolean ConnectionsInitialized = false;
        string[] ConnectionsConnectionString;
        SQLiteConnection[] ConnectionsDataConnection;
        DateTime[] ConnectionsTimeStamp;

        public SQLiteConnection GetDataConnectionFromCache(ref string ConnectionString)
        {

            if (ConnectionsInitialized = false)
            {
                // Initialize variables
                string[] ConnectionsConnectionString = new string[50];
                SQLiteConnection[] ConnectionsDataConnection = new SQLiteConnection[50];
                DateTime[] ConnectionsTimeStamp = new DateTime[50];
                ConnectionsInitialized = true;

                int Oldest = 0;
                DateTime OldestTime;

                //   See if connection already in cache
                for (int i = 0; i < ConnectionsConnectionString.Length; i++)
                {
                    if (ConnectionsConnectionString[i] == ConnectionString)
                    {
                        ConnectionsTimeStamp[i] = DateTime.Now;

                        //Must check to see if connection still valid and open. If it isn't, open it here

                        if (ConnectionsDataConnection[i].State == ConnectionState.Closed)
                        {

                            try
                            {
                                ConnectionsDataConnection[i].ConnectionString = ConnectionsConnectionString[i];
                                ConnectionsDataConnection[i].Open();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Connecting to SQL failed" + ConnectionString + "  " + ex.Message + "  " + ex.StackTrace);
                                return null;
                            }

                            return ConnectionsDataConnection[i];
                        }
                    }
                }

                //   Add new connection if not in the list
                for (int i = 0; i < ConnectionsConnectionString.Length; i++)
                {
                    if (ConnectionsConnectionString[i] == "")
                    {

                        ConnectionsConnectionString[i] = ConnectionString;
                        ConnectionsDataConnection[i] = new SQLiteConnection();
                        try
                        {
                            ConnectionsDataConnection[i].ConnectionString = ConnectionsConnectionString[i];
                            ConnectionsDataConnection[i].Open();
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Connecting to SQL failed" + ConnectionString + "  " + ex.Message + "  " + ex.StackTrace);
                            return null;
                        }

                        ConnectionsTimeStamp[i] = DateTime.Now;
                        return ConnectionsDataConnection[i];
                    }
                }

                OldestTime = DateTime.Now;
                //  Now find the oldest connection and reuse that one
                //  Note: if this fails, the code will reuse the connection in index 0. That is ok.
                for (int i = 0; i < ConnectionsConnectionString.Length; i++)
                {
                    if (ConnectionsTimeStamp[i] < OldestTime)
                    {
                        Oldest = i;
                        OldestTime = ConnectionsTimeStamp[i];
                    }
                }

                //   Close connection in index Oldest if it is open
                if (ConnectionsDataConnection[Oldest].State == ConnectionState.Open)
                {
                    ConnectionsDataConnection[Oldest].Close();
                }

                ConnectionsConnectionString[Oldest] = ConnectionString;

                try
                {
                    ConnectionsDataConnection[Oldest].ConnectionString = ConnectionString;
                    ConnectionsDataConnection[Oldest].Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("Connecting to SQL failed" + ConnectionString + "  " + ex.Message + "  " + ex.StackTrace);
                    return null;
                }

                ConnectionsTimeStamp[Oldest] = DateTime.Now;
                return ConnectionsDataConnection[Oldest];
            }

            return null;
        }

        public connectionCache()
        {
            // Constructor


        }
    }
}
