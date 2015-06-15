﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopIK4 : TypedLoop
    {
        private TypedElementPositionInSegment _ik401;

        public TypedLoopIK4() : base("IK4") { }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            base.Initialize(parent, delimiters, loopSpecification);
            _ik401 = new TypedElementPositionInSegment(Loop, 1);
        }

        public TypedElementPositionInSegment IK401
        {
            get { return _ik401; }
        }

        public string IK402_DataElementReferenceNumber
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string IK403_SyntaxErrorCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string IK404_CopyOfBaDataElement
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }
    }
}
