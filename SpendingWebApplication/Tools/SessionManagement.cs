using Newtonsoft.Json;
using SpendingWebApplication.Models;
using System.Web;

namespace SpendingWebApplication.Tools
{
    public static class SessionManagement
    {
        public static void DeleteSession(HttpContext context)
        {
            context.Session.Remove("SpendingWebApplication.Session");
            context.Session.Clear();
        }

        public static void CreateSession(HttpContext context, UsuarioModel usuario)
        {
            context.Session.SetString("SpendingWebApplication.Session", JsonConvert.SerializeObject(usuario));
        }

        public static UsuarioModel GetSession(HttpContext context)
        {
            try
			{
				var accountString = context.Session.GetString("SpendingWebApplication.Session");

				if (string.IsNullOrEmpty(accountString))
				{
					return null;
				}

				return JsonConvert.DeserializeObject<UsuarioModel>(accountString);
			}
			catch
			{
				return null;
			}
        }
    }
}
