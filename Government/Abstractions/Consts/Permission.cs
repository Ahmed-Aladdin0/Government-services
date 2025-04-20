using System.Reflection;

namespace SurvayBasket.Abstractions.Consts.cs
{
    public static  class Permissions
    {
        public static string Type { get; } = "permissions"; //Permissions

        public const string MangeAccount = "Account_Mangment"; 
  
        public const string create_response = "admin.create_response";

        public const string auth_login = "auth.admin.login";
        public const string register = "auth.admin.register";
        public const string Resend_confirm_email = "auth.admin.resend_confirm_email";

        public const string services_view_all = "services.view_all";
        public const string create_services = "services.create";
        public const string update_services = "services.update";
        public const string toggle_services = "services.toggle_availability";


        public const string GetUsers = "users:read";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";

        public const string GetRoles = "roles:read";
        public const string AddRoles = "roles:add";
        public const string UpdateRoles = "roles:update";

        public const string Results = "results:read";




        public static IList<string?> GetAllPermissions()=>
           typeof(Permissions).GetFields().Select(x=>x.GetValue(x) as string).ToList();

    }
}

