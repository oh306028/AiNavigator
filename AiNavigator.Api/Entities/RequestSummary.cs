using System.ComponentModel.DataAnnotations.Schema;

namespace AiNavigator.Api.Entities
{
    public class RequestSummary
    {
        public int Id { get; set; }
        public string GeneralSummary { get; set; }

        public virtual List<RequestHistory> RequestHistories { get; set; } = new List<RequestHistory>();
    }
}
