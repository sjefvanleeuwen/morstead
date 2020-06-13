using System;
using Vs.Morstead.Bpm.TestData;

namespace Vs.Morstead.Bpm.Model.Tests
{
    public class BpmnTestFixture : IDisposable
    {
        public BpmnProcess Process { get; private set; }

        public void LoadBpmn(string path)
        {
            Process = new BpmnProcess(TestFileLoader.Load(path));
        }

        public BpmnTestFixture()
        {

        }
        public void Dispose()
        {
            // clean up test data
        }
    }
}