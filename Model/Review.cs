using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Serial.Model
{
    public enum PriorityModer : byte { Good, NotBad, Bad}
    public class Review
    {
        public string Descr { get; private set; }
        public DateTime DateCreate { get; private set; }
        public DateTime DateUpdate { get; private set; }
        public PriorityModer Priority { get; private set; }
        public bool Completed { get; set; }

        public Review(string descr, DateTime dateCreate, DateTime dateUpdate, PriorityModer priority, bool completed = false)
        {
            this.Descr = descr;
            this.DateCreate = dateCreate;
            this.DateUpdate = dateUpdate;
            this.Priority = priority;
            this.Completed = completed;
        }
        public override string ToString()
        {
            return Descr + ", Priority: " + Priority.ToString() + ", Date Create " + DateCreate.ToString() + ", Date Update " + DateUpdate.ToString() + ", Completed: " + ( Completed ? "yes" : "no");
        }
    }
}
