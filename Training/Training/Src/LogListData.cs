using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Training.Model;

namespace Training.Src
{
    class LogListData
    {
        public static List<LogList> Log { get; private set; }

        static LogListData()
        {
            var temp = new List<LogList>();

            AddLog(temp);
            AddLog(temp);
            AddLog(temp);

            Log = temp.OrderBy(i => i.Name).ToList();
        }

        static void AddLog(List<LogList> logObj)
        {
            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });
            
            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });
            
            logObj.Add(new LogList()
            {
                Name = "Will Smith",
                Date = "19 July 2019",
                Time = "02",
                Status = "Status changed from Not Started to in progress"
            });

        }
    }
}
