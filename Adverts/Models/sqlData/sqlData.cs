using System;
using System.Data;

public class sqlData
{
    public static DataTable sqlQueryFill(string connectionName, string sqlText)
    {
        DataTable result = new DataTable();

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ToString();
        using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
        {
            connection.Open();

            Npgsql.NpgsqlCommand suppliersCommand = new Npgsql.NpgsqlCommand(sqlText, connection);
            suppliersCommand.CommandType = CommandType.Text;
            Npgsql.NpgsqlDataAdapter suppliersAdapter = new Npgsql.NpgsqlDataAdapter(suppliersCommand);

            DataSet oDataSet = new DataSet();
            suppliersAdapter.Fill(oDataSet, "itemList");
            result = oDataSet.Tables["itemList"];
            suppliersAdapter = null;
            oDataSet = null;
            connection.Close();
        }
        return result;
    }

    public static void sqlQueryExecute(string connectionName, string sqlText)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
        {
            connection.Open();
            Npgsql.NpgsqlCommand suppliersCommand = new Npgsql.NpgsqlCommand(sqlText, connection);
            suppliersCommand.ExecuteNonQuery();
            connection.Close();
        }
    }

    public static int sqlInsertWithId(string connectionName, string sqlText, string sqlKeyText)
    {
        int result = 0;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
        {
            connection.Open();

            Npgsql.NpgsqlCommand suppliersCommand = new Npgsql.NpgsqlCommand(sqlText + sqlKeyText, connection);
            suppliersCommand.CommandType = CommandType.Text;
            Npgsql.NpgsqlDataAdapter suppliersAdapter = new Npgsql.NpgsqlDataAdapter(suppliersCommand);

            DataSet oDataSet = new DataSet();
            suppliersAdapter.Fill(oDataSet, "itemList");
            DataTable dataTable = oDataSet.Tables["itemList"];
            if (dataTable.Rows.Count > 0)
            {
                result = Convert.ToInt32(dataTable.Rows[0]["id"]);
            }
            suppliersAdapter = null;
            oDataSet = null;
            connection.Close();
        }
        return result;
    }
}

