﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace TableStorage.Abstractions.Tests
{
    public partial class TableStoreAsyncTests : IDisposable
    {
        private const string TableName = "TestTableAsync";
        private const string ConnectionString = "UseDevelopmentStorage=true";
        private readonly ITableStore<TestTableEntity> _tableStorage;

        public TableStoreAsyncTests()
        {
            _tableStorage = new TableStore<TestTableEntity>(TableName, ConnectionString);
        }

        public void Dispose()
        {
            _tableStorage.DeleteTableAsync().Wait();
        }

        [Fact]
        public async Task table_does_exist_then_exist_check_returns_true()
        {
            // Arrange
            await _tableStorage.DeleteTableAsync();
            await _tableStorage.CreateTableAsync();

            // Act
            var result = await _tableStorage.TableExistsAsync();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task table_does_not_exist_then_exist_check_returns_false()
        {
            // Arrange
            await _tableStorage.DeleteTableAsync();

            // Act
            var result = await _tableStorage.TableExistsAsync();

            // Assert
            result.Should().BeFalse();
        }
    }
}