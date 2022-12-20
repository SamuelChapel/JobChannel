using System;

namespace JobChannel.Domain.DTO
{
    public record JobOfferFindRequest(
        int? Id_Region,
        int? Id_Department,
        int? Id_City,
        int? Id_Job,
        int? Id_Contract,
        DateTime? PublicationDate,
        string? SearchString
    );
}
