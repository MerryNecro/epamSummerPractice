using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.AWARDS.Entities
{
    public class Award
    {
        Award() { }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;

        }

        public int Id { get; set; }

        public string Title { get; set; }

    }

}
