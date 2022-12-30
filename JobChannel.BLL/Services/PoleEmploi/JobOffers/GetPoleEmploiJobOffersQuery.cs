using System;
using JobChannel.Domain.Contracts;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    public record GetPoleEmploiJobOffersQuery(
        ValueTuple<int, int>? Range,
        string? CodeRome,
        string? Commune,
        int? PublieeDepuis,
        bool? EntreprisesAdaptees
        ) : IQuery;
}