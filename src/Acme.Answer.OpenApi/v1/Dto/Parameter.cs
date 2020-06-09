namespace Acme.Answer.OpenApi.v1.Dto
{
    public partial class AnswerResponse
    {
        public class Parameter : IParameter
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}