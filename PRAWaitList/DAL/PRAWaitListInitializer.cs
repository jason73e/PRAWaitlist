using System.Collections.Generic;
using PRAWaitList.Models;

namespace PRAWaitList.DAL
{
    public class PRAWaitListInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PRAWaitListContext>
    {
        protected override void Seed(PRAWaitListContext context)
        {
        }
    }
}