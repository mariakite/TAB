namespace Helpers.Html.Models
{
    public class Toastr
    {
        private string p_message;
        private ToastrType p_type;
        private string p_title;

        public string Message
        {
            get
            {
                return p_message;
            }
            set
            {
                p_message = value;
            }
        }

        public ToastrType Type
        {
            get
            {
                return p_type;
            }
            set
            {
                p_type = value;
            }
        }

        public string Title
        {
            get
            {
                return p_title;
            }
            set
            {
                p_title = value;
            }
        }

        public Toastr(string title, string message, ToastrType type)
        {
            Message = message;
            Type = type;
            Title = title;
        }
    }

    public enum ToastrType
    {
        Success,
        Warning,
        Danger,
        Info
    }
}