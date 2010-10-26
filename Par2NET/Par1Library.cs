﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Par2NET
{
    public class Par1Library
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
