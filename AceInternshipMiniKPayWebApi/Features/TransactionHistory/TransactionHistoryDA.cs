using AceInternshipMiniKPayWebApi.Model;
using AceInternshipMiniKPayWebApi.Models;
using Dapper;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace AceInternshipMiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryDA
    {
        private readonly IDbConnection _db;

        public TransactionHistoryDA(IDbConnection db)
        {
            _db = db;
        }

        public bool IsExistCustomerCode(string customerCode)
        {
            string query = @"select * from Tbl_Customer with (nolock) where CustomerCode=@CustomerCode";
            var item = _db.Query<CustomerModel>(query, new { CustomerCode = customerCode }).FirstOrDefault();
            return item is not null;
            
        }
        public List<CustomerTransactionHistoryModel> TransactionHistoryByCustomerCode(string customerCode) {
            var lst = new List<CustomerTransactionHistoryModel>();
            string query = @"select CTH.*  from Tbl_CustomerTransactionHistory CTH
inner join Tbl_Customer C on CTH.FromMobileNo = C.MobileNo
where CustomerCode = @CustomerCode";

           lst = _db.Query<CustomerTransactionHistoryModel>(query , new {CustomerCode = customerCode}).ToList();  

            return lst;
        }
    }
}
 