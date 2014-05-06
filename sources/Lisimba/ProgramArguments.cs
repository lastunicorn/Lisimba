using System;

namespace DustInTheWind.Lisimba
{
    class ProgramArguments
    {
        private readonly string fileName = string.Empty;

        public string FileName
        {
            get { return fileName; }
        }

        public ProgramArguments(string[] args)
        {
            CmdLineArgs argList = new CmdLineArgs(args);

            if (argList.Count > 0)
                fileName = argList[0];

            //if (argList.ContainsKey("f"))
            //    fileName = argList["f"];

            //if (argList.ContainsKey("filename"))
            //    fileName = argList["filename"];
        }
    }
}
