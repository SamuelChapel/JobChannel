using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.OAuth;

namespace JobChannel.BLL.Services.PoleEmploi.Auth
{
    public class AuthServicePoleEmploi
    {
        private DateTime ExpirationDate;
        public bool IsExpired => DateTime.Now > ExpirationDate;

        public AuthServicePoleEmploi() => ExpirationDate = DateTime.Now;

        public async Task<string?> GenerateAccessToken(HttpClient client)
        {
            AccessTokenResponse token;

            try
            {
                var postData = new List<KeyValuePair<string, string>>
                {
                    new("grant_type", "client_credentials"),
                    new("client_id", "PAR_jobchannel_6a45a0539110fd6d4bed061bf58e6779c35056e399531210fa201c738764bd60"),
                    new("client_secret", "df89901879b6de5bf5ae63d38bc8fa7cd647c1675c43861fd8e635e13077b40a"),
                    new("scope", "api_romev1 api_offresdemploiv2 o2dsoffre nomenclatureRome")
                };

                var tokenResponse = client.PostAsync(new Uri("https://entreprise.pole-emploi.fr/connexion/oauth2/access_token?realm=/partenaire"), new FormUrlEncodedContent(postData!)).Result;

                token = await tokenResponse.Content.ReadAsAsync<AccessTokenResponse>(new[] { new JsonMediaTypeFormatter() });
                ExpirationDate = DateTime.Now + TimeSpan.FromMinutes(15);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            return token?.AccessToken;
        }
    }
}