using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Tiendita.utils
{
    class ContraseñaUtils
    {
        public static bool IsValiPassword(string contrasenia)
        {
            Regex regex = new Regex(@"^(.{0,7}|[^0-9]*|[^A-Z])$");
            if (regex.IsMatch(contrasenia))
            {
                return true;

            }
            else
            {
                return false;
            }

 
        }
    }

}
