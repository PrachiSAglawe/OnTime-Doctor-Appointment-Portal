using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
   public class LoginResultDto
    {
        public bool IsLoggedIn {  get; set; }
        public Int64 LoggedInID {  get; set; }
        public string LoggeedInName {  get; set; }
        public string Message {  get; set; }
    }
}
