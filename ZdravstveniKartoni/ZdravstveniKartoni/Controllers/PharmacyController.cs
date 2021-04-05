using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Services;

namespace ZdravstveniKartoni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        /// <summary>
        /// Get all receipts for pharmacy worker
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllReceipts")]
        public List<Receipt> GetAllReceipts()
        {
            return _pharmacyService.GetAllReceipts();
        }

        /// <summary>
        /// Search Receipts by userId
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpPost("searchReceipts")]
        public List<Receipt> SearchReceipts([FromQuery] int searchValue)
        {
            return _pharmacyService.SearchReceipts(searchValue);
        }

        [HttpPost("anullReceiptByReceiptId/{receiptId}")]
        public bool AnullReceiptByReceiptId(int receiptId)
        {
            return _pharmacyService.AnullReceiptByReceiptId(receiptId);
        }
    }
}