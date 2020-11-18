using System;
using System.Collections.Generic;
using System.Text;

namespace Like.Domain.Entities
{
    public class Cont
    {
        private int _vl = 0;

        public int VA { get => _vl; }

        public void Incrementar()
        {
            _vl++;
        }
    }
}
