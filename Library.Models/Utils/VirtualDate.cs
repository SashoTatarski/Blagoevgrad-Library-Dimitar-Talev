using System;

namespace Library.Models.Utils
{
    public static class VirtualDate
    {
        public static DateTime VirtualToday { get; private set; }

        public static void StartVirtualTime() => VirtualToday = DateTime.Today;

        public static void SkipDays(int days) => VirtualToday = VirtualToday.AddDays(days);
    }
}
