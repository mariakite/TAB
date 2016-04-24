using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace paymentModels
{
    public class order
    {
        public order()
        {
        }

        public order(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public int region_id { get; set; }
        public int category_id { get; set; }
        public string hash_key { get; set; }
        public string adverts { get; set; }
        public int count { get; set; }
        public double sum { get; set; }
        public int user_id { get; set; }
        public DateTime time_pay { get; set; }
        public int status { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM orders WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.region_id = Convert.ToInt32(itemRow["region_id"]);
                this.category_id = Convert.ToInt32(itemRow["category_id"]);
                this.user_id = Convert.ToInt32(itemRow["user_id"]);
                this.count = Convert.ToInt32(itemRow["count"]);
                this.status = Convert.ToInt32(itemRow["status"]);

                this.hash_key = Convert.ToString(itemRow["hash_key"]).Trim();
                this.adverts = Convert.ToString(itemRow["adverts"]).Trim();
                this.sum = Convert.ToDouble(itemRow["sum"]);

                this.time_pay = Convert.ToDateTime(itemRow["time_pay"]);
            }
        }     

        public static order getOrder (string hash_key)
        {
            order result = new order();
            string sqlText = "SELECT * FROM orders WHERE hash_key='" + hash_key + "';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                result.id = Convert.ToInt32(itemRow["id"]);
                result.region_id = Convert.ToInt32(itemRow["region_id"]);
                result.category_id = Convert.ToInt32(itemRow["category_id"]);
                result.user_id = Convert.ToInt32(itemRow["user_id"]);
                result.count = Convert.ToInt32(itemRow["count"]);
                result.status = Convert.ToInt32(itemRow["status"]);
                result.hash_key = Convert.ToString(itemRow["hash_key"]).Trim();
                result.adverts = Convert.ToString(itemRow["adverts"]).Trim();
                result.sum = Convert.ToDouble(itemRow["sum"]);
                result.time_pay = Convert.ToDateTime(itemRow["time_pay"]);
            }
            return result;
        }

        public static int createOrder (string hash_key, int count, string sum, string adverts)
        {
            int result = 0;
            sum = sum.Replace(",",".");
            try
            {
                string sqlText = "INSERT INTO orders (hash_key,count,adverts,sum)VALUES('" + hash_key + "'," + count.ToString() + ",'" + adverts + "','" + sum + "');";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (int)constant.status.ok;
            }
            catch
            {
                result = (int)constant.status.error;
            }
            
            return result;
        }

        public static int createOrder(string hash_key, int count, string sum, int region_id, int category_id, int user_id)
        {
            int result = 0;
            sum = sum.Replace(",", ".");
            try
            {
                string sqlText = "INSERT INTO orders(hash_key, category_id, region_id, count, sum, user_id) VALUES ('"+hash_key+"', "+category_id.ToString()+", "+region_id.ToString()+", "+count.ToString()+", '"+sum+"', "+user_id.ToString()+");";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (int)constant.status.ok;
            }
            catch
            {
                result = (int)constant.status.error;
            }

            return result;
        }

        public static int updateOrder(string hash_key, string operation_id, string sum)
        {
            int result = 0;
            //communicate.sentMail("maros428@gmail.com","label="+hash_key+ " operation_id=" + operation_id+" sum="+sum,"Уведомление о платеже");
            string sqlText = "UPDATE orders SET status="+((int)constant.status_payment.pay).ToString() + ", operation_id='"+ operation_id+"' WHERE hash_key='"+ hash_key + "';";
            sqlData.sqlQueryExecute("data-postresql", sqlText);

            //заносим рассылку в разрешенные или добавляем объвления в сессию
            order order = order.getOrder(hash_key);
            if (order.id > 0)
            {
                if (order.category_id > 0)
                {
                    sqlText = "INSERT INTO delivery_access(time_start, time_end, order_id, category_id, region_id, user_id, status)" +
                              "VALUES('" + DateTime.Now.ToString() + "', '" + DateTime.Now.AddDays(order.count).ToString() + "', " + order.id.ToString() + ", " + order.category_id.ToString() + ", " + order.region_id.ToString() + ", " + order.user_id.ToString() + ", " + ((int)constant.status_payment.pay).ToString() + ");" +
                              "UPDATE delivers SET status="+((int)constant.status_delivery.payment).ToString()+" WHERE user_id="+order.user_id.ToString()+ " AND category_id=" + order.category_id.ToString() + " AND status="+((int)constant.status_delivery.create).ToString()+";";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                }
                //else if (order.adverts != String.Empty)
                //{
                //    string adverts = HttpContext.Current.Session["adverts"] != null ? HttpContext.Current.Session["adverts"].ToString() + "," + order.adverts : order.adverts;
                //    HttpContext.Current.Session["adverts"] = adverts;
                //}
            }
            return result;
        }
    }
}