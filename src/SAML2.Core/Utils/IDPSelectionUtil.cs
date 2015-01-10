﻿using SAML2.Config;

namespace SAML2.Utils
{
    /// <summary>
    /// This delegate is used handling events, where the framework have several configured IDP's to choose from
    /// and needs information on, which one to use.
    /// </summary>
    /// <param name="ep">List of configured endpoints</param>
    /// <returns>The <see cref="IdentityProviderEndpoint"/> for the IDP that should be used for authentication</returns>
    public delegate IdentityProvider IdpSelectionEventHandler(IdentityProviders ep);

    /// <summary>
    /// Contains helper functionality for selection of IDP when more than one is configured
    /// </summary>
    public class IdpSelectionUtil
    {
        /// <summary>
        /// The event handler will be called, when no Common Domain Cookie is set, 
        /// no IdentityProviderEndpointElement is marked as default in the configuration,
        /// and no <c>idpSelectionUrl</c> is configured.
        /// Make sure that only one event handler is added, since only the last result of the event handler invocation will be used.
        /// </summary>
        public static event IdpSelectionEventHandler IdpSelectionEvent;

        /// <summary>
        /// Helper method for generating URL to a link, that the user can click to select that particular IdentityProviderEndpointElement for authorization.
        /// Usually not called directly, but called from <c>IdentityProviderEndpointElement.GetIDPLoginUrl()</c>
        /// </summary>
        /// <param name="idpId">Id of IDP that an authentication URL is needed for</param>
        /// <returns>A URL that can be used for logging in at the IDP</returns>
        public static string GetIdpLoginUrl(string idpId, Saml2Section config)
        {
            throw new System.NotImplementedException();
            // TODO: This should be needed eventually, but probably has to be handled outside of core
            //return string.Format("{0}?{1}={2}", config.ServiceProvider.Endpoints.DefaultSignOnEndpoint.LocalPath, Saml20SignonHandler.IdpChoiceParameterName, HttpUtility.UrlEncode(idpId));
        }

        /// <summary>
        /// Invokes the IDP selection event handler.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <returns>The <see cref="IdentityProvider"/>.</returns>
        internal static IdentityProvider InvokeIDPSelectionEventHandler(IdentityProviders endpoints)
        {
            return IdpSelectionEvent != null ? IdpSelectionEvent(endpoints) : null;
        }
    }
}
