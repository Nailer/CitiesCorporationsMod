using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CitiesCorporations.Model
{
    class Corporation
    {
        public string Name { get; private set; }
        public uint Id { get; private set; }
        public Color Color { get; private set; }

        public Corporation(uint id, string name, Color color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
