using System;
using System.Collections.Generic;
using System.Data;

namespace paymentModels
{
    public class delivery_access
    {
        public delivery_access()
        {
        }

        public delivery_access(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public int region_id { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public int order_id { get; set; }
        public int user_id { get; set; }
        public DateTime time_start { get; set; }
        public DateTime time_end { get; set; }
        public int status { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT *,categories.name AS category_name FROM delivery_access INNER JOIN categories ON categories.id=delivery_access.category_id WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.region_id = Convert.ToInt32(itemRow["region_id"]);
                this.category_id = Convert.ToInt32(itemRow["category_id"]);
                this.category_name = Convert.ToString(itemRow["category_id"]).Trim();
                this.user_id = Convert.ToInt32(itemRow["user_id"]);
                this.order_id = Convert.ToInt32(itemRow["order_id"]);
                this.time_start = Convert.ToDateTime(itemRow["time_start"]);
                this.time_end = Convert.ToDateTime(itemRow["time_end"]);
                this.status = Convert.ToInt32(itemRow["status"]);
            }
        }     

        public static List<delivery_access> getActualAccessForUser(int user_id)
        {
            List<delivery_access> result = new List<delivery_access>();
            string sqlText = "SELECT *,categories.name AS category_name FROM delivery_access INNER JOIN categories ON categories.id=delivery_access.category_id WHERE user_id=" + user_id.ToString() + " AND status="+((int)constant.status_payment.pay).ToString()+";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                delivery_access insertItem = new delivery_access
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = Convert.ToString(itemRow["category_id"]).Trim(),
                    user_id = Convert.ToInt32(itemRow["user_id"]),
                    order_id = Convert.ToInt32(itemRow["order_id"]),
                    time_start = Convert.ToDateTime(itemRow["time_start"]),
                    time_end = Convert.ToDateTime(itemRow["time_end"]),
                    status = Convert.ToInt32(itemRow["status"])
                };
                result.Add(insertItem);
            }
            return result;
        }
    }
}