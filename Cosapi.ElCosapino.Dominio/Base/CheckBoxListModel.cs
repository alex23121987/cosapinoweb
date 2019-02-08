using System.ComponentModel.DataAnnotations;

namespace Cosapi.ElCosapino.Dominio.Base
{
    public class CheckBoxListModel
    {
        [Display(Name = "Selecciona")]
        public string ParametroTablas { get; set; }
    }

    public class CHKListOptions
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool B_IsSelected { get; set; }

    }

    public class CHKListOptionsText
    {
        public string S_ID { get; set; }
        public string S_Descripcion { get; set; }
        public bool B_IsSelected { get; set; }

    }
}
