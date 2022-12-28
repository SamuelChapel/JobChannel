namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    public record GetPoleEmploiJobOffersQuery(
        (int start, int end) Range,
        string CodeRome
        );
}