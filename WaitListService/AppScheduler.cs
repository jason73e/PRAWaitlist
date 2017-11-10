using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WaitListService
{
    public partial class AppScheduler : ServiceBase
    {
        System.Timers.Timer _timer = new System.Timers.Timer();
        DataSet ds = new DataSet();

        private string app2Launch;
        private void AppLauncher(string path)
        {
            app2Launch = path;
        }
        public void runApp(AppTask t)
        { 
            AppTaskHistory th = new AppTaskHistory();
            try
            {
                th.taskid = t.uid;
                ProcessStartInfo pInfo = new ProcessStartInfo(app2Launch);
                pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                th.startdate = DateTime.Now;
                Process p = Process.Start(pInfo);
                WriteToLog(DateTime.Now.ToString() + " || Starting Processid: " + p.Id.ToString() + " ThreadID: " + Thread.CurrentThread.Name + " TaskName: " + t.Name + " at " + th.startdate.ToString());
                p.WaitForExit();
                DateTime dtNow = p.ExitTime;
                th.startdate = p.StartTime;
                th.enddate = dtNow;
                th.status = p.ExitCode.ToString();
                DateTime runTime = DateTime.Now;
                string strInterval = t.RepeatUnit.ToString();
                switch(strInterval)
                {
                    case "D":
                        runTime = dtNow.AddDays(t.RepeatInterval);
                        break;
                    case "W":
                        runTime = dtNow.AddDays(t.RepeatInterval*7);
                        break;
                    case "M":
                        runTime = dtNow.AddMonths(t.RepeatInterval);
                        break;
                    case "h":
                        runTime = dtNow.AddHours(t.RepeatInterval);
                        break;
                    case "m":
                        runTime = dtNow.AddMinutes(t.RepeatInterval);
                        break;
                    case "s":
                        runTime = dtNow.AddSeconds(t.RepeatInterval);
                        break;
                }
                t.NextRun = runTime;

            }
            catch(Exception e)
            {
                th.enddate = DateTime.Now;
                th.status = "Error: " + e.Message + " at " + e.Source;
                WriteToLog(DateTime.Now.ToString() + " || Error: " + e.Message);
            }
            finally
            {
                t.running = false;
                UpdateAppTask(t);
                th.ts = DateTime.Now;
                AddTaskHistory(th);
            }
        }

        private void AddTaskHistory(AppTaskHistory th)
        {
            try
            {
                dbContext db = new dbContext();
                db.AppTaskHistories.Add(th);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                WriteToLog(DateTime.Now.ToString() + " || AddTaskHistory || " + e.Message + " " + th.taskid + " " + th.status);
            }
        }

        void timeElapsed(object sender, ElapsedEventArgs args)
        {
            try
            {
                _timer.Stop();
                DateTime currTime = DateTime.Now;
                List<AppTask> ls = GetAppTasks();
                foreach(AppTask t in ls)
                {
                    DateTime runTime = t.NextRun;
                    if(runTime < currTime)
                    {
                        string exePath = t.ExePath;
                        WriteToLog(DateTime.Now.ToString() + " || Starting Task " + exePath);
                        t.running = true;
                        UpdateAppTask(t);
                        AppLauncher(exePath);
                        Thread th = new Thread(() => runApp(t));
                        th.Start();
                        Thread.Sleep(1000);

                    }
                }
            }
            catch(Exception e)
            {
                WriteToLog(DateTime.Now.ToString() + " || timeElapsed || " + e.Message);
            }
            finally
            {
                _timer.Start();
            }
        }

        public void UpdateAppTask(AppTask t)
        {
            try
            {
                dbContext db = new dbContext();
                AppTask a = db.AppTasks.Find(t.uid);
                a.active = t.active;
                a.ExePath = t.ExePath;
                a.Name = t.Name;
                a.NextRun = t.NextRun;
                a.RepeatInterval = t.RepeatInterval;
                a.RepeatUnit = t.RepeatUnit;
                a.running = t.running;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                WriteToLog(DateTime.Now.ToString() + " || UpdateAppTask || " + ex.Message);
            }
        }

        private List<AppTask> GetAppTasks()
        {
            dbContext db = new dbContext();
            List<AppTask> ls = db.AppTasks.Where(x => x.NextRun <= DateTime.Now && x.active == true && x.running == false).ToList();
            return ls;
        }
        public AppScheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToLog("============= " + DateTime.Now.ToString() + " ==================");
            WriteToLog("============= Starting Service ==================");
            _timer.Interval = 5000;
            _timer.Elapsed += new ElapsedEventHandler(timeElapsed);
            _timer.Start();
        }

        protected override void OnStop()
        {
            WriteToLog("============= " + DateTime.Now.ToString() + " ==================");
            WriteToLog("============= Stopping Service ==================");

        }

        public void WriteToLog(string TextToLog)
        {
            StreamWriter sw = null;
            string CurrentLogFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Log.txt";
            sw = new StreamWriter(CurrentLogFilePath, true);
            sw.WriteLine(TextToLog);
            sw.Flush();
            sw.Close();
        }
    }
}
