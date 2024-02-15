using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiWpf.DTO
{
    public class ZombieDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Health { get; set; }

        public int ZombieTypeId { get; set; }
        public string ZombieType { get; set; }
    }
}
