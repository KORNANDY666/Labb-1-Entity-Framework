using System.ComponentModel;

namespace Sistatestetlab1.Models
{
    public class Vacation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        [DisplayName("First Day")]
        public DateTime StartDate { get; set; }
        [DisplayName("Last Day")]
        public DateTime EndDate { get; set; }
        [DisplayName("Time Created")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

       

    }
}

