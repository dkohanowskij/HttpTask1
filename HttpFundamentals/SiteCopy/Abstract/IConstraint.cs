﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCopy.Abstract
{
    public interface IConstraint
    {
        ConstraintType ConstraintType { get; }
        bool IsAcceptable(Uri uri);
    }
}
