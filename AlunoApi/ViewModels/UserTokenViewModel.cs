using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.ViewModels
{
    public class UserTokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
