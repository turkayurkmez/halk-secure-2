﻿using InjectionAttacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XSRFHelper xSRFHelper = new XSRFHelper();
        xSRFHelper.Validate(this, HiddenField1);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}