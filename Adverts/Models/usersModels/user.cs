using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace usersModels
{
    public class user
    {
        public user()
        {
        }

        public user(int id)
        {
            this.getElement(id);
        }

        #region properties
        public int id { get; set; }
        public string hash_key { get; set; }
        public string email { get; set; }
        public bool is_admin { get; set; }
        public int status { get; set; }
        #endregion

        private void getElement(int id)
        {
            string sqlText = "SELECT * FROM users WHERE id=" + id + ";";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                this.id = Convert.ToInt32(itemRow["id"]);
                this.email = Convert.ToString(itemRow["email"]).Trim();
                this.hash_key = Convert.ToString(itemRow["hash_key"]).Trim();
                this.status = Convert.ToInt32(itemRow["status"]);
                this.is_admin = Convert.ToBoolean(itemRow["is_admin"]);
            }
            else
            {
                this.status = (int)constant.status_user.error;
            }
        }

        public static IList<user> getList()
        {
            IList<user> result = new List<user>();

            string sqlText = "SELECT * FROM users ORDER BY email";

            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            foreach (DataRow itemRow in itemTable.Rows)
            {
                user insertItem = new user
                {
                    id = Convert.ToInt32(itemRow["id"]),
                    email = Convert.ToString(itemRow["email"]).Trim(),
                    hash_key = Convert.ToString(itemRow["hash_key"]).Trim(),
                    status = Convert.ToInt32(itemRow["status"]),
                    is_admin = Convert.ToBoolean(itemRow["is_admin"])
                };
                result.Add(insertItem);
            }
            return result;
        }

        public static user getUser(string hash_key)
        {
            user result = new user();
            string sqlText = "SELECT * FROM users WHERE hash_key='" + hash_key + "';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                result.id = Convert.ToInt32(itemRow["id"]);
                result.email = Convert.ToString(itemRow["email"]).Trim();
                result.hash_key = Convert.ToString(itemRow["hash_key"]).Trim();
                result.status = Convert.ToInt32(itemRow["status"]);
                result.is_admin = Convert.ToBoolean(itemRow["is_admin"]);
            }
            return result;
        }

        public static int getIdFromHash(string hash_key)
        {
            int result = 0;
            string sqlText = "SELECT id FROM users WHERE hash_key='" + hash_key + "';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                result = Convert.ToInt32(itemRow["id"]);
            }
            return result;
        }

        public static int getIdFromEmail(string email)
        {
            int result = 0;
            string sqlText = "SELECT id FROM users WHERE email='" + email + "';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                result = Convert.ToInt32(itemRow["id"]);
            }
            return result;
        }

        public static int authorization(string email, string password)
        {
            int result = 0;
            string hash_password = identity.getMD5Hash(password);
            string sqlText = "SELECT id, hash_key, is_admin FROM users WHERE email='"+email+"' AND password='"+hash_password+"';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                HttpContext.Current.Session["hash_key"] = Convert.ToString(itemRow["hash_key"]).Trim();
                HttpContext.Current.Session["is_admin"] = Convert.ToBoolean(itemRow["is_admin"]);
                HttpContext.Current.Session["is_auth"] = true;
                result = (int)constant.status.ok;
            }
            else
            {
                HttpContext.Current.Session["hash_key"] = "";
                HttpContext.Current.Session["is_admin"] = false;
                HttpContext.Current.Session["is_auth"] = false;
                result = (int)constant.status.error;
            }
            return result;
        }

        public static int saveUser(string email)
        {
            int result = 0;
            try
            {
                string sqlText = "SELECT id FROM users WHERE email='" + email + "'";
                DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
                if (itemTable.Rows.Count > 0)
                {
                    result = (int)constant.status_registrate.already_insert;
                }
                else
                {
                    string password = identity.generatePassword();
                    string hash_key = identity.getHashKey();
                    string hash_password = identity.getMD5Hash(password);
                    sqlText = "INSERT INTO users(hash_key, email, password) VALUES ('" + hash_key + "', '" + email + "', '" + hash_password + "');";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                    string message = "Вы успешно зарегистрированы на сайте tabavi.ru<br />" +
                                     "Ваш пароль: " + password + "<br/>" +
                                     "Страница авторизации находится по адресу: http://tabavi.ru/account/login/ <br/><br/>" +
                                     "Если вы не регистрировались на нашем сайте, то просто проигнорируйте это сообщение.<br/><br/>" +
                                     "Спасибо, что воспользовались нашим сервисом. Надеемся, что он окажется для вас полезен.<br/>" +
                                     "Команда tabavi.ru";
                    communicate.sentMail(email, message, "Регистрация на сайте tabavi.ru", true);
                    result = (int)constant.status_registrate.success;
                }
            }
            catch (Exception e)
            {
                result = (int)constant.status_registrate.error;
            }
            return result;
        }

        public static int saveUser(string email, string password)
        {
            int result = 0;
            try
            {
                string sqlText = "SELECT id FROM users WHERE email='" + email + "'";
                DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
                if (itemTable.Rows.Count > 0)
                {
                    result = (int)constant.status_registrate.already_insert;
                }
                else
                {
                    string hash_key = identity.getHashKey();
                    string hash_password = identity.getMD5Hash(password);
                    sqlText = "INSERT INTO users(hash_key, email, password) VALUES ('" + hash_key + "', '" + email + "', '" + hash_password + "');";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                    string message = "Вы успешно зарегистрированы на сайте tabavi.ru<br />" +
                                     "Ваш пароль: "+ password + "<br/>" +
                                     "Страница авторизации находится по адресу: http://tabavi.ru/account/login/ <br/><br/>" +
                                     "Если вы не регистрировались на нашем сайте, то просто проигнорируйте это сообщение.<br/><br/>" +
                                     "Спасибо, что воспользовались нашим сервисом. Надеемся, что он окажется для вас полезен.<br/>" +
                                     "Команда tabavi.ru";
                    communicate.sentMail(email, message, "Регистрация на сайте tabavi.ru", true);
                    authorization(email, password);
                    result = (int)constant.status_registrate.success;
                }
            }
            catch(Exception e)
            {
                result = (int)constant.status_registrate.error;
            }
            return result;
        }

        public static int updateEmail(string hash_key,string email)
        {
            int result = 0;
            try
            {
                string sqlText = "SELECT id FROM users WHERE email='" + email + "'";
                DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
                if (itemTable.Rows.Count > 0)
                {
                    result = (int)constant.status_registrate.already_insert;
                }
                else
                {
                    sqlText = "UPDATE users SET email='"+email+"' WHERE hash_key='"+hash_key+"';";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                    result = (int)constant.status_registrate.success;
                }
            }
            catch (Exception e)
            {
                result = (int)constant.status_registrate.error;
            }
            return result;
        }

        public static int updatePassword(string hash_key, string old_password, string new_password)
        {
            int result = 0;
            try
            {
                string hash_old_password = identity.getMD5Hash(old_password);
                string sqlText = "SELECT id FROM users WHERE hash_key='" + hash_key + "' AND password='"+ hash_old_password + "';";
                DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
                if (itemTable.Rows.Count > 0)
                {
                    string hash_new_password = identity.getMD5Hash(new_password);
                    sqlText = "UPDATE users SET password='" + hash_new_password + "' WHERE hash_key='" + hash_key + "';";
                    sqlData.sqlQueryExecute("data-postresql", sqlText);
                    result = (int)constant.status_registrate.success;
                }
                else
                {
                    result = (int)constant.status_registrate.incorrect_password;
                }
            }
            catch (Exception e)
            {
                result = (int)constant.status_registrate.error;
            }
            return result;
        }

        public static int blockUser(int id)
        {
            int result = 0;
            return result;
        }

        public static int setPassword(string email)
        {
            int result = 0;
            string sqlText = "SELECT id FROM users WHERE email='" + email + "';";
            DataTable itemTable = sqlData.sqlQueryFill("data-postresql", sqlText);
            if (itemTable.Rows.Count > 0)
            {
                DataRow itemRow = itemTable.Rows[0];
                int id = Convert.ToInt32(itemRow["id"]);
                string password = identity.generatePassword();
                string hash_password = identity.getMD5Hash(password);
                sqlText = "UPDATE users SET password='"+hash_password+"' WHERE id="+id.ToString()+";";
                sqlData.sqlQueryExecute("data-postresql", sqlText);
                string message = "Вами был закан сброс пароля.<br />" +
                                     "Ваш новый пароль: " + password + "<br/>" +
                                     "Страница авторизации находится по адресу: http://tabavi.ru/account/login/ <br/><br/>" +
                                     "Спасибо, что пользуетесь нашим сервисом.<br/>" +
                                     "Команда tabavi.ru";
                communicate.sentMail(email, message, "Сброс пароля", false);
                result = (int)constant.status_registrate.set_password;
            }
            else
            {
                result = (int)constant.status_registrate.error;
            }
            return result;
        }
    }
}