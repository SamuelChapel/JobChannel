namespace JobChannel.API.Controllers.Geographics.Cities.Responses
{
    public record CityGetResponse(
        int Id,
        string Name,
        string PostCode,
        string DepartmentName
        );
}
