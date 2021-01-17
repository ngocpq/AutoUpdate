using System.Collections.Generic;

namespace UpdateServer.Entities
{
    public class Command
    {
        public Command(string name)
        {
            Name = name;
            Parameters = new Dictionary<string, string>();
        }
        public string Name { get; set; }
        public Dictionary<string,string> Parameters { get; set; }        

        public void SetParameter(string name,string value)
        {
            Parameters[name] = value;
        }
        public string GetParameterValue(string name)
        {
            return Parameters[name];
        }

        public void SetParameters(params string[] paramsNameValuePairs)
        {
            for(int i = 0; i < paramsNameValuePairs.Length / 2; i++)
            {
                Parameters[paramsNameValuePairs[i*2]] = paramsNameValuePairs[i*2+1];
            }
            
        }
    }
}