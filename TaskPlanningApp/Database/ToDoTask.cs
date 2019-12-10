using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace TaskPlanningApp.Model
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime Deadline { get; set; }
        public bool HighPriority { get; set; }
        public bool Finished { get; set; }
        public string DeadlineDay
        {
            get
            {
                return Deadline.Day + " " + Deadline.ToString("MMM", new CultureInfo("pl-PL"));
            }
        }
        public ToDoTask()
        {

        }
        public ToDoTask(string title, string shortDescription)
        {
            Title = title;
            ShortDescription = shortDescription;
        }
    }
}
