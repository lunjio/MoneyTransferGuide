using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MoneyTransferGuide.Services
{
    public abstract class BaseHttpService
    {
        protected async Task<HttpOperationResult<T>> SendRequestAsync<T>(Uri url, HttpMethod httpMethod = null, IDictionary<string, string> headers = null, object requestData = null, int connectTimeOut = 3000) where T : class
        {
            var result = new HttpOperationResult<T>();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                result.RegisterErrorState(new Exception("Отсутствует подключение к интернету."));
                return result;
            }

            var method = httpMethod ?? HttpMethod.Get;
            var data = (requestData == null ? null : JsonConvert.SerializeObject(requestData));

            try
            {
                using (var request = new HttpRequestMessage(method, url))
                {
                    if (data != null) request.Content = new StringContent(data, Encoding.UTF8, "application/json");

                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Add(header.Key, header.Value);
                        }
                    }

                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            var connectcancellationToken = new CancellationTokenSource();
                            connectcancellationToken.CancelAfter(TimeSpan.FromMilliseconds(connectTimeOut));
                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, connectcancellationToken.Token).ConfigureAwait(false))
                            {
                                var content = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                if (response.IsSuccessStatusCode)
                                {
                                    try
                                    {
                                        result.Result = content == null ? null : JsonConvert.DeserializeObject<T>(content);
                                    }
                                    catch (Exception e)
                                    {
                                        result.RegisterErrorState(e);
                                    }
                                }
                                else
                                {
                                    result.RegisterErrorState(new HttpRequestException(response.ReasonPhrase));
                                }
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                result.RegisterErrorState(e);
            }

            return result;
        }
    }
}
