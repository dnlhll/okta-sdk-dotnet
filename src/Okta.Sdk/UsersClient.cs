﻿// <copyright file="UsersClient.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Sdk
{
    /// <summary>
    /// Provides methods that manipulate <see cref="User"/> resources, by communicating with the Okta Users API.
    /// </summary>
    public sealed partial class UsersClient : OktaClient, IUsersClient, IAsyncEnumerable<IUser>
    {
        /// <inheritdoc/>
        public IAsyncEnumerator<IUser> GetEnumerator() => ListUsers().GetEnumerator();

        /// <inheritdoc/>
        public Task<IUser> CreateUserAsync(CreateUserWithPasswordOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var user = new User
            {
                Profile = options.Profile,
                Credentials = new UserCredentials
                {
                    Password = new PasswordCredential { Value = options.Password },
                },
            };

            return CreateUserAsync(user, options.Activate, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IUserCredentials> ChangePasswordAsync(
            ChangePasswordOptions options,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var changePasswordRequest = new ChangePasswordRequest
            {
                OldPassword = new PasswordCredential { Value = options.CurrentPassword },
                NewPassword = new PasswordCredential { Value = options.NewPassword },
            };

            return ChangePasswordAsync(changePasswordRequest, userId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ChangeRecoveryQuestionAsync(string userId, ChangeRecoveryQuestionOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var newCredentials = new UserCredentials
            {
                Password = new PasswordCredential { Value = options.CurrentPassword },
                RecoveryQuestion = new RecoveryQuestionCredential
                {
                    Question = options.RecoveryQuestion,
                    Answer = options.RecoveryAnswer,
                },
            };

            return ChangeRecoveryQuestionAsync(newCredentials, userId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IResetPasswordToken> ResetPasswordAsync(
            string userId,
            bool? sendEmail = true,
            CancellationToken cancellationToken = default(CancellationToken))
            => ResetPasswordAsync(userId, null, sendEmail, cancellationToken);
    }
}
