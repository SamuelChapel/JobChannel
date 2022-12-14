using System.Globalization;
using FluentValidation.Resources;

namespace JobChannel.BLL.Validation
{
    public class FrenchLanguageManager : LanguageManager
    {
        public FrenchLanguageManager()
        {
            Culture = new CultureInfo("fr");

            AddTranslation("fr", "JobNotFound", "Le poste n'a pas été trouvé");
            AddTranslation("fr", "ContractNotFound", "Le contrat n'a pas été trouvé");
            AddTranslation("fr", "RegionNotFound", "La région n'a pas été trouvé");
            AddTranslation("fr", "DepartmentNotFound", "Le département n'a pas été trouvé");
            AddTranslation("fr", "CityNotFound", "La ville n'a pas été trouvé");
            AddTranslation("fr", "JobOfferNotFound", "Le poste n'a pas été trouvé");
        }
    }
}
