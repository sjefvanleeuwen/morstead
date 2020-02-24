using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Enum;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces
{
    public interface IQuestion
    {
        string QuestionTemplate { set; }
        Dictionary<string, string> TemplateVarables { set; }
        QuestionType QuestionType { get; set; }
        IEnumerable<IAnswerOptions> AnswerOptions { get; set; }
        string ParameterName { get; set; }
        string Answer { get; set; }

        string Text { get; }
        bool IsAnswered { get; }
    }
}
