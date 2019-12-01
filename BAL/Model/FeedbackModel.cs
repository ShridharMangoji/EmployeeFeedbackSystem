using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Model
{
  public  class FeedbackModel
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public long statusId { get; set; }
        public string Message { get; set; }
        public long CreatedBy { get; set; }
        public long CreatedFor { get; set; }
        public long FeedbackCategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
    public string CreatedForName { get; set; }
        public string FeedbackCategoryName { get; set; }
    }
}
