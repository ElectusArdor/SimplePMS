using Simple_PMS.Views;

namespace Simple_PMS.Services
{
    internal class Message
    {
        public static void Show(string title, string text, bool onTop)
        {
            var window = new MessageBox();
            window.Title = title;
            window.Content = text;
            window.Topmost = onTop;
            window.Show();
        }
    }
}
