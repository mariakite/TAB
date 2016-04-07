using System;
using System.Collections.Generic;
using System.Data;

namespace infoModels
{
    public class param_name
    {
        public param_name()
        {
        }

        public param_name(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public string name { get; set; }
        public int category_id { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM param_advert_for_export WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["param_name_id"]);
                this.name = Convert.ToString(itemRow["param_name"]).Trim();
                this.category_id = Convert.ToInt32(itemRow["category_id"]);
            }
            else
            {
                this.id = 0;
            }
        }

        public static IList<param_name> getList()
        {
            IList<param_name> result = new List<param_name>();

            string sqlText = "SELECT * FROM param_advert_for_export ORDER BY category_id";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                param_name insertItem = new param_name
                {
                    id = Convert.ToInt32(itemRow["param_name_id"]),
                    name = Convert.ToString(itemRow["param_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"])
                };
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<param_name> getList(int category_id)
        {
            IList<param_name> result = new List<param_name>();

            string sqlText = "SELECT * FROM param_advert_for_export WHERE category_id="+category_id.ToString()+ " ORDER BY param_name";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                param_name insertItem = new param_name
                {
                    id = Convert.ToInt32(itemRow["param_name_id"]),
                    name = Convert.ToString(itemRow["param_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"])
                };
                result.Add(insertItem);
            }
            return result;
        }
    }

    public class param_value
    {
        public param_value()
        {
        }

        public param_value(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int value_id { get; set; }
        public string value { get; set; }
        public int param_name_id { get; set; }
        public string param_name { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM param_value_for_export WHERE param_value_id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.value_id = Convert.ToInt32(itemRow["param_value_id"]);
                this.value = Convert.ToString(itemRow["param_value"]).Trim();
                this.param_name_id = Convert.ToInt32(itemRow["param_name_id"]);
                this.param_name = Convert.ToString(itemRow["param_name"]).Trim();
            }
            else
            {
                this.value_id = 0;
            }
        }

        public static IList<param_value> getList()
        {
            IList<param_value> result = new List<param_value>();

            string sqlText = "SELECT * FROM param_value_for_export ORDER BY param_name_id";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                param_value insertItem = new param_value
                {
                    value_id = Convert.ToInt32(itemRow["param_value_id"]),
                    value = Convert.ToString(itemRow["param_value"]).Trim(),
                    param_name_id = Convert.ToInt32(itemRow["param_name_id"]),
                    param_name = Convert.ToString(itemRow["param_name"]).Trim()
                };
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<param_value> getList(int param_id)
        {
            IList<param_value> result = new List<param_value>();

            string sqlText = "SELECT * FROM param_value_for_export WHERE param_name_id="+param_id.ToString()+ " ORDER BY param_value";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                param_value insertItem = new param_value
                {
                    value_id = Convert.ToInt32(itemRow["param_value_id"]),
                    value = Convert.ToString(itemRow["param_value"]).Trim(),
                    param_name_id = Convert.ToInt32(itemRow["param_name_id"]),
                    param_name = Convert.ToString(itemRow["param_name"]).Trim()
                };
                result.Add(insertItem);
            }
            return result;
        }
    }
}