using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaqulukaNercerewhelbeba.Data;
using BaqulukaNercerewhelbeba.Model;
using BaqulukaNercerewhelbeba.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaqulukaNercerewhelbeba.Business
{
    /// <summary>
    /// ��Rss���ͷ��͸���MatterMost����
    /// </summary>
    public class RssCourier
    {
        private readonly ILogger<RssCourier> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RssCourier(ILogger<RssCourier> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// ����
        /// </summary>
        public async void Start()
        {
            while (true)
            {
                _logger.LogInformation($"{DateTime.Now} ��ʼ��ȡ����");
                var minTime = TimeSpan.FromDays(1);

                using (var serviceScope = _serviceProvider.CreateScope())
                {
                    await using var blogContext = serviceScope.ServiceProvider.GetService<BlogContext>();

                    await foreach (var blog in blogContext.Blog)
                    {
                        var blogDescriptionList = await GetBlog(blog.BlogRss);

                        foreach (var blogDescription in blogDescriptionList)
                        {
                            var distance = DateTime.Now - blogDescription.Time;

                            if (distance > minTime)
                            {
                                _logger.LogInformation($"{blogDescription.Title} ����ʱ�� {blogDescription.Time} ���뵱ǰ{distance.TotalDays:0}");
                                continue;
                            }

                            foreach (var matterMost in blogContext.Blog.Where(temp => temp.BlogRss == blog.BlogRss).Select(temp => new MatterMost(temp.MatterMostUrl)))
                            {
                                // ���û�з��������mattermost�������
                                if (blogContext.PublishedBlogList.All(temp =>
                                    temp.Blog != blogDescription.Url && temp.MatterMost != matterMost.Url))
                                {
                                    _logger.LogInformation($"��{matterMost.Url}����{blogDescription.Title}����");
                                    matterMost.SendText($"[{blogDescription.Title}]({blogDescription.Url})");
                                    _logger.LogInformation($"���� {blogDescription.Title}");

                                    blogContext.PublishedBlogList.Add(new PublishedBlog()
                                    {
                                        Blog = blogDescription.Url,
                                        MatterMost = matterMost.Url,
                                        Time = DateTime.Now,
                                    });

                                    blogContext.SaveChanges();
                                }
                                else
                                {
                                    _logger.LogInformation($"{blogDescription.Title}���{matterMost.Url}������");
                                }
                            }
                        }


                    }

                    blogContext.PublishedBlogList.RemoveRange(blogContext.PublishedBlogList.Where(publishedBlog => (DateTime.Now - publishedBlog.Time) > minTime));

                    blogContext.SaveChanges();
                }

                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }


        private static async Task<List<BlogDescription>> GetBlog(string url)
        {
            var task = Task.Run(async () =>
            {
                var blogList = new List<BlogDescription>();
                var newsFeedService = new NewsFeedService(url);
                var syndicationItems = await newsFeedService.GetNewsFeed();
                foreach (var syndicationItem in syndicationItems)
                {
                    var description =
                        syndicationItem.Description.Substring(0, Math.Min(200, syndicationItem.Description.Length));
                    var time = syndicationItem.Published;
                    var uri = syndicationItem.Links.FirstOrDefault()?.Uri;

                    if (time < syndicationItem.LastUpdated)
                    {
                        time = syndicationItem.LastUpdated;
                    }

                    blogList.Add(new BlogDescription()
                    {
                        Title = syndicationItem.Title,
                        Description = description,
                        Time = time.DateTime,
                        Url = uri?.AbsoluteUri
                    });
                }

                return blogList;
            });

            var t = Task.Delay(TimeSpan.FromMinutes(10));
            await Task.WhenAny(t, task);

            if (task.IsCompleted)
            {
                return await task;
            }

            return new List<BlogDescription>();
        }
    }
}