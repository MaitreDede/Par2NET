using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Par2NET.Interface;

namespace Par2NET
{
    public class Par1Library : IParLibrary
    {
        public ParResult Process(List<string> inputFiles, List<string> recoveryFiles, ParAction action)
        {
            switch (action)
            {
                case ParAction.ParCreate:
                    return Create(ref inputFiles, ref recoveryFiles);
                case ParAction.ParRepair:
                    return Repair(ref inputFiles, ref recoveryFiles);
                case ParAction.ParVerify:
                    return Verify(ref inputFiles, ref recoveryFiles);
            }

            return ParResult.LogicError;
        }

        ParResult IParLibrary.Process<TArgs>(TArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if(args is Par1LibraryArguments)
            {
                return this.Process((Par1LibraryArguments)(object)args);
            }
            else
            {
                throw new ArgumentException("Args type must be Par1LibArguments", nameof(args));
            }
        }

        public ParResult Process(Par1LibraryArguments args)
        {
            List<string> inputFiles = new List<string>(args.InputFiles);
            List<string> recoveryFiles = new List<string>(args.RecoveryFiles);

            switch (args.Action)
            {
                case ParAction.ParCreate:
                    return Create(ref inputFiles, ref recoveryFiles);
                case ParAction.ParRepair:
                    return Repair(ref inputFiles, ref recoveryFiles);
                case ParAction.ParVerify:
                    return Verify(ref inputFiles, ref recoveryFiles);
            }

            return ParResult.LogicError;
        }

        public ParResult Create(ref List<string> inputFiles, ref List<string> recoveryFiles)
        {
            return ParResult.LogicError;
        }

        public ParResult Repair(ref List<string> inputFiles, ref List<string> recoveryFiles)
        {
            return ParResult.LogicError;
        }

        public ParResult Verify(ref List<string> inputFiles, ref List<string> recoveryFiles)
        {
            return ParResult.LogicError;
        }
    }
}
