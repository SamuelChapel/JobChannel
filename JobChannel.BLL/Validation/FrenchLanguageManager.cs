using System.Globalization;
using System.Text;
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
            AddTranslation("fr", "DateIntervalInvalid", "La date de début et de fin de validité doivent être renseignées ensembles");
            AddTranslation("fr", "IdRegionsInvalid", "Les régions doivent avoir une Id supérieures à 0");
            AddTranslation("fr", "IdDepartmentsInvalid", "Les départements doivent avoir une Id supérieures à 0");
            AddTranslation("fr", "IdCitiesInvalid", "Les villes doivent avoir une Id supérieures à 0");
            AddTranslation("fr", "IdJobsInvalid", "Les métiers doivent avoir une Id supérieures à 0");
            AddTranslation("fr", "IdContractsInvalid", "Les contrats doivent avoir une Id supérieures à 0");
            AddTranslation("fr", "JobNotInDB", "Le nom du code rome doit exister dans la base de donnée");
            AddTranslation("fr", "UrlInvalid", "L'adresse url entré doit être une url valide");
            AddTranslation("fr", "RangeInvalid", "L'interval entre les deux valeurs doit être compris entre 0 et 149");
        }
    }
}
