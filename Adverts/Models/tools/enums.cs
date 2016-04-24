namespace constant
{
    public class str
    {
        public const string name_project = "TABAVI";
        public const string email = "info@tabavi.ru";
        public const string email_password = "I23t04/16";
        public const string smtp = "smtp.yandex.ru";
        public const int smtp_port = 587;
        public const int region_spb_id = 78;
        public const int category_realty_id = 8;
        public const string category_realty_name = "Недвижимость";
        public const int startYear = 2016;

        public const int delivery_3 = 500;
        public const int delivery_7 = 1000;
        public const int delivery_15 = 1500;
        public const int delivery_30 = 2000;
        public const string source = "https://www.avito.ru/";
    }
    

    public enum status : int
    {
        @default = 0,
        error = -1000,
        ok = 1000
    }

    public enum status_user : int
    {
        active = 0,
        block = -100,
        error = -1000
    }

    public enum status_registrate : int
    {
        success = 1000,
        create_user = 500,
        set_password = 100,
        already_insert = -100,
        incorrect_password = -200,
        error = -1000
    }

    public enum status_delivery : int
    {
        create = 0,
        payment = 500,
        cancel = -500,
        complete = 1000
    }

    public enum status_payment : int
    {
        not_pay = 0,
        pay = 1000
    }
}

