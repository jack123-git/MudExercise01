using Dapper;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MudExerciseLib.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// 轉換TableValuedParameter靜態方法
        /// </summary>
        /// <typeparam name="T">基礎型別</typeparam>
        /// <param name="records">基礎型別清單</param>
        /// <returns>SqlMapper.ICustomQueryParameter</returns>
        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(this List<T> records)
        {
            if (records.Count == 0)
                return null;
            else
            {
                var dt = new DataTable();
                Type type = typeof(T);
                dt.Columns.Add("Value", type);

                foreach (var item in records)
                {
                    DataRow row = dt.NewRow();
                    row["Value"] = item;
                    dt.Rows.Add(row);
                }

                // 判斷是否為Guid String
                bool isGuidString = false;
                if (type == typeof(string) && records.Count > 0)
                {
                    isGuidString = records[0].ToString().IsGuidString();
                }

                string udtName = "dbo.udt_String";
                if (isGuidString)
                    udtName = "dbo.udt_UniqueIdentifier";
                else if (type == typeof(string))
                    udtName = "dbo.udt_String";
                else if (type == typeof(int))
                    udtName = "dbo.udt_Int";
                else if (type == typeof(bool))
                    udtName = "dbo.udt_Bit";
                else if (type == typeof(DateTime))
                    udtName = "dbo.udt_DateTime";
                else if (type == typeof(DateTimeOffset))
                    udtName = "dbo.udt_DateTimeOffset";
                else if (type == typeof(decimal))
                    udtName = "dbo.udt_Decimal";
                else if (type == typeof(long))
                    udtName = "dbo.udt_BigInt";
                return dt.AsTableValuedParameter(udtName);
            }
        }

        public static DataTable ListConvertDataTable<T>(this List<T> model, string TableName) where T : class
        {
            try
            {
                Type value = typeof(T);
                var list = new List<PropertyInfo>(value.GetProperties()); //屬性清單
                var table = new DataTable();//產生實體datatable
                table.TableName = TableName; //表名
                foreach (var property in list)
                {
                    //獲取屬性資料類型
                    Type PropertyType = property.PropertyType;
                    //驗證資料類型是否為空
                    if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        PropertyType = property.PropertyType.GetGenericArguments()[0];
                    }
                    table.Columns.Add(property.Name, PropertyType);
                }
                foreach (var dataValue in model)
                {
                    DataRow row = table.NewRow();
                    list.ForEach(p => { row[p.Name] = p.GetValue(dataValue); });
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// String 擴充方法類別
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 判斷是否為Guid String
        /// </summary>
        /// <param name="guidString"></param>
        /// <returns>true/false</returns>
        public static bool IsGuidString(this string guidString)
        {
            Match match = Regex.Match(guidString, @"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$|^[0-9a-f]{32}$ ", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));
            return match.Success;
        }
    }

}
