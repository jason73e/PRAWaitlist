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
            var states = new List<StateModel>
            {
                new StateModel() { Name="Alabama", StateID="AL"},
                new StateModel() { Name="Alaska", StateID="AK"},
                new StateModel() { Name="Arizona", StateID="AZ"},
                new StateModel() { Name="Arkansas", StateID="AR"},
                new StateModel() { Name="California", StateID="CA"},
                new StateModel() { Name="Colorado", StateID="CO"},
                new StateModel() { Name="Connecticut", StateID="CT"},
                new StateModel() { Name="District of Columbia", StateID="DC"},
                new StateModel() { Name="Delaware", StateID="DE"},
                new StateModel() { Name="Florida", StateID="FL"},
                new StateModel() { Name="Georgia", StateID="GA"},
                new StateModel() { Name="Hawaii", StateID="HI"},
                new StateModel() { Name="Idaho", StateID="ID"},
                new StateModel() { Name="Illinois", StateID="IL"},
                new StateModel() { Name="Indiana", StateID="IN"},
                new StateModel() { Name="Iowa", StateID="IA"},
                new StateModel() { Name="Kansas", StateID="KS"},
                new StateModel() { Name="Kentucky", StateID="KY"},
                new StateModel() { Name="Louisiana", StateID="LA"},
                new StateModel() { Name="Maine", StateID="ME"},
                new StateModel() { Name="Maryland", StateID="MD"},
                new StateModel() { Name="Massachusetts", StateID="MA"},
                new StateModel() { Name="Michigan", StateID="MI"},
                new StateModel() { Name="Minnesota", StateID="MN"},
                new StateModel() { Name="Mississippi", StateID="MS"},
                new StateModel() { Name="Missouri", StateID="MO"},
                new StateModel() { Name="Montana", StateID="MT"},
                new StateModel() { Name="Nebraska", StateID="NE"},
                new StateModel() { Name="Nevada", StateID="NV"},
                new StateModel() { Name="New Hampshire", StateID="NH"},
                new StateModel() { Name="New Jersey", StateID="NJ"},
                new StateModel() { Name="New Mexico", StateID="NM"},
                new StateModel() { Name="New York", StateID="NY"},
                new StateModel() { Name="North Carolina", StateID="NC"},
                new StateModel() { Name="North Dakota", StateID="ND"},
                new StateModel() { Name="Ohio", StateID="OH"},
                new StateModel() { Name="Oklahoma", StateID="OK"},
                new StateModel() { Name="Oregon", StateID="OR"},
                new StateModel() { Name="Pennsylvania", StateID="PA"},
                new StateModel() { Name="Rhode Island", StateID="RI"},
                new StateModel() { Name="South Carolina", StateID="SC"},
                new StateModel() { Name="South Dakota", StateID="SD"},
                new StateModel() { Name="Tennessee", StateID="TN"},
                new StateModel() { Name="Texas", StateID="TX"},
                new StateModel() { Name="Utah", StateID="UT"},
                new StateModel() { Name="Vermont", StateID="VT"},
                new StateModel() { Name="Virginia", StateID="VA"},
                new StateModel() { Name="Washington", StateID="WA"},
                new StateModel() { Name="West Virginia", StateID="WV"},
                new StateModel() { Name="Wisconsin", StateID="WI"},
                new StateModel() { Name="Wyoming", StateID="WY"}
            };
            states.ForEach(s => context.States.Add(s));
            context.SaveChanges();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PRAWaitList.App_Data.USSchoolList.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    List<SchoolModel> schools = new List<SchoolModel>();
                    while(csvReader.Read())
                    {
                        SchoolModel school = new SchoolModel();
                        school.SchoolName = csvReader.GetField<string>("SchoolName");
                        school.StateName = csvReader.GetField<string>("StateName");
                        school.StateAbbr = csvReader.GetField<string>("StateAbbr");
                        school.SchoolID = csvReader.GetField<string>("SchoolID");
                        school.AgencyName = csvReader.GetField<string>("AgencyName");
                        school.AgencyID = csvReader.GetField<string>("AgencyID");
                        schools.Add(school);
                    }
                    //                    var schools = csvReader.GetRecords<SchoolModel>();
                    context.Schools.AddRange(schools);
                }
            }
            context.SaveChanges();

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