using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizWebApplication.Extensions
{
    public class UserUtils
    {
        public static String GetUserFriendlyName(ClaimsPrincipal user)
        {
            Console.WriteLine("Getting user friendly name...");
            Console.WriteLine(user);

            Console.WriteLine("Getting user friendly name...");

            user.Claims.ToList().ForEach(claim => Console.WriteLine(claim.Type + " " + claim.Value));

            Claim nameClaim = user.Claims.Where(claim => claim.Type.ToLower().Equals("name")).FirstOrDefault();

            //need to handle unauthenticated users!!!
            if (nameClaim == null)
            {
                string subject = GetUserSubject(user);
                if (subject == null)
                {
                    return null;
                }
                else
                {
                    return subject;
                }
            }
            else
            {
                return nameClaim.Value;
            }
        }

        public static String GetUserSubject(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
