﻿// <copyright file="TestableOktaClient.cs" company="Okta, Inc">
// Copyright (c) 2014 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Extensions.Logging.Abstractions;
using Okta.Sdk.Configuration;
using Okta.Sdk.Internal;

namespace Okta.Sdk.UnitTests
{
    public class TestableOktaClient : OktaClient
    {
        public static readonly OktaClientConfiguration DefaultFakeConfiguration = new OktaClientConfiguration
        {
            OrgUrl = "https://fake.example.com",
            Token = "foobar",
        };

        //public TestableOktaClient(IDataStore dataStore, OktaClientConfiguration configuration = null, RequestContext requestContext = null)
        //    : base(dataStore, configuration ?? DefaultFakeConfiguration, requestContext ?? new RequestContext())
        //{
        //}

        public TestableOktaClient(IRequestExecutor requestExecutor)
            : base(
                new DefaultDataStore(requestExecutor, new DefaultSerializer(), new ResourceFactory(null, null), NullLogger.Instance),
                DefaultFakeConfiguration,
                new RequestContext())
        {
        }
    }
}
