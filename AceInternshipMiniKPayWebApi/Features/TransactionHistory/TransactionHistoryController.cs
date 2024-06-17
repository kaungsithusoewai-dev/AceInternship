using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternshipMiniKPayWebApi.Features.TransactionHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryBL _transactionHistoryBL;
       
public TransactionHistoryController(TransactionHistoryBL transactionHistoryBL)
        {
            _transactionHistoryBL = transactionHistoryBL;
        }
        [HttpPost]
        public IActionResult TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(requestModel.CustomerCode))
                {
                    return BadRequest("Invalid customer code");
                }

               var model = _transactionHistoryBL.TransactionHistory(requestModel);
                
                return Ok(model);
            }
            catch (Exception ex)
            {
                   return StatusCode(500, ex.ToString());
            }

        }
    }
}