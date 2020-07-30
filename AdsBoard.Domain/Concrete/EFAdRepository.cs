using AdsBoard.Domain.Abstract;
using AdsBoard.Domain.Entities;
using System.Collections.Generic;


namespace AdsBoard.Domain.Concrete
{
    public class EFAdRepository: IAdRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Advertisement> Advertisements
        {
            get { return context.Advertisements; }
        }
    }
}
