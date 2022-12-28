namespace JobChannel.JobOfferManager.Features.JobOffer
{
    public struct GetPoleEmploiJobOffersRequest
    {
        public (int start, int end) Range { get; set; }
        public string CodeRome { get; set; }

        public GetPoleEmploiJobOffersRequest((int start, int end) range, string codeRome)
        {
            Range = range;
            CodeRome = codeRome;
        }
    }
}