﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public class Transaction : HierarchicalLoopContainer
    {
        private List<string> _loopStartingSegmentIds;
        private List<string> _loopWithLoopsStartingSegmentIds;
        private List<HierarchicalLoop> _allHLoops;

        internal Transaction(X12DelimiterSet delimiters, string segment, TransactionSpecification spec)
            : base(delimiters, segment)
        {
            Specification = spec;
        }

        public TransactionSpecification Specification { get; private set; }

        public override IList<LoopSpecification> AllowedChildLoops
        {
            get
            {
                if (Specification != null)
                    return Specification.LoopSpecifications;
                else
                    return new List<LoopSpecification>();
            }
        }

        public override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                if (Specification != null)
                    return Specification.SegmentSpecifications;
                else
                    return new List<SegmentSpecification>();
            }
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _loopStartingSegmentIds = new List<string>();
            _loopStartingSegmentIds.Add("NM1");
            _loopWithLoopsStartingSegmentIds = new List<string>();
            _allHLoops = new List<HierarchicalLoop>();
        }

        protected override void PostValidate()
        {
            if (SegmentId != "ST")
                throw new ArgumentException(String.Format("Segment Id expected to be 'ST' but got '{0}'.", SegmentId));
            if (DataElements.Length < 2)
                throw new ArgumentException(String.Format("ST segment expects at least two data elements but got '{0}'.", SegmentString));
        }

        public string ControlNumber
        {
            get { return DataElements[1]; }
        }

        internal List<HierarchicalLoop> AllLoops
        {
            get { return _allHLoops; }
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                writer.WriteStartElement("Transaction");
                writer.WriteAttributeString("ControlNumber", ControlNumber);

                base.WriteXml(writer);

                writer.WriteEndElement();
            }
        }


    }
}