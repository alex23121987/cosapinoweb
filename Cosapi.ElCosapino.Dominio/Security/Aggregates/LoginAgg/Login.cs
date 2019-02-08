using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg
{  
    public class Login
    {
        #region Propiedades        
        public String S_Usuario { get; set; }
        public String S_Password { get; set; }
        #endregion

        #region Constructor
        public Login()
        {
        }
        #endregion
    }
}
