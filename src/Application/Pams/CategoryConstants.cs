using System.Collections.Generic;
using System.Linq;

namespace Application.Pams;

public class CategoryConstants
{
    public const string RT = "Realtime";
    public const string FTC = "FTC";

    public static List<string> GetCategoryOptions()
    {
        return typeof(CategoryConstants).GetFields().Select(x => x.GetValue(null).ToString()).ToList();
    }
}
