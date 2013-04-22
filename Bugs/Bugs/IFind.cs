using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs
{
    interface IFind
    {
        void NextLoop();
        void FindingFinished();
        void FileSavedAs(string filname);
        void StartFinding(int numOfThreads);
    }
}
