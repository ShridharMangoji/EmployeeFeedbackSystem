using System.Collections.Generic;

namespace BAL.Util
{
    public class Constants
    {
        public static readonly List<int> ClosedStatus = new List<int>() { (int)eFeedbackStatus.Closed_Yes, (int)eFeedbackStatus.Closed_No };

        public static int EscalationPeriod = 0;
    }

  public  enum eFeedbackStatus
    {
        New=1,
        Escalated=2,
        Closed_Yes,
        Closed_No
    }
}
