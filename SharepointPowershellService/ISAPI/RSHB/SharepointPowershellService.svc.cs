using FuckingExternalWrapperToSuppressCompilerError;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SharepointPowershellService.ISAPI.RSHB
{
    [BasicHttpBindingServiceMetadataExchangeEndpoint]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single,
        IgnoreExtensionDataObject = true,
        IncludeExceptionDetailInFaults = true)]
    class SharepointPowershellService : ISharepointPowershellService
    {
        private const string resvar = "result";
        private const string fullmsg = "Runspaces collection is full!";
        public BlockingCollection<FuckingExternalStruct> Runspaces { get; set; } =
            new BlockingCollection<FuckingExternalStruct>(
                new WantThisFuckingSimpleConcurrentColletionToBeOutOfTheBox<FuckingExternalStruct>(),
                10
                );
        public dynamic RunScriptInstance(string scriptName, dynamic payload)
        {
            dynamic output = new ExpandoObject() as IDictionary<string, object>;
            if (Runspaces.IsCompleted)
            {
                output["error"] = fullmsg;
                return output;
            }
            try
            {
                output["type"] = "message";
                //using (SPSite site = new SPSite("request"))
                //{
                    //using (SPWeb web = site.AllWebs["web"])
                    //{
                        var iss = InitialSessionState.Create();

                        var rs = RunspaceFactory.CreateRunspace(iss);
                        rs.Open();
                        rs.SessionStateProxy.SetVariable("this", this);
                        //rs.SessionStateProxy.SetVariable("site", site);
                        //rs.SessionStateProxy.SetVariable("web", web);
                        rs.SessionStateProxy.SetVariable(resvar, payload);
                        rs.SessionStateProxy.SetVariable("payload", payload);

                        var pp = rs.CreatePipeline();
                        pp.Commands.AddScript("http://win-ilm3n2d1r60/_layouts/15/SharepointPowershellService/" + scriptName + ".ps1");

                        var fck = new FuckingExternalStruct { Rs = rs };

                        if (Runspaces.TryAdd(fck, 1000))
                        {
                            pp.Invoke();
                            output = rs.SessionStateProxy.GetVariable(resvar);
                            Runspaces.TryTake(out fck);
                        }
                        else
                        {
                            output["error"] = fullmsg;
                        }
                        pp.Dispose();
                        rs.Close();
                        rs.Dispose();
                    //}
                //}
                //var stringReader = new StringReader(output);
                //var jReader = new JsonTextReader(stringReader);
                //var jsonSerializer = new JsonSerializer();
                //var json = jsonSerializer.Deserialize<Dictionary<string, object>>(jReader);
                //return json.AsDynamic();
                //var response = new ExpandoObject() as IDictionary<string, Object>;
            }
            catch { }

            return output;

        }
    }
}
