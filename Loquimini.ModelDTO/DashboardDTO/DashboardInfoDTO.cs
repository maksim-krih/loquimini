using System;

namespace Loquimini.ModelDTO.DashboardDTO
{
    public class DashboardInfoDTO
    {
        public decimal TotalSum { get; set; }
        public decimal TotalDebts { get; set; }
        public int CurrentFilled { get; set; }
        public int TotalFilled { get; set; }
        public int CurrentPaid { get; set; }
        public int TotalPaid { get; set; }
    }
}
