﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Giselle.Net.EtherNetIP
{
    public class CommandItems : List<CommandItem>
    {
        public CommandItems()
        {

        }

        public T Find<T>() where T : CommandItem
        {
            return this.OfType<T>().FirstOrDefault();
        }

        public virtual void Read(ENIPProcessor processor, bool isRequest)
        {
            this.Clear();
            var itemCount = processor.ReadUShort();

            for (var i = 0; i < itemCount; i++)
            {
                var id = processor.ReadUShort();
                var bytes = new byte[processor.ReadUShort()];
                processor.ReadBytes(bytes);

                var registration = CommandItemRegister.FromId(id);

                using (var ms = new MemoryStream(bytes))
                {
                    var item = registration.SelectType(isRequest).GetConstructor(new Type[0]).Invoke(new object[0]) as CommandItem;
                    item.Read(new ENIPProcessor(ms));
                    this.Add(item);
                }

            }

        }

        public virtual void Write(ENIPProcessor processor)
        {
            processor.WriteUShort((ushort)this.Count);

            foreach (var item in this)
            {
                var registration = CommandItemRegister.FromType(item.GetType());

                using (var ms = new MemoryStream())
                {
                    item.Write(new ENIPProcessor(ms));
                    processor.WriteUShort(registration.Id);
                    processor.WriteUShort((ushort)ms.Length);
                    processor.WriteBytes(ms.ToArray());
                }

            }

        }

    }

}
