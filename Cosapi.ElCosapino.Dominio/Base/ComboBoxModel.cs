using System.ComponentModel.DataAnnotations;

namespace Cosapi.ElCosapino.Dominio.Base
{
    public class ComboBoxModel
    {
        [Display(Name = "Selecciona")]
        public string ParametroTablas { get; set; }
    }

    public class DDLOptions
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

    }

    public class DDLOptionsText
    {
        public string S_ID { get; set; }

        public string S_Descripcion { get; set; }
    }

    public class OptionsTextMultiple
    {
        public int N_ID { get; set; }

        public string S_Descripcion { get; set; }

        public string S_Codigo { get; set; }
    }
}
