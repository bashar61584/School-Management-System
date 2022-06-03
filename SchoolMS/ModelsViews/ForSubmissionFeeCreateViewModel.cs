using SchoolMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMS.ModelsViews
{
    public class ForSubmissionFeeCreateViewModel
    {
        public IEnumerable<StudentPromote> studentpromote{ get; set; }
        public SubmessionFee submessionFee { get; set; }
        public IEnumerable<MonthsModel>monthmodel { get; set; }
    }
}