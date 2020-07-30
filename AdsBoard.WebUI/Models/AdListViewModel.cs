using AdsBoard.Domain.Entities;
using System.Collections.Generic;


namespace AdsBoard.WebUI.Models
{
    public class AdListViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
