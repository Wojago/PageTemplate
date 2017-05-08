using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DCMA.ResourcePage.TemplateWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Uri redirectUrl;
            switch (SharePointContextProvider.CheckRedirectionStatus(Context, out redirectUrl))
            {
                case RedirectionStatus.Ok:
                    return;
                case RedirectionStatus.ShouldRedirect:
                    Response.Redirect(redirectUrl.AbsoluteUri, endResponse: true);
                    break;
                case RedirectionStatus.CanNotRedirect:
                    Response.Write("An error occurred while processing your request.");
                    Response.End();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string script = @"
            function chromeLoaded() {
                $('body').show();
            }

            //function callback to render chrome after SP.UI.Controls.js loads
            function renderSPChrome() {
                //Set the chrome options for launching Help, Account, and Contact pages
                var options = {
                    'appTitle': document.title,
                    'onCssLoaded': 'chromeLoaded()'
                };

                //Load the Chrome Control in the divSPChrome element of the page
                var chromeNavigation = new SP.UI.Controls.Navigation('divSPChrome', options);
                chromeNavigation.setVisible(true);
            }";

            //register script in page which shows the content when chrome was added and 
            Page.ClientScript.RegisterClientScriptBlock(typeof(Default), "BasePageScript", script, true);
        }

        protected void btnCreateResourcePage_Click(object sender, EventArgs e)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);
            using (var clctx = spContext.CreateUserClientContextForSPHost())
            {

                var PagesUrl = clctx.Web.Lists.GetByTitle("Site Pages");
                clctx.Load(PagesUrl.RootFolder, f => f.ServerRelativeUrl);
                clctx.Load(PagesUrl.RootFolder.Files);
                clctx.ExecuteQuery();
                string strUrl = string.Format("{0}/{1}", PagesUrl.RootFolder.ServerRelativeUrl, "Template" + ".aspx");
                File templateFile = clctx.Web.Lists.GetByTitle("Site Pages").RootFolder.Files.GetByUrl(strUrl);
                clctx.Load(templateFile, b => b.Exists);
                clctx.ExecuteQuery();
                try
                {
                    templateFile.CopyTo(PagesUrl.RootFolder.ServerRelativeUrl + "/" + TxtBoxPageName.Text + ".aspx", false);
                    clctx.ExecuteQuery();
                    Label1.Text = "<b>Page is created successfully<b>";
                }
                catch (Exception)
                {

                    Label1.Text = "<b>Page is not created with name provided, please try another name or contact system administrator if problem persists.<b>";
                }


            }


        }
    }
}