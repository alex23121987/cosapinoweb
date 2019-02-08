using Cosapi.ElCosapino.Dominio.Security.Aggregates.InboxAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMInbox : VMBase
    {
        public List<Inbox> List_Inbox { get; set; }
        public Inbox Inbox { get; set; }
        public int MessageResultType { get; set; }
        public string MessageResult { get; set; }
    }
}