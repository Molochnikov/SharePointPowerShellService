using System.Collections.Concurrent;
using System.IO;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SharepointPowershellService.ISAPI.RSHB
{
    [ServiceContract]
    interface ISharepointPowershellService
    {
       /* BlockingCollection<Runspace> Runspaces
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }*/
        [OperationContract]
        [WebInvoke(UriTemplate = "{scriptName}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        dynamic RunScriptInstance(string ScriptName, dynamic payload);
    }
}
