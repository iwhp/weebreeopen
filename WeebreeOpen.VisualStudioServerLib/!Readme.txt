============================================================================================================
https://www.visualstudio.com/integrate/get-started/auth/oauth#scopes
http://integrate.visualstudio.com/
https://www.visualstudio.com/en-us/integrate/api/overview
http://blogs.msdn.com/b/bharry/archive/2014/05/12/a-new-api-for-visual-studio-online.aspx
------------------------------------------------------------------------------------------------------------
http://vsozendesk.codeplex.com/
https://vsorest.codeplex.com/
------------------------------------------------------------------------------------------------------------
https://ardimedia.visualstudio.com/defaultcollection/bvd.li.web/_apis/wit/queries?$depth=1
https://ardimedia.visualstudio.com/defaultcollection/_apis/wit/workitems?ids=877,878&fields=System.Id,System.Title,System.State,Microsoft.VSTS.Scheduling.RemainingWork
------------------------------------------------------------------------------------------------------------
https://ardimedia.visualstudio.com/defaultcollection/bvd.li.web/_apis/wit/classificationnodes
https://ardimedia.visualstudio.com/DefaultCollection/b96dfa60-840f-4da5-a420-d4cb57352598/_apis/wit/classificationNodes/Iterations?$depth=2
============================================================================================================

============================================================================================================
VISUAL STUDIO APP REGISTRATION
------------------------------------------------------------------------------------------------------------
https://app.vssps.visualstudio.com/profile/view
https://app.vssps.visualstudio.com/app/view?clientId=e3dc2183-e99c-43d0-8996-60897b1f8482
https://app.vssps.visualstudio.com/app/register

Company Info
> Company Name:           Weebree
> Company Website:        www.weebree.com
> Terms of Service URL:   none
> Privacy Statement URL:  none

Application Info
> Application Name:       Weebree
> Description:            Manage your tasks.
> Application             Website: http://www.weebree.com
> Authorization           Callback URL: https://www.weebree.com/signin-vso
> Scopes:                 ALL

Register Info
> App ID:                 E3DC2183-E99C-43D0-8996-60897B1F8482
> App Secret:             eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im9PdmN6NU1fN3AtSGpJS2xGWHo5M3VfVjBabyJ9.eyJjaWQiOiJlM2RjMjE4My1lOTljLTQzZDAtODk5Ni02MDg5N2IxZjg0ODIiLCJjc2kiOiI1ODBiOWM0Mi02NTY0LTRhMmMtYTE5Mi0yZTMxYjFlMTIwODMiLCJuYW1laWQiOiI2NGZjYjFkMS1mNDUwLTQ5MDYtYTY0Yy01OTZjNmVlYzRlMDgiLCJpc3MiOiJhcHAudnNzcHMudmlzdWFsc3R1ZGlvLmNvbSIsImF1ZCI6ImFwcC52c3Nwcy52aXN1YWxzdHVkaW8uY29tIiwibmJmIjoxNDI2NDQxNjY0LCJleHAiOjE0NTc5Nzc2NjR9.kRoUEm3u3V0JOERuN1JS7er-cy0WF7VyOyZz4UBYhvypkAGESbzyRanEuxeG9lE1xil1rPyzY7_dfAeqPmLnQdduUUoRRiPbWLIkwFddNkenxLtVC1oZrvf4CuvUveEqtKc6GRr2262VMhQC2o3NDZJJfAtZiD6C2G_iadO70sDEMIK2EONdx7e6OFereYMabmMPQiusIoanIOQ9YrIafzi1NWWr55Up0v_aFHvHFTIOuCSpejbub-WjdIfTUgSd-quxEyI8emR57MIGyMP9lTfIZJiowoSHb8rG5f-UuX8Fcy5FARRTwgoH2MfXPZpOwUnggObPN9TsKsAe1W1rtA
> Authorize URL:          https://app.vssps.visualstudio.com/oauth2/authorize
> Access Token URL:       https://app.vssps.visualstudio.com/oauth2/token
> Authorized Scopes:      vso.build_execute vso.chat_manage vso.code_manage vso.test_write vso.work_write
============================================================================================================

============================================================================================================
BouncyCastle.Crypto
------------------------------------------------------------------------------------------------------------
For documentation and mailing lists, see the project web site: http://www.bouncycastle.org/csharp/
Contact me directly for package/build issues: www.soygul.com/contact
© 2011, Bouncy Castle Project
============================================================================================================

============================================================================================================
CLONED FROM: https://vsorest.codeplex.com
============================================================================================================

============================================================================================================
public static async void GetBuilds()
{
    try
    {
        var username = "username";
        var password = "password";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", username, password))));

            using (HttpResponseMessage response = client.GetAsync(
                        "https://{account}.visualstudio.com/DefaultCollection/_apis/build/builds").Result)
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
============================================================================================================
