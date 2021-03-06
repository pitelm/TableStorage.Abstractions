﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using TableStorage.Abstractions.Models;
using TableStorage.Abstractions.Store;
using TableStorage.Abstractions.Tests.Helpers;
using Xunit;

namespace TableStorage.Abstractions.Tests.Store
{
    public partial class TableStoreAsyncTests : IDisposable
    {
        private const string TableName = "TestTableAsync";
        private const string ConnectionString = "UseDevelopmentStorage=true";
        private readonly ITableStore<TestTableEntity> _tableStorage;
        private readonly TableStorageOptions _tableStorageOptions = new TableStorageOptions();

        public TableStoreAsyncTests()
        {
            _tableStorage = new TableStore<TestTableEntity>(TableName, ConnectionString, _tableStorageOptions);
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