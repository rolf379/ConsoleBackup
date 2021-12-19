using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsolBackup {
    public class Program {

        public class CompressClass {
            private void ActCompress (string CompressedFolder) {
                ZipFile.CreateFromDirectory (CompressedFolder, Path.Combine (Directory.GetParent (CompressedFolder).FullName, Path.GetFileNameWithoutExtension (CompressedFolder) + ".zip"));
            }
            public void CompressMethod (string[] args) {
                if (args.Length > 0) {
                    string CompressedFolders = args[0];
                    Task thread = null;
                    if (CompressedFolders.Contains (',')) {

                        string[] CompressedFoldersSplit = CompressedFolders.Split (',');
                        foreach (var CompressedFolder in CompressedFoldersSplit) {
                            thread = Task.Factory.StartNew (() => ActCompress (CompressedFolder));
                        }
                        Task.WaitAll (thread);
                    } else {
                        thread = Task.Factory.StartNew (() => ActCompress (CompressedFolders));
                        Task.WaitAll (thread);
                    }
                } else {
                    Console.WriteLine ("To Compress Folder Use Argument!");
                }
            }
        }
        static void Main (string[] args) {
            Stopwatch stopwatch = new Stopwatch ();
            stopwatch.Start ();
            CompressClass compressClass = new CompressClass ();
            compressClass.CompressMethod (args);
            stopwatch.Stop ();
            long ElapsedTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine ("Task completed, Elapsed Time: " + ElapsedTime);
        }
    }
}