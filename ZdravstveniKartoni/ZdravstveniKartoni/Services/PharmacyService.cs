using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;

namespace ZdravstveniKartoni.Services
{
    public interface IPharmacyService
    {
        List<Receipt> GetAllReceipts();
        List<Receipt> SearchReceipts(int searchValue);
        bool AnullReceiptByReceiptId(int receiptId);
    }

    public class PharmacyService : IPharmacyService
    {
        private DataContext _dataContext;

        public PharmacyService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AnullReceiptByReceiptId(int receiptId)
        {
            var receipt = _dataContext.Receipts.Where(r => r.Id == receiptId).FirstOrDefault();
            
            if(receipt == null)
            {
                return false;
            }
            
            receipt.Status = 2;

            _dataContext.Receipts.Update(receipt);
            _dataContext.SaveChanges();

            return true;
        }

        public List<Receipt> GetAllReceipts()
        {
            var receipts = _dataContext.Receipts.Where(r => r.Status == 1).ToList();

            if(receipts == null)
            {
                return new List<Receipt>();
            }

            return receipts;
        }

        public List<Receipt> SearchReceipts(int searchValue)
        {
            return _dataContext.Receipts.Where(r => r.UserId == searchValue && r.Status == 1).ToList();
        }
    }
}
