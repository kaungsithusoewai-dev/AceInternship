using AceInternshipMiniKPayWebApi.Models;

namespace AceInternshipMiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryBL
    {
        private readonly TransactionHistoryDA _transactionHistoryDA;
        public TransactionHistoryBL(TransactionHistoryDA transactionHistoryDA)
        {
            _transactionHistoryDA = transactionHistoryDA;
        }

        public TransactionHistoryResponseModel TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();
            // Exit Customer Code
            bool isExist = _transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode!);
            if (!isExist)
            {
                model.IsSuccess = false;
                model.Message = "Customer dosen't exist";
                return model;
            }
                // Transaction history by Customer Code
                var lst = _transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode!);
                
                    model.IsSuccess = true;
                    model.Message = "Success";
                    model.Data = lst;
                    return model;
                 
            }
        } 
    } 


