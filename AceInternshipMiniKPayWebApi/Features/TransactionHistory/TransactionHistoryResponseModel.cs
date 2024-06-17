using AceInternshipMiniKPayWebApi.Models;

namespace AceInternshipMiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<CustomerTransactionHistoryModel> Data { get; set; }
    }
}