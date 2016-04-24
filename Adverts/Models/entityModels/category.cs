using System;
using System.Collections.Generic;
using System.Data;

namespace entityModels
{
    public class category
    {
        public category()
        {
        }

        public category(int id)
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
            string sqlText = "SELECT * FROM categories WHERE id=" + id + ";";
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

        public static IList<category> getList()
        {
            IList<category> result = new List<category>();

            string sqlText = "SELECT * FROM categories ORDER BY name";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                category insertItem = new category
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    name = Convert.ToString(itemRow["name"]).Trim(),
                    rest_avito_code = Convert.ToString(itemRow["rest_avito_code"]).Trim()
                };
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<category> getListToPeople()
        {
            IList<category> result = new List<category>();

            string sqlText = "SELECT * FROM categories WHERE id IN (1,8,11,24) ORDER BY name";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                category insertItem = new category
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    name = Convert.ToString(itemRow["name"]).Trim(),
                    rest_avito_code = Convert.ToString(itemRow["rest_avito_code"]).Trim()
                };
                if (insertItem.id == constant.str.category_realty_id)
                {
                    insertItem.name = constant.str.category_realty_name;
                }
                result.Add(insertItem);
            }
            return result;
        }
    }
}