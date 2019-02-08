using System;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class CombinatedSectionActivity
    {
        #region Properties
        public int N_ID_COMBINATION_SECCION { get; set; }
        public int N_ID_COMBINATION { get; set; }
        public String NAME { get; set; }
        #endregion
        #region Constructors

        public CombinatedSectionActivity()
        {
        }

        public CombinatedSectionActivity(int n_ID_COMBINATION_SECCION, int n_ID_COMBINATION, String s_NAME)
        {
            N_ID_COMBINATION_SECCION = n_ID_COMBINATION_SECCION;
            N_ID_COMBINATION = n_ID_COMBINATION;
            NAME = s_NAME;
        }


        #endregion
    }
}
