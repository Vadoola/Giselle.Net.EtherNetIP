﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Giselle.Net.EtherNetIP
{
    public class CommandItemSequencedAddress : CommandItem
    {
        public uint ConnectionID { get; set; }
        public uint SequenceCount { get; set; }

        public CommandItemSequencedAddress()
        {

        }

        public override void Read(ENIPProcessor processor)
        {
            base.Read(processor);

            this.ConnectionID = processor.ReadUInt();
            this.SequenceCount = processor.ReadUInt();
        }

        public override void Write(ENIPProcessor processor)
        {
            base.Write(processor);

            processor.WriteUInt(this.ConnectionID);
            processor.WriteUInt(this.SequenceCount);
        }

    }

}
