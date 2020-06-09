namespace Acme.Answer.OpenApi.v1.Dto
{
    public partial class AnswerResponse
    {
        public interface IParameter
        {
            string Name { get; set; }
            string Value { get; set; }
        }
    }
}