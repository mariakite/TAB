using System;
using System.Collections.Generic;
using System.Data;

namespace infoModels
{
    public class info_param_name
    {
        public info_param_name()
        {
        }

        public info_param_name(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public string name { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM info_param_name WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.name = Convert.ToString(itemRow["name"]).Trim();
            }
            else
            {
                this.id = 0;
            }
        }

        public static List<info_param_name> getList(string ids)
        {
            List<info_param_name> result = new List<info_param_name>();

            string sqlText = "SELECT * FROM info_param_name WHERE id IN (" + ids+") ORDER BY sort;";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                info_param_name insertItem = new info_param_name
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    name = Convert.ToString(itemRow["name"]).Trim()
                };
                result.Add(insertItem);
            }
            return result;
        }
    }
}