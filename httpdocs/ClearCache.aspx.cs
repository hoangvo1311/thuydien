using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class ClearCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> keys = new List<string>();


        
        // retrieve application Cache enumerator

        IDictionaryEnumerator enumerator = Cache.GetEnumerator();



        // copy all keys that currently exist in Cache

        while (enumerator.MoveNext())
        {

            keys.Add(enumerator.Key.ToString());

        }



        // delete every key from cache

        for (int i = 0; i < keys.Count; i++)
        {

            Cache.Remove(keys[i]);

        }
    }
}
