namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Helpers
{
    public class FormElementHelperTests
    {
        //[Fact]
        //public void ShouldGetCorrectFormElementTypeBoolean()
        //{

        //}

        //[Fact]
        //public void ShouldParseExecutionResult()
        //{
        //    var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResultEmpty);
        //    var formElementBase = formElement.Data as IOptionsFormElementData;
        //    Assert.Null(formElementBase);

        //    var moqExecutionResult = InitMoqExecutionResult(1);
        //    formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult);
        //    formElementBase = formElement.Data as IOptionsFormElementData;
        //    Assert.Equal("woonland", formElementBase.Name);
        //    Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, formElementBase.InferedType);
        //    Assert.Equal(string.Empty, formElementBase.Label);
        //    Assert.Contains("woonland", formElementBase.Options.Keys);
        //    Assert.Equal("Woonland", formElementBase.Options["woonland"]);
        //    Assert.Equal(string.Empty, formElementBase.TagText);
        //    Assert.Equal("Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.", formElementBase.HintText);
        //}

        //[Fact]
        //public void ShouldParseExecutionResultList()
        //{
        //    var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResultEmpty);
        //    var formElementBase = formElement.Data as IOptionsFormElementData;
        //    Assert.Null(formElementBase);

        //    var moqExecutionResult = InitMoqExecutionResult(2);
        //    formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult);
        //    formElementBase = formElement.Data as IOptionsFormElementData;
        //    Assert.Equal("woonland", formElementBase.Name);
        //    Assert.Equal(TypeInference.InferenceResult.TypeEnum.List, formElementBase.InferedType);
        //    Assert.Equal(string.Empty, formElementBase.Label);
        //    Assert.NotNull(formElementBase.Options["optie1"]);
        //    Assert.Equal("optie1", formElementBase.Options["optie1"]);
        //    Assert.NotNull(formElementBase.Options["optie2"]);
        //    Assert.Equal("optie2", formElementBase.Options["optie2"]);
        //    Assert.Equal(string.Empty, formElementBase.TagText);
        //    Assert.Equal("Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.", formElementBase.HintText);
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeBooleana()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.Boolean,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is Radio);
        //    Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeDouble()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.Double,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is Number);
        //    Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeList()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.List,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is Select);
        //    Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeTimeSpan()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.TimeSpan,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is IFormElementBase);
        //    Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeDateTime()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.DateTime,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is IFormElementBase);
        //    Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypeString()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.String,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is IFormElementBase);
        //    Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //[Fact]
        //public void ShouldGetCorrectFormElementTypePeriod()
        //{
        //    var ParameterList = new List<IParameter> { new ClientParameter()
        //        {
        //            Type = TypeInference.InferenceResult.TypeEnum.Period,
        //            Name = "test"
        //        }
        //    } as IEnumerable<IParameter>;
        //    var moqParameters = new Mock<IParametersCollection>();
        //    moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
        //    var moqQuestion = new Mock<IQuestionArgs>();
        //    moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
        //    var moqExecutionResult = new Mock<IExecutionResult>();
        //    moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
        //    var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
        //    Assert.True(formElement is IFormElementBase);
        //    Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
        //}

        //private IExecutionResult InitMoqExecutionResult(int type)
        //{
        //    var moq = new Mock<IExecutionResult>();
        //    var moqCoreStep = InitMoqCoreStep();
        //    //moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
        //    moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { null, new FlowExecutionItem(moqCoreStep) });
        //    moq.Setup(m => m.InferedType).Returns(type == 1 ? TypeInference.InferenceResult.TypeEnum.Boolean : TypeInference.InferenceResult.TypeEnum.List);
        //    moq.Setup(m => m.GetQuestionParameters()).Returns(GetParameterList(type));
        //    return moq.Object;
        //}

        //private IStep InitMoqCoreStep()
        //{
        //    var moq = new Mock<IStep>();
        //    moq.Setup(m => m.Description).Returns("This is a test question");
        //    return moq.Object;
        //}

        //private IParametersCollection InitMoqParementerCollection(int type)
        //{
        //    var moq = new Mock<IParametersCollection>();
        //    moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { InitMoqParameter(type) });
        //    return moq.Object;
        //}

        //private IList<IParameter> GetParameterList(int type)
        //{
        //    return new List<IParameter> { InitMoqParameter(type) };
        //}

        //private IParameter InitMoqParameter(int type)
        //{
        //    var moq = new Mock<IParameter>();
        //    moq.Setup(m => m.Name).Returns("woonland");
        //    if (type == 1)
        //    {
        //        moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
        //    }
        //    if (type == 2)
        //    {
        //        moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.List);
        //        moq.Setup(m => m.Value).Returns(new List<object> { "optie1", "optie2" });
        //    }
        //    return moq.Object;
        //}


    }
}
