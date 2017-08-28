using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PRAWaitList.Models;
using System.Reflection;
using System.IO;
using CsvHelper;
using System.Text;

namespace PRAWaitList.DAL
{
    public class PRAWaitListInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PRAWaitListContext>
    {
        protected override void Seed(PRAWaitListContext context)
        {


            var schoolyears = new List<SchoolYearModel>
            {
                new SchoolYearModel() { Name="2010 - 2011", StartYear=2010 , EndYear=2011},
                new SchoolYearModel() { Name="2011 - 2012", StartYear=2011 , EndYear=2012},
                new SchoolYearModel() { Name="2012 - 2013", StartYear=2012 , EndYear=2013},
                new SchoolYearModel() { Name="2013 - 2014", StartYear=2013 , EndYear=2014},
                new SchoolYearModel() { Name="2014 - 2015", StartYear=2014 , EndYear=2015},
                new SchoolYearModel() { Name="2015 - 2016", StartYear=2015 , EndYear=2016},
                new SchoolYearModel() { Name="2016 - 2017", StartYear=2016 , EndYear=2017},
                new SchoolYearModel() { Name="2017 - 2018", StartYear=2017 , EndYear=2018},
                new SchoolYearModel() { Name="2018 - 2019", StartYear=2018 , EndYear=2019},
                new SchoolYearModel() { Name="2019 - 2020", StartYear=2019 , EndYear=2020},
                new SchoolYearModel() { Name="2020 - 2021", StartYear=2020 , EndYear=2021}
            };
            schoolyears.ForEach(s => context.SchoolYears.Add(s));
            context.SaveChanges();

            var hearaboutpras = new List<HearAboutPRAModel>
            {
                new HearAboutPRAModel() { Text="School Website", IsChecked = false, Value=1},
                new HearAboutPRAModel() { Text="District Website", IsChecked = false, Value=2},
                new HearAboutPRAModel() { Text="Friend recommendation", IsChecked = false, Value=3},
                new HearAboutPRAModel() { Text="Live in the neighborhood", IsChecked = false, Value=4},
                new HearAboutPRAModel() { Text="PRA parent recommendation", IsChecked = false, Value=5},
                new HearAboutPRAModel() { Text="General Internet research", IsChecked = false, Value=6},
                new HearAboutPRAModel() { Text="Other", IsChecked = false, Value=7}
            };
            hearaboutpras.ForEach(s => context.HearAboutPRAs.Add(s));
            context.SaveChanges();
        }
    }
}