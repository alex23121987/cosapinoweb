using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.InboxAgg
{
    public interface IInboxRepository
    {
        List<Inbox> List_Inbox_Paginate(string S_IDTipo, string S_IDUsuario);

        Inbox Delete(Inbox item);

        List<Inbox> List_Inbox_Resumen(int N_IDUsuario);
    }
}
