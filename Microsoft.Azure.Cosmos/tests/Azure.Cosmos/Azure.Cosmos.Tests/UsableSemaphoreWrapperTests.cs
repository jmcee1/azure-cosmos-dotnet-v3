﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
namespace Azure.Cosmos.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsableSemaphoreWrapperTests
    {
        [TestMethod]
        public async Task NotDisposedAfterUsing()
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
            using(await semaphore.UsingWaitAsync())
            {
                ;
            }

            // Normal flow
            await semaphore.WaitAsync();
            semaphore.Release();

            semaphore.Dispose();
        }
    }
}
