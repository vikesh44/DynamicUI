using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Pxic.DynamicUI.DTO;
using System.Configuration;

namespace Pxic.DynamicUI.Helper
{
    public class DatabaseHelper
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Updates the data in database using a specific stored procedure
        /// </summary>
        /// <param name="procedureName">Name of the stored procedure to be executed</param>
        /// <param name="parameters"><see cref="List{ProcedureParameter}"/> for the stored procedure</param>
        /// <returns>Returns the number of records updated</returns>
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

        /// <summary>
        /// Gets the first record by executing the stored procedure
        /// </summary>
        /// <typeparam name="T">Datatype in which data to be returned</typeparam>
        /// <param name="procedureName">Name of the stored procedure to be executed</param>
        /// <param name="parameters"><see cref="List{ProcedureParameter}"/> for the stored procedure</param>
        /// <returns>Returns the first record by executing the stored procedure and convert the record in type <typeparamref name="T"/> provided by user</returns>
        public object? GetFirstRecord<T>(string procedureName, List<ProcedureParameter> parameters)
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

        /// <summary>
        /// Gets the list of record by executing the stored procedure
        /// </summary>
        /// <typeparam name="T">Datatype in which data to be returned</typeparam>
        /// <param name="procedureName">Name of the stored procedure to be executed</param>
        /// <param name="parameters"><see cref="List{ProcedureParameter}"/> for the stored procedure</param>
        /// <returns>Returns the list of record by executing the stored procedure and convert the records in type <typeparamref name="T"/></returns>
        /// <exception cref="Exception">
        /// Returns the empty lis in case of any exception
        /// </exception>
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
            ConnectionString = ConfigurationManager.AppSettings["connectionString"] ?? string.Empty;
        }

        private static readonly object objLock = new();

        private static DatabaseHelper? instance = null;
        /// <summary>
        /// Gets the singleton instance of the <see cref="DatabaseHelper"/> class.
        /// </summary>
        /// <remarks>
        /// This property ensures that only one instance of the <see cref="DatabaseHelper"/> class 
        /// is created and provides a globally accessible point to Database operations. 
        /// The implementation is thread-safe, using double-checked locking to ensure 
        /// that the instance is created only once in a multithreaded environment as well.
        /// </remarks>
        /// <returns>
        /// The single instance of the <see cref="DatabaseHelper"/> class.
        /// </returns>
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
