using System.Linq;

namespace MyAPP.API.Services
{
    public static class UserService{

        private static System.Collections.Generic.Dictionary<string,string> UsersandRoles;

        static UserService(){
            UsersandRoles = new System.Collections.Generic.Dictionary<string, string>();
            UsersandRoles.Add("admin","admin");
            UsersandRoles.Add("user","user");
        }

        public static string GetRoleByUserName(string userName){
            var key = UsersandRoles.Keys.FirstOrDefault(x=>x == userName);
            if (key == null){
                return null;
            }
            return UsersandRoles[key];
        }
    }
}