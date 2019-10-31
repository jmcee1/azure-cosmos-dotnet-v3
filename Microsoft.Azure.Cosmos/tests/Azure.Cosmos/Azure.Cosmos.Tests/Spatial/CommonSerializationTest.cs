﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Azure.Cosmos.Test.Spatial
{
    using System;

    using Azure.Cosmos.Spatial;
    using Azure.Cosmos.Spatial.Converters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    /// <summary>
    /// Serialization tests applying to all geometry types.
    /// </summary>
    [TestClass]
    public class CommonSerializationTest
    {
        /// <summary>
        /// Tests that incorrect JSON throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void TestInvalidJson()
        {
            string json = @"{""type"":""Poi}";
            var point = JsonConvert.DeserializeObject<Point>(json);
        }

        /// <summary>
        /// Tests that coordinates cannot be null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestNullCoordinates()
        {
            string json = @"{""type"":""Point"",""coordinates"":null}";
            var point = JsonConvert.DeserializeObject<Point>(json);
        }

        /// <summary>
        /// Tests that type cannot be null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestNullType()
        {
            string json = @"{""type"":null, ""coordinates"":[20, 30]}";
            var point = JsonConvert.DeserializeObject<Point>(json);
        }

        /// <summary>
        /// Tests that type cannot be null and uses <see cref="GeometryJsonConverter"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestNullTypeGeometry()
        {
            string json = @"{""type"":null, ""coordinates"":[20, 30]}";
            var point = JsonConvert.DeserializeObject<Geometry>(json);
        }

        /// <summary>
        /// Tests bounding box with not even number of coordinates.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestBoundingBoxWithNonEvenNumberOfCoordinates()
        {
            string json = @"{""type"":""Point"", ""coordinates"":[20, 30], ""bbox"":[0, 0, 0, 5, 5]}";
            var point = JsonConvert.DeserializeObject<Point>(json);
        }

        /// <summary>
        /// Tests bounding box with insufficient number of coordinates.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestBoundingBoxWithNotEnoughCoordinates()
        {
            string json = @"{""type"":""Point"", ""coordinates"":[20, 30], ""bbox"":[0, 0]}";
            var point = JsonConvert.DeserializeObject<Point>(json);
        }

        /// <summary>
        /// Tests bounding box which is null.
        /// </summary>
        [TestMethod]
        public void TestNullBoundingBox()
        {
            string json = @"{""type"":""Point"", ""coordinates"":[20, 30], ""bbox"":null}";
            var point = JsonConvert.DeserializeObject<Point>(json);
            Assert.IsNull(point.BoundingBox);
        }

        /// <summary>
        /// Tests empty coordinates.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestEmptyCoordinates()
        {
            string json = @"{
                    ""type"":""Point"",
                    ""coordinates"":[],
                    }";
            JsonConvert.DeserializeObject<Point>(json);
        }
    }
}
