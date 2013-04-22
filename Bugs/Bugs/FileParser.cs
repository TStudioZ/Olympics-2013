using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Bugs
{
    class FileParser
    {
        public const int minThreads = 1;
        public const int maxThreads = 16;
        public static void LoadWriteFile(string filenameIn, string filenameOut, int numOfThreads, IFind parent)
        {
            string bug;
            string program = "";
            string[] programSplitted = null;
            ManualResetEvent[] doneEvents;
            BugFind[] bugFinders;

            using (TextReader reader = new StreamReader(filenameIn))
            {
                if ((bug = reader.ReadLine()) != null && (program = reader.ReadLine()) != null)
                {
                    do
                    {
                        parent.NextLoop();

                        programSplitted = Regex.Split(program, bug);
                        if (programSplitted.Length < numOfThreads)
                            numOfThreads = programSplitted.Length;
                        doneEvents = new ManualResetEvent[numOfThreads];
                        bugFinders = new BugFind[numOfThreads];
                        int[] stringsForThreadsCount = new int[numOfThreads];
                        for (int i = 0, threadIndex = 0; i < programSplitted.Length; i++, threadIndex++)
                        {
                            if (threadIndex >= bugFinders.Length)
                                threadIndex = 0;
                            ++stringsForThreadsCount[threadIndex];
                        }

                        int textIndexStop = 0;
                        for (int i = 0, textIndexStart = 0
                            ; i < stringsForThreadsCount.Length
                            ; i++, textIndexStart = textIndexStop + 1)
                        {
                            textIndexStop = textIndexStart + stringsForThreadsCount[i] - 1;

                            doneEvents[i] = new ManualResetEvent(false);
                            bugFinders[i] = new BugFind(textIndexStart, textIndexStop,
                                bug, programSplitted, doneEvents[i]);

                            ThreadPool.QueueUserWorkItem(bugFinders[i].ThreadPoolCallback, i);
                        }
                        WaitHandle.WaitAll(doneEvents);
                        program = "";
                        for (int i = 0; i < bugFinders.Length; i++)
                        {
                            program += bugFinders[i].Output;
                        }
                    } while (program.Contains(bug));
                }
            }
            parent.FindingFinished();

            if (program != null)
            {
                using (TextWriter writer = File.CreateText(filenameOut))
                {
                    program = program.Replace("\r\n", "");
                    writer.Write(program);
                    writer.WriteLine();
                }
            }
            parent.FileSavedAs(filenameOut);
        }
    }
}
