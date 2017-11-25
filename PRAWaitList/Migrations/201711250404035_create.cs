namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Double(nullable: false),
                        Family_ID = c.String(),
                        Lastname = c.String(),
                        Firstname = c.String(),
                        Address = c.String(),
                        Apt_Unit_No = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Phone = c.String(),
                        Sex = c.String(),
                        Student_County_of_Residence = c.String(),
                        Current_School_Year = c.String(),
                        Student_Current_Grade = c.String(),
                        Student_DOB = c.String(),
                        Student_School = c.String(),
                        District_Data = c.String(),
                        Status = c.String(),
                        Sibling_Pref = c.String(),
                        Sib1_Name = c.String(),
                        Sib1_atPRA = c.String(),
                        Sib1_School = c.String(),
                        Sib1_Grade = c.String(),
                        Sib1_DOB = c.DateTime(),
                        Sib2_Name = c.String(),
                        Sib2_atPRA = c.String(),
                        Sib2_School = c.String(),
                        Sib2_Grade = c.String(),
                        Sib2_DOB = c.DateTime(),
                        Sib3_Name = c.String(),
                        Sib3_atPRA = c.String(),
                        Sib3_School = c.String(),
                        Sib3_Grade = c.String(),
                        Sib3_DOB = c.String(),
                        Sib4_Name = c.String(),
                        Sib4_atPRA = c.String(),
                        Sib4_School = c.String(),
                        Sib4_Grade = c.String(),
                        Sib4_DOB = c.String(),
                        Sib5_Name5 = c.String(),
                        Sib5_atPRA = c.String(),
                        Sib5_School = c.String(),
                        Sib5_Grade = c.String(),
                        Sib5_DOB = c.String(),
                        Sib6_Name6 = c.String(),
                        Sib6_atPRA = c.String(),
                        Sib6_School = c.String(),
                        Sib6_Grade = c.String(),
                        Father_Lastname = c.String(),
                        Father_Firstname = c.String(),
                        Father_Middle = c.String(),
                        Father_Address = c.String(),
                        Father_Apt_Unit_No = c.String(),
                        Father_City = c.String(),
                        Father_State = c.String(),
                        Father_Zip = c.String(),
                        Father_Home_Phone = c.String(),
                        Father_Work_Phone = c.String(),
                        Father_Cell_Phone = c.String(),
                        Father_Email = c.String(),
                        Mother_Lastname = c.String(),
                        Mother_Firstname = c.String(),
                        Mother_Middle = c.String(),
                        Mother_Address = c.String(),
                        Mother_Apt_Unit_No = c.String(),
                        Mother_City = c.String(),
                        Mother_State = c.String(),
                        Mother_Zip = c.String(),
                        Mother_Home_Phone = c.String(),
                        Mother_Work_Phone = c.String(),
                        Mother_Cell_Phone = c.String(),
                        Mother_Email = c.String(),
                        How_did_you_learn_about_PRA_choices = c.String(),
                        How_did_you_learn_about_PRA = c.String(),
                        Comments = c.String(),
                        Intent_to_Enroll1 = c.String(),
                        Remote_computer_name = c.String(),
                        User_name = c.String(),
                        Browser_type = c.String(),
                        Timestamp = c.DateTime(),
                        Source = c.String(),
                        Intent_to_Enroll = c.String(),
                        password = c.String(),
                        password_verification = c.String(),
                        password_hint = c.String(),
                        imported = c.Boolean(),
                        ImportErrorMsg = c.String(),
                        UID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
        }
    }
}
