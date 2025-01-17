﻿using Bazaro.Web.Models;

namespace Bazaro.Web.Services.Commands.CalendarEntries
{
    public static class InsertCalendarEntry
    {
        public class Command
        {
            public int EntryId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        /// <summary>
        /// Inserts CalendarEntry
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static Task Handle(BazaroContext context, Command request)
        {
            context.Add(new CalendarEntry
            {
                EntryId = request.EntryId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Created = DateTime.Now,
            });

            return context.SaveChangesAsync();
        }
    }
}
