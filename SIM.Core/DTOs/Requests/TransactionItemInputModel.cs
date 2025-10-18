using SIM.Core.DTOs.Responses;

namespace SIM.Core.DTOs.Requests
{
    public class TransactionItemInputModel : TransactionItemModel
    {
        public int Index { get; set; }

        new public int? Id { get; set; }

    }
}
