using Culqi.Entities;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
#if !NET45
using System.Runtime.CompilerServices;
#endif
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Services.Base
{
    public abstract class Service<TEntityReturned> where TEntityReturned : ICulqiEntity
    {
        private ICulqiClient client;

        protected Service()
        {
        }

        protected Service(ICulqiClient client)
        {
            this.client = client;
        }

        public abstract string BasePath { get; }

        public virtual string BaseUrl => Client.ApiBase;

        public ICulqiClient Client
        {
            get => client ?? CulqiConfiguration.CulqiClient;
            set => client = value;
        }

        protected Task<TEntityReturned> CreateEntity(BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return RequestAsync(HttpMethod.Post, ClassUrl(), options, requestOptions, cancellationToken);
        }

        protected Task<TEntityReturned> DeleteEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return RequestAsync(HttpMethod.Delete, InstanceUrl(id), options, requestOptions, cancellationToken);
        }       

        protected Task<TEntityReturned> GetEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return RequestAsync(HttpMethod.Get, InstanceUrl(id), options, requestOptions, cancellationToken);
        }

        protected Task<CulqiList<TEntityReturned>> ListEntities(ListOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return RequestAsync<CulqiList<TEntityReturned>>(HttpMethod.Get, ClassUrl(), options, requestOptions, cancellationToken);
        }
       
#if !NET45
        protected IAsyncEnumerable<TEntityReturned> ListEntitiesAutoPaging(ListOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return ListRequestAutoPaging<TEntityReturned>(ClassUrl(), options, requestOptions, cancellationToken);
        }
#else
         protected IEnumerable<TEntityReturned> ListEntitiesAutoPaging(ListOptions options, RequestOptions requestOptions)
        {
            return ListRequestAutoPaging<TEntityReturned>(ClassUrl(), options, requestOptions);
        }
#endif

        protected Task<TEntityReturned> UpdateEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return RequestAsync(new HttpMethod("PATCH"), InstanceUrl(id), options, requestOptions, cancellationToken);
        }

        protected Task<TEntityReturned> RequestAsync(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken = default)
        {
            return RequestAsync<TEntityReturned>(method, path, options, requestOptions, cancellationToken);
        }

        protected T Request<T>(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions) where T : ICulqiEntity
        {
            return RequestAsync<T>(method, path, options, requestOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected async Task<T> RequestAsync<T>(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken = default) where T : ICulqiEntity
        {
            requestOptions = SetupRequestOptions(requestOptions);
            return await Client.RequestAsync<T>(method, path, options, requestOptions, cancellationToken).ConfigureAwait(false);
        }        

#if !NET45
        protected async IAsyncEnumerable<T> ListRequestAutoPaging<T>(
            string url,
            ListOptions options,
            RequestOptions requestOptions,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
            where T : ICulqiEntity
        {
            var page = await RequestAsync<CulqiList<T>>(HttpMethod.Get, url, options, requestOptions, cancellationToken);

            options = options ?? new ListOptions();
            bool iterateBackward = false;

            if (!string.IsNullOrEmpty(options.Before) && string.IsNullOrEmpty(options.After))
            {
                iterateBackward = true;
            }

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (iterateBackward)
                {
                    page.Reverse();
                }

                string itemId = null;
                foreach (var item in page)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (item == null)
                    {
                        continue;
                    }

                    itemId = ((IHasId)item).Id;
                    yield return item;
                }

                if (string.IsNullOrEmpty(itemId))
                {
                    break;
                }

                if (iterateBackward)
                {
                    options.Before = itemId;
                }
                else
                {
                    options.After = itemId;
                }

                page = await RequestAsync<CulqiList<T>>(HttpMethod.Get, url, options, requestOptions, cancellationToken);
            }
        }
#else
        protected IEnumerable<T> ListRequestAutoPaging<T>(
            string url,
            ListOptions options,
            RequestOptions requestOptions)
            where T : ICulqiEntity
        {
            var page = Request<CulqiList<T>>(HttpMethod.Get, url, options, requestOptions);

            options = options ?? new ListOptions();
            bool iterateBackward = false;

            if (!string.IsNullOrEmpty(options.Before) && string.IsNullOrEmpty(options.After))
            {
                iterateBackward = true;
            }

            while (true)
            {
                if (iterateBackward)
                {
                    page.Reverse();
                }

                string itemId = null;
                foreach (var item in page)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    itemId = ((IHasId)item).Id;
                    yield return item;
                }

                if (string.IsNullOrEmpty(itemId))
                {
                    break;
                }

                if (iterateBackward)
                {
                    options.Before = itemId;
                }
                else
                {
                    options.After = itemId;
                }

                page = Request<CulqiList<T>>(HttpMethod.Get, url, options, requestOptions);
            }
        }
#endif

        protected RequestOptions SetupRequestOptions(RequestOptions requestOptions)
        {
            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            requestOptions.BaseUrl = requestOptions.BaseUrl ?? BaseUrl;

            return requestOptions;
        }

        protected virtual string ClassUrl()
        {
            return BasePath;
        }

        protected virtual string InstanceUrl(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "The resource ID cannot be null or whitespace.",
                    nameof(id));
            }

            return $"{ClassUrl()}/{WebUtility.UrlEncode(id)}";
        }
    }
}
