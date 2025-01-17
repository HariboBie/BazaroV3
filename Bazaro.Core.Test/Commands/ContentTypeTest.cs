﻿using Bazaro.Test.Base;
using Bazaro.Web.Models;
using System.Threading.Tasks;
using Xunit;
using Bazaro.Web.Services;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bazaro.Core.Test.Commands
{
    public class ContentTypeTest : TestBase, IClassFixture<DatabaseBaseFixture>
    {
        private readonly ContentTypeSevice _service;

        public ContentTypeTest(DatabaseBaseFixture fixture) : base(fixture)
        {
            _service = _scope.GetInstance<ContentTypeSevice>();

            CreateDatabaseOnTimeData();
        }

        protected override async void DatabaseOneTimeData()
        {
            _context.Add(new ContentType
            {
                Title = "Test",
                Created = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task InsertContentTypePassingTest()
        {
            var oldData = await _context.Set<ContentType>().ToListAsync();

            _context.RemoveRange(_context.Set<ContentType>());
            await _context.SaveChangesAsync();

            Assert.Empty(_context.Set<ContentType>());

            await _service.Insert(new Web.Services.Commands.ContentTypes.InsertContentType.Command
            {
                Title = "Test2"
            });

            var data = await _context.Set<ContentType>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            var item = data.First();

            Assert.Equal("Test2", item.Title);
#pragma warning disable xUnit2002 // Do not use null check on value type
            Assert.NotNull(item.Created);
#pragma warning restore xUnit2002 // Do not use null check on value type

            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateContentTypePassingTest()
        {
            var dataId = _context.Set<ContentType>().First().Id;

            Assert.NotEmpty(_context.Set<ContentType>());

            await _service.Update(new Web.Services.Commands.ContentTypes.UpdateContentType.Command
            {
                Id = dataId,
                Title = "Test2",
            });

            var data = await _context.Set<ContentType>().ToListAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);

            var item = data.First();

            Assert.Equal("Test2", item.Title);
#pragma warning disable xUnit2002 // Do not use null check on value type
            Assert.NotNull(item.Updated);
#pragma warning restore xUnit2002 // Do not use null check on value type
        }

        [Fact]
        public async Task DeleteContentTypePassingTest()
        {
            var data = _context.Set<ContentType>().First();
            var oldData = data;

            Assert.NotEmpty(_context.Set<ContentType>());

            await _service.Delete(new Web.Services.Commands.ContentTypes.DeleteContentType.Command
            {
                Id = data.Id
            });

            Assert.Empty(_context.Set<ContentType>());

            _context.AddRange(oldData);
            await _context.SaveChangesAsync();
        }
    }
}
