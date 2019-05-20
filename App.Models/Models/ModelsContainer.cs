using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DataBaseAccess;

namespace App.Models.Models
{
    public static class ModelsContainer
    {
        public static AppUserModel GetAspNetUser()
        {
            return new AppUserModel();
        }
    }
}
