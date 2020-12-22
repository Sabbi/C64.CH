using C64.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Seeder.Pages
{
    public partial class Seed
    {
        private async Task AddGuestbook()
        {
            var rnd = new Random();
            for (var i = 0; i < 20; i++)
            {
                var rs = GetRandomStuff();
                var entry = new GuestbookEntry
                {
                    Comment = rs.Text,
                    Homepage = rs.Url,
                    Name = rs.Handle,
                    WrittenAt = DateTime.Now.AddDays(rnd.Next(-365, 0)),
                    Ip = RandomIp(),
                };

                await dbContext.GuestbookEntries.AddAsync(entry);
            }
        }

        private async Task AddLinks()
        {
            var rnd = new Random();
            for (var i = 0; i < 15; i++)
            {
                var rs = GetRandomStuff();
                var entry = new Link
                {
                    LinkCategoryId = rnd.Next(1, 6),
                    Url = rs.Url,
                    Name = rs.Handle,
                    Added = DateTime.Now.AddDays(rnd.Next(-365, 0)),
                };

                await dbContext.Links.AddAsync(entry);
            }
        }

        private async Task AddSiteNews()
        {
            var rnd = new Random();
            for (var i = 0; i < 10; i++)
            {
                var rs = GetRandomStuff();
                var entry = new SiteInfo
                {
                    PublishedAt = DateTime.Now.AddDays(rnd.Next(-365, 0)),
                    Show = true,
                    Text = rs.Text,
                    Title = rs.Text.Substring(0, 10),
                    Writer = rs.Handle
                };
                await dbContext.SiteInfos.AddAsync(entry);
            }
        }

        private async Task AddTools()
        {
            var json = System.IO.File.ReadAllText("Data/tools.json");
            var parsed = JsonSerializer.Deserialize<List<Tool>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await dbContext.Tools.AddRangeAsync(parsed);
        }
    }
}