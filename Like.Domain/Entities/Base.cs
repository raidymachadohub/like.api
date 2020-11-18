using System;

namespace Like.Domain.Entities
{
    public class Base
    {
        public string guid { get; set; }

        public Base() => this.guid = Guid.NewGuid().ToString();

    }
}
