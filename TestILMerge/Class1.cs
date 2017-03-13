using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System.IO;

namespace TestILMerge
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(context.UserId);

            string json = @"{
                'CPU': 'Intel',
                'PSU': '500W',
                'Drives': [
                  'DVD read/writer'
                  /*(broken)*/,
                  '500 gigabyte hard drive',
                  '200 gigabype hard drive'
                ]
            }";


            trace.Trace("Start");
           
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    trace.Trace("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                }
                else
                {
                    trace.Trace("Token: {0}", reader.TokenType);
                }
            }

            trace.Trace("End");
        }
    }
}
