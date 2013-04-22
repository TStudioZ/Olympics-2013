using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bugs
{
    class BugFind
    {
        private int start;
        private int stop;
        private string bug;
        private string output;
        public string Output { get { return this.output; } }
        private string[] bugText;

        private ManualResetEvent _doneEvent;

        public BugFind(int start, int stop, string bug, string[] bugText, ManualResetEvent doneEvent)
        {
            this.start = start;
            this.stop = stop;
            this.bug = bug;
            this.bugText = bugText;
            _doneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            CreateText();
            FindBugs();
        }

        public void CreateText()
        {
            output = "";
            for (int i = start; i <= stop; i++)
            {
                output += bugText[i];
            }
        }

        public void FindBugs()
        {
            while (output.Contains(bug))
            {
                output = output.Replace(bug, "");
            }
            _doneEvent.Set();
        }
    }
}
