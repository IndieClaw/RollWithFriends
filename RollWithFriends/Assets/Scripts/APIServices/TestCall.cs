using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class TestCall : MonoBehaviour
{
    #region Fields and properties
    private static readonly HttpClient _client = new HttpClient();

    private static readonly string _remoteUrl = "https://rollwithfriendsapp.azurewebsites.net";
    private static readonly string tokenUrl = "https://rollwithfriendsapp.azurewebsites.net/.auth/me";
    #endregion

    #region Public methods

    #endregion


    #region Private methods	

    void Start()
    {
        print(GetTest().Description);
    }

    void Update()
    {

    }

    Item GetTest()
    {        
        //var token = _client.GetStringAsync($"{tokenUrl}").Result;
        //var idToken = "AQABAAAAAACQN9QBRU3jT6bcBQLZNUj7muXZhGDWfj2bkyn9YVZQqVq5SETOw8cGckjpWeSQnzXrhn3CoILfMUInLXsNDXpSh3T2daX4VQXDEhr5n675MH_aaNdSKG5HPRI-AVMBlVFlNk_dnuK8oRsYk3WeUgQNjmr7hEr9jD9giX4R0WgDNX19ZqqyBThs1jrpx9Q1Pucn35fQ3XlePf0s4wojtZkvxY3j-MYVqxiSWKJg0hJ6PZKx_aV2vwLDlgtuiJQ6Y7Lh_NZvg_Wh1ydU9g8NEEvcNsGXNxMrcUUT7-ASm4UaexpMiVFQcyjEqkbywMlu_8YQrtIUa08Sau3a830WdKS2tcyekoXIfDLZN5cihfp_PyK6Uan5mth-zS1iOI_hy8fT4SbsxyzJUMYya0BJbSw6TwnYIYR4px-4iqDA5h0chp6vedaG1xYfYFejK6oeEOqo1XFyNTpKmmLXyCLVF2zx5wJr457MYZJvDtW1PUdR2Ek_OebddWdZCge4ISAXcuob7WXCIrMHTGFKh_-GoNL0Z3tQjQ3I_y1M3TRHrPp255hORzTU_SWvrCEMEU08JOWMdga75IPyfXT4k-iJiCVB86bf2QOP_SoJFQ9_kwmQV3Cyvep3daqqLsqdeWdo7aNZKPG_WfWDgKomcMFVRt7AMbC7fqTWzhxfqcdSyPpHWhyest-8JFk5WtLipCqvURokWJRCw2KIpwmq90Y8Ry75fhtmWYVOkoNqNYs-wrmikFWajqq2-TLnQKlxZXUG6EWX4SA4_7MD5rjoB2xvVS6IwHSsUsKDYadl-o5pRs2O20QWJq0gqO5beqlmXYNPvmaGHdbfPYCuLEerofk7mWXaNC7FTBK9K-HeGvUTqUa34hWzz3hz9biscwGyGXOEPqGD5fqO1JfppK765GctcRj2sn07XgCZUBPUeAzUi0ev-pQiGFCgTW-4bjmu_ZJ0oAPyRzU1QE_Psub5IpJ5jI5Zxn-XsUYryZwLif0P7pgzNUF6_Ia5NjplhGpZEDiaYD3hF4frjJLYtBdfTKFBBz5gvVoP-AOtUuCAyoqqJhM2qCjFkrNG_8toiNFCM3mHuJUsE9ytsdmD8WSc3vPO4vL9P3Xq82SCqWeOSDUV95E55oc8g48LVlsjaLHrm-UT4FVksX4xE6KV-qPPP9pVONZQ2hKiRSmt-hP234Fdm3iPSrTCnm8i0iWPLCVvhC5tEdw6YAxzEf09A7gsAk402kdjwSB8wyKtkJnP73mlBvncm2uRyN8CbhQOkFheea8I9GWyANElMDXPlPjF28_4fdcmIAA";
        //print(token);
        //------------------
        //_client.DefaultRequestHeaders.Accept.Clear();
        // _client.DefaultRequestHeaders.Authorization =
        //     new AuthenticationHeaderValue("Bearer", Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"]);

        // _client.DefaultRequestHeaders.Authorization =
        //     new AuthenticationHeaderValue("Bearer", idToken);        
        // _client.DefaultRequestHeaders.Add("key","asdkakkakda");

        var data = _client.GetStringAsync($"{_remoteUrl}/item/1").Result;
        return JsonConvert.DeserializeObject<Item>(data);        
    }

    #endregion
}

public class Item
{

    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Completed { get; set; }

    public Item()
    {

    }

    public Item(string id, string name, string description, bool completed)
    {
        Id = id;
        Name = name;
        Description = description;
        Completed = completed;
    }
}
