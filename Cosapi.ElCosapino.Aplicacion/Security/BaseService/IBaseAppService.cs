using Cosapi.ElCosapino.Dominio.Base;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.BaseService
{
    public interface IBaseAppService
    {        
        List<DDLOptionsText> Lista_ParametroCombo(string sTabla, int IDUserUnidad);

        List<CHKListOptionsText> Lista_ParametroCheckBox(string sTabla, int IDUserUnidad);

        List<DDLOptionsText> Lista_Responsable();

        List<DDLOptionsText> Lista_Entregable();

        List<DDLOptionsText> Lista_Especialidad(int N_IDUnidad);

        List<DDLOptionsText> Lista_Especialidad_Con_ID();
    }
}
