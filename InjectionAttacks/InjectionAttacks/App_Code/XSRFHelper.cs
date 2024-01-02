using InjectionAttacks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttacks
{

    public class XSRFHelper
    {
        public void Validate(Page page, HiddenField hiddenField)
        {
            //1. Kişi login olduğunda ya da ilk kez request gönderdiğinde bir token üret.
            //2. Bu token'ı hem sunucuda hem de istemcide kaydet.
            //3. Eğer request post ise o zaman sakladığın token ile gelen token'i karşılaştır.

            if (!page.IsPostBack)
            {

                var token = Guid.NewGuid();
                hiddenField.Value = token.ToString();
                page.Session["token"] = token;


            }
            else
            {
                var compareGuid = (Guid)page.Session["token"];
                if (hiddenField.Value != compareGuid.ToString())
                {

                    page.Response.Write("yakaladık saldırıyı....");
                }
            }
        }
    }


}