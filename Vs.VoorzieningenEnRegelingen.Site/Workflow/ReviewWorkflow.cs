using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Vs.VoorzieningenEnRegelingen.Site.Workflow
{
    public class ReviewWorkflow : IWorkflow<ReviewDataClass>
    {
        public string Id => "ReviewWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<ReviewDataClass> builder)
        {
            builder
                .StartWith<ProvideArtefact>()
                .Input(p => p.Id, p => p.ArtefactId);
        }

        public void Build(IWorkflowBuilder<object> builder)
        {
           //
        }
    }

    public class ReviewDataClass
    {
        public string ArtefactId { get; set; }
        public string UserId { get; set; }
        public ReviewStatus Status { get; set; }
    }

    public enum ReviewStatus
    {
        Approved,
        Rejected
    }

    public class ProvideArtefact : StepBody
    {
        public string Id { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }

    public class AddRole : StepBody
    {
        public string UserId { get; set; }
        public RoleType Role { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }

    public enum RoleType
    {
        Reviewer
    }
}
