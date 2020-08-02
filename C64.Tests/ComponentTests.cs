using Bunit;
using C64.Data.Entities;
using C64.FrontEnd.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace C64.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void DetailComments()
        {
            using var ctx = new TestContext();

            var mock = new Mock<ILogger<WriteComment>>();

            var comments = new List<Comment>();

            comments.Add(new Comment
            {
                CommentedAt = DateTime.MinValue,
                Ip = "1.1.1.1",
                CommentId = 1,
                Text = "TestComment",
                User = new User { UserName = "Sabbi" },
            });

            // Act
            var cut = ctx.RenderComponent<DetailComments>(("Comments", comments));

            // Assert
            cut.Markup.Contains("<p class=\"mt-0\">TestComment</p>");
            cut.Markup.Contains($"<small>Sabbi @ {DateTime.MinValue}</small>");
        }
    }
}