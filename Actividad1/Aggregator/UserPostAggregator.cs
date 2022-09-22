using Actividad1.Dto;
using Actividad1.Utilitarios;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace Actividad1.Aggregator
{
    public class UserPostAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {

                if (responses.Any(x => x.Items.Errors().Count > 0))
                {
                    return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
                }

                var USER = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
                var POST = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();


                var userObj = USerializeObjeto.deserializeJsom_ObjetoList<UserDto>(USER);

                var postObj = USerializeObjeto.deserializeJsom_ObjetoList<PostDto>(POST);

                foreach (var itemUser in userObj)
                {


                    foreach (var itemPost in postObj)
                    {
                        if (itemUser.id == itemPost.userId)
                        {
                            itemUser.posts.Add(itemPost);
                        }

                    }
                }


                var contentBody = JsonConvert.SerializeObject(userObj);

                var stringContent = new StringContent(contentBody)
                {
                    Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
                };

                return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
