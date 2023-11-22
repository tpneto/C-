using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLayer//remember to change it.
{
    class Programs
    {
        internal static int UpdatePrograms()
        {
            // =========================================================================
            //  Business rules for Programs
            // =========================================================================

            return Data.Programs.UpdatePrograms();
        }
    }

    class Students
    {
        internal static int UpdateStudents()
        {
            // =========================================================================
            //  Business rules for Students
            // =========================================================================

            DataTable dt = Data.Students.GetStudents()
                              .GetChanges(DataRowState.Added | DataRowState.Modified);
            if ((dt != null) && (dt.Select("YearEnrollment < 2017 or YearEnrollment > 2023").Length > 0))
            {
                Midterm_Prep.Form1.msgCommandInvalidYear();
                Data.Students.GetStudents().RejectChanges();
                return -1;
            }
            else
            {
                return Data.Students.UpdateStudents();
            }
        }
    }
}
