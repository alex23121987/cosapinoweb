using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.BaseAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class BaseRepository : IBaseRepository
    {    
        public BaseRepository(){}      

        public List<DDLOptionsText> Lista_ParametroCombo(string sTabla, int IDUserUnidad)
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Sellst_Parametro");
            oDatabase.AddInParameter(oDbCommand, "@S_Tabla", DbType.String, sTabla);
            oDatabase.AddInParameter(oDbCommand, "@IDUserUnidad", DbType.String, IDUserUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["S_ID"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<CHKListOptionsText> Lista_ParametroCheckBox(string sTabla, int IDUserUnidad)
        {
            var lstParametro = new List<CHKListOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Sellst_Parametro");
            oDatabase.AddInParameter(oDbCommand, "@S_Tabla", DbType.String, sTabla);
            oDatabase.AddInParameter(oDbCommand, "@IDUserUnidad", DbType.String, IDUserUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new CHKListOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["S_ID"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"]),
                        B_IsSelected = false
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> Lista_Responsable()
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Base_Sellst_Responsable");

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["S_IDUsuario"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_NombreCompleto"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> Lista_Entregable()
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Base_Sellst_Entregable");

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["S_ID"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> Lista_Especialidad(int N_IDUnidad)
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Base_Sellst_Especialidad");
            oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, N_IDUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["N_IDEspecialidad"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> Lista_Especialidad_Con_ID()
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Base_Sellst_Especialidad_Con_ID");

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["N_IDEspecialidad"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> Lista_Perfil()
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Base_Sellst_Perfil");

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["S_ID"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }
    }
}
