using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class EmployeeRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateCode { get; set; }
        public int HoursWorked { get; set; }
        public decimal PayRate { get; set; }

        protected decimal _taxesOwed;
        public decimal TaxesOwed {
            get { return _taxesOwed; }
        }

        protected decimal _totalEarned;
        public decimal TotalEarned { 
            get { return _totalEarned;} 
        }
        
        protected string _totalEarnedToString;


        public EmployeeRecord()
        {
            Id = 00000;
            Name = "default name";
            StateCode = "default code";
            HoursWorked = 000;
            PayRate = 0.00M;
        }

        public EmployeeRecord(int Id, string name, string code, int hours, decimal rate)
        {
            this.Id = Id;
            this.Name = name.Trim();
            this.StateCode = code.Trim();
            this.HoursWorked = hours;
            this.PayRate = rate;
            this._totalEarned = rate * hours;
            this._totalEarnedToString = this._totalEarned.ToString();
            this._taxesOwed = TaxCalculator.ComputeTaxFor(this.StateCode, this._totalEarnedToString);
        }
    }
}
