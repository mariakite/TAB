using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace entityModels
{
    public class delivery
    {
        public delivery()
        {
        }

        public delivery(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public int user_id { get; set; }
        public string email { get; set; }
        public int region_id { get; set; }
        public string region_name { get; set; }
        public string category_name { get; set; }
        public int category_id { get; set; }
        public string advert_param_ids { get; set; }
        public string advert_value_ids { get; set; }

        public usersModels.user user { get; set; }
        public List<infoModels.info_param_name> param_names { get; set; }
        public List<List<infoModels.info_param_value>> param_values { get; set; }

        public DateTime last_sent { get; set; }
        public DateTime start_delivery { get; set; }
        public DateTime end_delivery { get; set; }
        public int status { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM delivers WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.email = Convert.ToString(itemRow["email"]).Trim();
                this.region_id = Convert.ToInt32(itemRow["region_id"]);
                this.category_id = Convert.ToInt32(itemRow["category_id"]);
                this.advert_param_ids = Convert.ToString(itemRow["advert_param_ids"]).Trim();
                this.advert_value_ids = Convert.ToString(itemRow["advert_value_ids"]).Trim();
                this.last_sent = Convert.ToDateTime(itemRow["last_sent"]);
                this.status = Convert.ToInt32(itemRow["status"]);
            }
            else
            {
                this.id = 0;
            }
        }

        public static int saveDelivery(string email, int region_id, int category_id, string param_ids, string value_ids)
        {
            int result = 0;
            int user_id = 0;
            //проверяем пользователя
            if (Convert.ToBoolean(HttpContext.Current.Session["is_auth"]))
            {
                user_id = usersModels.user.getIdFromHash(Convert.ToString(HttpContext.Current.Session["hash_key"]));
            }
            else
            {
                result = usersModels.user.saveUser(email);
                if (result == (int)constant.status_registrate.success)
                {
                    user_id = usersModels.user.getIdFromEmail(email);
                    result = (Int32)constant.status_registrate.create_user;
                }
            }
            if (user_id != 0)
            {
                try
                {
                    string sqlText = "INSERT INTO delivers(" +
                                 "user_id, email, region_id, category_id, advert_param_ids, advert_value_ids, last_sent, status)" +
                                 "VALUES("+user_id.ToString()+",'" + email + "', " + region_id.ToString() + ", " + category_id.ToString() + ", '" + param_ids + "', '" + value_ids + "', '" + DateTime.Now.ToString() + "', " + (Int32)constant.status_delivery.create + ");";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                    if (result == 0)
                    {
                        result = (Int32)constant.status.ok;
                    }
                }
                catch (Exception e)
                {
                    result = (Int32)constant.status.error;
                }
            }
            return result;
        }

        public static IList<delivery> getList()
        {
            IList<delivery> result = new List<delivery>();

            string sqlText = "SELECT *,regions.name AS region_name, categories.name AS category_name FROM delivers INNER JOIN regions ON delivers.region_id=regions.id INNER JOIN categories ON delivers.category_id=categories.id ORDER BY email;";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                delivery insertItem = new delivery
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    email = Convert.ToString(itemRow["email"]).Trim(),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = ((string)itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = ((string)itemRow["category_name"]).Trim(),
                    advert_param_ids = Convert.ToString(itemRow["advert_param_ids"]).Trim(),
                    advert_value_ids = Convert.ToString(itemRow["advert_value_ids"]).Trim(),
                    last_sent = Convert.ToDateTime(itemRow["last_sent"]),
                    status = Convert.ToInt32(itemRow["status"]),
                    param_names = infoModels.info_param_name.getList(Convert.ToString(itemRow["advert_param_ids"]).Trim()),
                    //param_values = infoModels.info_param_value.getList(Convert.ToString(itemRow["advert_value_ids"]).Trim().Replace(";",",").Replace("_",","))
                };
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<delivery> getList(string hash_key)
        {
            IList<delivery> result = new List<delivery>();

            string sqlText = "SELECT *,regions.name AS region_name, categories.name AS category_name FROM delivers INNER JOIN regions ON delivers.region_id=regions.id INNER JOIN categories ON delivers.category_id=categories.id INNER JOIN users ON users.id=delivers.user_id WHERE users.hash_key='"+hash_key+ "' ORDER BY start_delivery DESC;";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                delivery insertItem = new delivery
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    email = Convert.ToString(itemRow["email"]).Trim(),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = ((string)itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = ((string)itemRow["category_name"]).Trim(),
                    advert_param_ids = Convert.ToString(itemRow["advert_param_ids"]).Trim(),
                    advert_value_ids = Convert.ToString(itemRow["advert_value_ids"]).Trim(),
                    last_sent = Convert.ToDateTime(itemRow["last_sent"]),
                    status = Convert.ToInt32(itemRow["status"]),
                    param_names = infoModels.info_param_name.getList(Convert.ToString(itemRow["advert_param_ids"]).Trim()),
                    param_values = new List<List<infoModels.info_param_value>>()
                };

                string[] values = Convert.ToString(itemRow["advert_value_ids"]).Trim().Split(';');
                foreach(string value in values)
                {
                    insertItem.param_values.Add(infoModels.info_param_value.getList(value));
                }
                if (insertItem.category_id == constant.str.category_realty_id)
                {
                    insertItem.category_name = constant.str.category_realty_name;
                }
                result.Add(insertItem);
            }
            return result;
        }

        public static IList<delivery> getList(int user_id)
        {
            IList<delivery> result = new List<delivery>();

            string sqlText = "SELECT *,regions.name AS region_name, categories.name AS category_name FROM delivers INNER JOIN regions ON delivers.region_id=regions.id INNER JOIN categories ON delivers.category_id=categories.id WHERE user_id=" + user_id.ToString() + " ORDER BY start_delivery DESC;";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                delivery insertItem = new delivery
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    email = Convert.ToString(itemRow["email"]).Trim(),
                    region_id = Convert.ToInt32(itemRow["region_id"]),
                    region_name = ((string)itemRow["region_name"]).Trim(),
                    category_id = Convert.ToInt32(itemRow["category_id"]),
                    category_name = ((string)itemRow["category_name"]).Trim(),
                    advert_param_ids = Convert.ToString(itemRow["advert_param_ids"]).Trim(),
                    advert_value_ids = Convert.ToString(itemRow["advert_value_ids"]).Trim(),
                    last_sent = Convert.ToDateTime(itemRow["last_sent"]),
                    status = Convert.ToInt32(itemRow["status"]),
                    param_names = infoModels.info_param_name.getList(Convert.ToString(itemRow["advert_param_ids"]).Trim()),
                    param_values = new List<List<infoModels.info_param_value>>()
                };

                string[] values = Convert.ToString(itemRow["advert_value_ids"]).Trim().Split(';');
                foreach (string value in values)
                {
                    insertItem.param_values.Add(infoModels.info_param_value.getList(value));
                }
                if (insertItem.category_id == constant.str.category_realty_id)
                {
                    insertItem.category_name = constant.str.category_realty_name;
                }
                result.Add(insertItem);
            }
            return result;
        }

        public static int updateStatusDelivery(int delivery_id, int status)
        {
            int result = 0;
            try
            {
                string sqlText = "UPDATE delivers SET status=" + status.ToString() + " WHERE id=" + delivery_id.ToString() + ";";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (Int32)constant.status.ok;
            }
            catch(Exception e)
            {
                result = (Int32)constant.status.error;
            }           
            return result;
        }

        public static int updateStatusDelivery(string hash_key, int delivery_id, int status)
        {
            int result = 0;
            try
            {
                int user_id = usersModels.user.getIdFromHash(hash_key);
                string sqlText = "UPDATE delivers SET status=" + status.ToString() + " WHERE id=" + delivery_id.ToString() + " AND user_id="+user_id.ToString()+";";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (Int32)constant.status.ok;
            }
            catch (Exception e)
            {
                result = (Int32)constant.status.error;
            }
            return result;
        }

        public static int deleteDelivery(int delivery_id)
        {
            int result = 0;
            try
            {
                string sqlText = "DELETE FROM delivers WHERE id=" + delivery_id.ToString() + ";";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (Int32)constant.status.ok;
            }
            catch (Exception e)
            {
                result = (Int32)constant.status.error;
            }
            return result;
        }

        public static int deleteDelivery(string hash_key, int delivery_id)
        {
            int result = 0;
            try
            {
                int user_id = usersModels.user.getIdFromHash(hash_key);
                string sqlText = "DELETE FROM delivers WHERE id=" + delivery_id.ToString() + " AND user_id="+user_id.ToString()+";";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                result = (Int32)constant.status.ok;
            }
            catch (Exception e)
            {
                result = (Int32)constant.status.error;
            }
            return result;
        }
    }
}