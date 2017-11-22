﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataFlow.Web.Helpers
{
    public class Common
    {
        public static string GetUntilOrEmpty(string text, string stopAt)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        public static List<SelectListItem> MonthSelectList()
        {
            var monthList = new List<SelectListItem>();
            monthList.Add(new SelectListItem() { Text = "Select Month", Value = string.Empty });
            monthList.AddRange(Enumerable.Range(01, 12).Select(x =>
                new SelectListItem()
                {
                    Text = x.ToString().PadLeft(2, '0'),
                    Value = x.ToString().PadLeft(2, '0')
                }));

            return monthList;
        }

        public static List<SelectListItem> YearSelectList()
        {
            var yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Text = "Select Year", Value = string.Empty });
            yearList.AddRange(Enumerable.Range(2010, 12).Select(x =>
                new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = x.ToString()
                }));

            return yearList;
        }
    }
}