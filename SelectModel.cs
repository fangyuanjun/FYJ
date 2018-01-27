using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYJ
{
    public class SelectModel
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
