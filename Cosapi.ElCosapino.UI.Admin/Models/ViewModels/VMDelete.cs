using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.UI.Admin.Util.CustomDataValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMDelete
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar fecha ejecución de baja.")]
        [DateFormatValidate(Constantes.Formaters.FormatDate, ErrorMessage = "La fecha de baja es incorrecta.")]
        public string S_DATE_TERMINATE_REGISTER { get; set; }

        [Required(ErrorMessage = "Debe ingresar motivo baja.")]
        public string S_OBSERVATION { get; set; }

        public string MessageResult { get; set; }

        public string S_NAME { get; set; }

        public string S_NAME_SHORT { get; set; }
       
        public string S_ACTION_TYPE { get; set; }
        public string S_REQUEST_TYPE { get; set; }
        //[Required(ErrorMessage = "Debe ingresar Function Id.")]
        public string S_CODE_FUNCTION { get; set; }    

        //public string S_NAME_COMPLETE { get; set; }
    }
}