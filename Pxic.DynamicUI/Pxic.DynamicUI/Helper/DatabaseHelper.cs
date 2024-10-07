using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Pxic.DynamicUI.DTO;

namespace Pxic.DynamicUI.Helper
{
    public class DatabaseHelper
    {
        public string ConnectionString { get; set; }

        public async Task<int> UpdateData(string procedureName, List<ProcedureParameter> parameters)
        {
            int recordsUpdated = 0;
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(procedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (ProcedureParameter parameter in parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }

                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
                con.Open();
                recordsUpdated = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception)
            {
                recordsUpdated = 0;
            }
            return recordsUpdated;
        }

        public object GetFirstRecord<T>(string procedureName, List<ProcedureParameter> parameters)
        {
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(procedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (ProcedureParameter parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }

                SqlDataAdapter da = new()
                {
                    SelectCommand = cmd
                };

                DataSet ds = new();

                da.Fill(ds);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (ds == null ||
                    ds.Tables == null ||
                    ds.Tables[0] == null ||
                    ds.Tables[0].Rows == null ||
                    ds.Tables[0].Rows.Count <= 0 ||
                    ds.Tables[0].Rows[0][0] == null)
                {
                    return null;
                }
                else
                {
                    return ds.Tables[0].Rows[0][0] is T data
                        ? data
                        : (T)Convert.ChangeType(ds.Tables[0].Rows[0][0], typeof(T));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<T> GetData<T>(string procedureName, List<ProcedureParameter> parameters)
        {
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(procedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (ProcedureParameter parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }

                SqlDataAdapter da = new()
                {
                    SelectCommand = cmd
                };

                DataSet ds = new();

                da.Fill(ds);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (ds == null ||
                    ds.Tables == null ||
                    ds.Tables[0] == null)
                {
                    return [];
                }
                else
                {
                    return ConvertDataTable<T>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                return [];
            }
        }

        private List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = [];
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] is not DBNull)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }

        #region Singleton Instance Implementation

        private DatabaseHelper()
        {
            ConnectionString = "Server=tcp:dynamicuisrv.database.windows.net,1433;Initial Catalog=DynamicUIDB;" +
                               "Persist Security Info=False;User ID=vikesh;Password=Niya@123;MultipleActiveResultSets=False;" +
                               "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        private static readonly object objLock = new();

        private static DatabaseHelper? instance = null;
        public static DatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        instance ??= new DatabaseHelper();
                    }
                }
                return instance;
            }
        }

        #endregion
    }

    
}
