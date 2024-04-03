using System.Collections.Generic;
using System.Linq;

namespace Application.Pams;

public class StatusConstants
{
    public const string PD = "Pending";
    public const string IP = "InProgress";
    public const string RS = "Resolved";

    public static List<string> GetStatusOptions()
    {
        return typeof(StatusConstants).GetFields().Select(x => x.GetValue(null).ToString()).ToList();
    }
}
