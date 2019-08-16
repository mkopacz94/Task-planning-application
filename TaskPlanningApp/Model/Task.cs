using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanningApp.Model
{
    public class Task
    {
        private DateTime _deadline;
        private string _name, _shortDescription, _longDescription;

        public Task()
        {
            _deadline = new DateTime();
            _name = _shortDescription = _longDescription = "Not assigned";
        }

        public Task(string name)
        {
            this._name = name;
            _deadline = new DateTime();
            _shortDescription = _longDescription = "Not assinged";
        }

        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }
        public DateTime Deadline
        {
            get { return _deadline; }
            set { this._deadline = value; }
        }

        public string ShortDescription
        {
            get { return _shortDescription; }
            set { this._shortDescription = value; }
        }

        public string LongDescription
        {
            get { return _longDescription; }
            set { this._longDescription = value; }
        }
    }
}
