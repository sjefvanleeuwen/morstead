namespace Vs.Morstead.Bpm.Model.Tasks
{
    public interface ITask
    {
        string Id { get;  }
        string Name { get;  }
        string Incoming { get; }
        string Outgoing { get; }
    }
}
