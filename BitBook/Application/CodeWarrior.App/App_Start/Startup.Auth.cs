﻿using System;
using CodeWarrior.Model;
using CodeWarrior.SharedLibrary.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using MongoDB.AspNet.Identity;
using Owin;
using CodeWarrior.App.Providers;

namespace CodeWarrior.App
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory =
                () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(MongoDbConfiguration.MongoConnection));

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: OAuthConfiguration.MicrosoftAuthClientId,
            //    clientSecret: OAuthConfiguration.MicrosoftAuthClientSecret);

            app.UseTwitterAuthentication(
                consumerKey: OAuthConfiguration.TwitterAuthConsumerKey,
                consumerSecret: OAuthConfiguration.TwitterAuthConsumerSecret);

            app.UseFacebookAuthentication(
                appId: OAuthConfiguration.FacebookAuthAppId,
                appSecret: OAuthConfiguration.FacebookAuthAppSecret);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = OAuthConfiguration.GoogleAuthClientId,
                ClientSecret = OAuthConfiguration.GoogleAuthClientSecret
            });
        }
    }
}