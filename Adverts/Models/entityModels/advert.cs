using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace entityModels
{
    public class advert
    {
        public advert()
        {
        }

        public advert(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public int rest_id { get; set; }
        public int region_id { get; set; }
        public string region_name { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string[] images { get; set; }
        public string[] @params { get; set; }
        public DateTime time_advert { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM adverts_for_site WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.rest_id = Convert.ToInt32(itemRow["rest_id"]);
                this.region_id = Convert.ToInt32(itemRow["region_id"]);
                this.region_name = Convert.ToString(itemRow["region_name"]).Trim();
                this.category_id = Convert.ToInt32(itemRow["category_id"]);
                this.category_name = Convert.ToString(itemRow["category_name"]).Trim();
                this.name = Convert.ToString(itemRow["user_name"]).Trim();
                this.phone = Convert.ToString(itemRow["phone"]).Trim();
                this.price = Convert.ToDouble(itemRow["price"]);
                this.title = Convert.ToString(itemRow["title"]).Trim();
                this.description = Convert.ToString(itemRow["description"]).Trim();
                this.url = constant.str.source+Convert.ToString(itemRow["url"]).Trim();
                this.images = Convert.ToString(itemRow["images"]).Trim().Split(',');
                this.time_advert = Convert.ToDateTime(itemRow["time_advert"]);
            }
            else
            {
                this.id = 0;
            }
        }     

        public static string getPaymentAdvertIds()
        {
            string result = "0";
            string ids = HttpContext.Current.Session["hash_key_order"] != null ? HttpContext.Current.Session["hash_key_order"].ToString() : "0";
            ids = ids.Replace(",","','");
            string sqlText = "SELECT adverts FROM orders WHERE hash_key IN ('"+ids+"') AND status="+((int)constant.status_payment.pay).ToString()+";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                result = result + "," + Convert.ToString(itemRow["adverts"]).Trim();
            }
            return result;
        }
        public static IList<advert> getList(int region_id, int category_id)
        {
            IList<advert> result = new List<advert>();
            string category_str = category_id.ToString();
            if (category_id == (int)constant.str.category_realty_id)
            {
                category_str = "8,9";
            }

            string sqlText = "SELECT * FROM adverts_for_site WHERE region_id="+region_id.ToString()+" AND category_id IN (" + category_str + ") ORDER BY time_advert DESC LIMIT 50;";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                advert insertItem = new advert
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = Convert.ToString(itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = Convert.ToString(itemRow["category_name"]).Trim(),
                    name = Convert.ToString(itemRow["user_name"]).Trim(),
                    phone = Convert.ToString(itemRow["phone"]).Trim(),
                    price = Convert.ToDouble(itemRow["price"]),
                    title = Convert.ToString(itemRow["title"]).Trim(),
                    description = Convert.ToString(itemRow["description"]).Trim(),
                    url = constant.str.source + Convert.ToString(itemRow["url"]).Trim(),
                    images = Convert.ToString(itemRow["images"]).Trim().Split(','),
                    time_advert = Convert.ToDateTime(itemRow["time_advert"])
                };
                
            }
            return result;
        }

        public static IList<advert> getList(string adverts)
        {
            IList<advert> result = new List<advert>();
            string sqlText = "SELECT * FROM adverts_for_site WHERE id IN (" + adverts + ") ORDER BY time_advert;";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                advert insertItem = new advert
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = Convert.ToString(itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = Convert.ToString(itemRow["category_name"]).Trim(),
                    name = Convert.ToString(itemRow["user_name"]).Trim(),
                    phone = Convert.ToString(itemRow["phone"]).Trim(),
                    price = Convert.ToDouble(itemRow["price"]),
                    title = Convert.ToString(itemRow["title"]).Trim(),
                    description = Convert.ToString(itemRow["description"]).Trim(),
                    url = constant.str.source + Convert.ToString(itemRow["url"]).Trim(),
                    images = Convert.ToString(itemRow["images"]).Trim().Split(','),
                    time_advert = Convert.ToDateTime(itemRow["time_advert"])
                };
                var parameters = new List<string>();

                string sqlParamText = "SELECT * FROM adverts_param_for_export WHERE rest_id=" + insertItem.rest_id.ToString() + " AND param_id <=10 ORDER BY param_sort;";
                DataTable itemTableParam = sqlData.sqlQueryFill("data-postresql", sqlParamText);
                foreach (DataRow itemRowParam in itemTableParam.Rows)
                {
                    string insertParam = Convert.ToString(itemRowParam["param_name"]).Trim() + ": " + Convert.ToString(itemRowParam["param_value"]).Trim();
                    if (Convert.ToString(itemRowParam["param_value"]).Trim().Length > 0)
                    {
                        parameters.Add(insertParam);
                    }
                }
                insertItem.@params = parameters.ToArray();
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<advert> getList(int region_id, int category_id, string param_ids, string value_ids, bool free)
        {
            IList<advert> result = new List<advert>();
            int n = 0;
            string category_str = category_id.ToString();
            if (category_id == (int)constant.str.category_realty_id)
            {
                category_str = "8,9";
            }
            string advs = getPaymentAdvertIds();
            IList <string> adverts = new List<string>(advs.Split(','));

            string s_where = "";
            string[] param = param_ids.Split(',');
            string[] value = value_ids.Split(';');

            if (value_ids != "")
            {
                for (int i = 0; i < value.Length; i++)
                {
                    s_where = s_where + " AND rest_id IN (SELECT rest_id FROM adverts_param WHERE param_id=" + param[i] + " AND value_id IN (" + value[i] + "))";
                }
            }

            if (free)
            {
                s_where = s_where + " AND time_advert<'"+DateTime.Now.AddMinutes(-31)+"' ";
            }
            string sqlText = "SELECT * FROM adverts_for_site WHERE region_id=" + region_id + " AND category_id IN(" + category_str + ") " + s_where + " ORDER BY time_advert DESC LIMIT 50;";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                n++;
                advert insertItem = new advert
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    rest_id = Convert.ToInt32(itemRow["rest_id"]),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = Convert.ToString(itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = Convert.ToString(itemRow["category_name"]).Trim(),
                    name = Convert.ToString(itemRow["user_name"]).Trim(),
                    phone = Convert.ToString(itemRow["phone"]).Trim(),
                    price = Convert.ToDouble(itemRow["price"]),
                    title = Convert.ToString(itemRow["title"]).Trim(),
                    description = Convert.ToString(itemRow["description"]).Trim(),
                    url = constant.str.source + Convert.ToString(itemRow["url"]).Trim(),
                    images = Convert.ToString(itemRow["images"]).Trim().Split(','),
                    time_advert = Convert.ToDateTime(itemRow["time_advert"])
                };

                if (!free && n>2 && adverts.IndexOf(insertItem.id.ToString())<0)
                {
                    insertItem.phone = "";
                }

                var parameters = new List<string>();
                
                string sqlParamText = "SELECT * FROM adverts_param_for_export WHERE rest_id=" + insertItem.rest_id.ToString() + " AND param_id <=10 ORDER BY param_sort;";
                DataTable itemTableParam = sqlData.sqlQueryFill("data-postresql", sqlParamText);
                foreach (DataRow itemRowParam in itemTableParam.Rows)
                {
                    string insertParam = Convert.ToString(itemRowParam["param_name"]).Trim()+": "+ Convert.ToString(itemRowParam["param_value"]).Trim();
                    if (Convert.ToString(itemRowParam["param_value"]).Trim().Length > 0)
                    {
                        parameters.Add(insertParam);
                    }
                }
                insertItem.@params = parameters.ToArray();
                result.Add(insertItem);
            }
            return result;
        }
    }
}