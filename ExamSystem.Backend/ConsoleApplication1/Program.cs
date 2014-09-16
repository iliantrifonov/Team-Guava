using ExamSystem.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ExamSystemData();
            context.Comments.Add(new ExamSystem.Backend.Models.Comment());
        }
    }
}
