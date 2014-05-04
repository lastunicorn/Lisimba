using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.Lisimba
{
    class ProgramArguments
    {
        private string fileName = string.Empty;

        public string  FileName
        {
            get { return fileName; }
        }

        public ProgramArguments(string[] args)
        {
            CmdLineArgs argList = null;
            try
            {
                argList = new CmdLineArgs(args);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (argList.Count > 0)
                fileName = argList[0];

            //if (argList.ContainsKey("f"))
            //    fileName = argList["f"];

            //if (argList.ContainsKey("filename"))
            //    fileName = argList["filename"];
        }
    }
}
