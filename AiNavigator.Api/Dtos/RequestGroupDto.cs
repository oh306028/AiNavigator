namespace AiNavigator.Api.Dtos
{
    public class RequestGroupDto
    {
        public Guid RequestId { get; set; }
        public List<PromptDetailsDto> Models { get; set; }
        public string Summary { get; set; }
        public string RequestDate { get; set; }   

    }

}
