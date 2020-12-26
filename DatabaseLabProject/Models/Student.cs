using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLabProject.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string NattionalCode { get; set; }
        public UInt32 Age { get; set; }
        public UInt32 Rank { get; set; }
        public float AverageMark { get; set; }
        public bool IsIranian { get; set; }
    }
}
