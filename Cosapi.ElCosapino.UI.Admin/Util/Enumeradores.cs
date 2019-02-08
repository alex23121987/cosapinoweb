using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Util
{
    public static class Enumeradores
    {
        public enum Jerarquia
        {
            Sistema = 1,
            Modulo = 2,
            OpcionMenu = 3,
            Permisos = 4,
            Accion = 5
        }

        public enum OtherFilters
        {
            Todos = -2,
            Seleccione = 0,
            Otros = -1,
        }

        public enum Acciones
        {
            Nuevo = 1,
            Editar = 2,
            DarDeBaja = 3
        }
        public enum Estados
        {
            Desactivo = 0,
            Activo = 1
        }
        public enum TipoResultado
        {
            Success = 0,
            Danger = 1
        }
    }
}