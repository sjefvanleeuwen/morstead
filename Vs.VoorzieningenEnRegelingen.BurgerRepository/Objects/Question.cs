using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects
{
    public class Question : IQuestion
    {
        public string QuestionTemplate { private get;  set; }
        public Dictionary<string, string> TemplateVarables { private get;  set; }
        public QuestionType QuestionType { get; set; }
        public IEnumerable<IAnswerOptions> AnswerOptions { get; set; }
        public string ParameterName { get; set; }
        public string Answer { get; set; }

        public string Text => throw new System.NotImplementedException();

        public bool IsAnswered => throw new System.NotImplementedException();
    }
}
