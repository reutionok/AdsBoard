﻿using AdsBoard.Domain.Abstract;
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
        public void SaveAd(Advertisement ad)
        {
            if (ad.AdId == 0)
                context.Advertisements.Add(ad);
            else
            {
                Advertisement dbEntry = context.Advertisements.Find(ad.AdId);
                if (dbEntry != null)
                {
                    dbEntry.Headline = ad.Headline;
                    dbEntry.Details = ad.Details;
                    dbEntry.ImageData = ad.ImageData;
                    dbEntry.ImageMimeType = ad.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
