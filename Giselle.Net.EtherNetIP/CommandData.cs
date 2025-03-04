﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Giselle.Net.EtherNetIP
{
    public class CommandData
    {
        public InterfaceHandle InterfaceHandle { get; set; }
        public ushort Timeout { get; set; }

        public CommandItems Items { get; private set; }

        public CommandData()
        {
            this.InterfaceHandle = InterfaceHandle.CIP;
            this.Timeout = 0;

            this.Items = new CommandItems();
        }

        public void Read(ENIPProcessor processor, bool isRequest)
        {
            this.InterfaceHandle = (InterfaceHandle)processor.ReadUInt();
            this.Timeout = processor.ReadUShort();

            this.Items.Read(processor, isRequest);
        }

        public void Write(ENIPProcessor processor)
        {
            processor.WriteUInt((uint)this.InterfaceHandle);
            processor.WriteUShort(this.Timeout);

            this.Items.Write(processor);
        }

    }

}
