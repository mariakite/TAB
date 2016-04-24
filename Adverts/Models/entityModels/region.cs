using System;
using System.Collections.Generic;
using System.Data;

namespace entityModels
{
    public class region
    {
        public region()
        {
        }

        public region(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public string name { get; set; }
        public string rest_avito_code { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM regions WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.name = Convert.ToString(itemRow["name"]).Trim();
                this.rest_avito_code = Convert.ToString(itemRow["rest_avito_code"]).Trim();
            }
            else
            {
                this.id = 0;
            }
        }

        public static IList<region> getList()
        {
            IList<region> result = new List<region>();

            string sqlText = "SELECT * FROM regions WHERE id IN (77,78) ORDER BY name";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                region insertItem = new region
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    name = Convert.ToString(itemRow["name"]).Trim(),
                    rest_avito_code = Convert.ToString(itemRow["rest_avito_code"]).Trim()
                };
                result.Add(insertItem);
            }
            return result;
        }
    }
}