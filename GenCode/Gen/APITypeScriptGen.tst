${
    using Typewriter.Extensions.WebApi;
    Template(Settings settings)
    {
        settings
            .IncludeProject("CMS.Web");
        
        settings.OutputExtension = ".ts";

        settings.OutputFilenameFactory = file => 
        {
            return $"outputTypescriptAPIs/{file.Name.Replace("Controller", "Api").Replace(".cs", ".ts")}";
        };
    }
    string apiName(Class c)
    {
        return c.Name.Replace("Controller", "Api");
    }
    string ImportModels(Class c)
    {
        IEnumerable<Type> types = c.Methods
            .Select(p => p.Type)
            .Where(t => (!t.IsPrimitive || t.IsEnum))
            .Select(t => t.IsGeneric ? t.TypeArguments.First() : t)
            .Where(t => t.Name != c.Name && t.Name != "DbGeography"  && t.Name != "Pagination")
            .Distinct();
        return string.Join(Environment.NewLine, types.Select(t => $"import {{ {t.Name} }} from './{t.Name}';").Distinct());
    }
    string ImportsList(Class objClass)
    {
        var apiSearchParam = "";
        List<string> lstType = new List<string>();
        // Get the methods in the Class
        var objMethods = objClass.Methods;
        // Loop through the Methdos in the Class
        foreach(Method m in objMethods)
        {
            // inport search param
            string methodType = m.Type.Name;
            if(m.Parameters.Any(x=>x.Type.Name == "Pagination")) {
                if (m.Type.Name == "IHttpActionResult" || m.Type.Name == "Task<IActionResult>" || m.Type.Name == "IActionResult") {
                    foreach (var a in m.Attributes) {
                        // Checks to see if there is an attribute to match returnType
                        if (a.Name == "ResponseType" || a.Name == "ProducesResponseType") {
                            string type = string.Empty;
                            bool isArray = a.Value.Contains("<");
                            bool isPaginatedResponse = a.Value.Contains("PaginatedResponse") ||  a.Value.Contains("PagedResult");

                            string formattedType = a.Value.Replace("<", "")
                                .Replace(">", "")
                                .Replace("typeof(", "")
                                .Replace(", 200", "")
                                .Replace(")", "");
                            string[] ar;
                            ar = formattedType.Split('.');
                            type = ar[ar.Length - 1];
                            
                            // mismatch on bool vs boolean
                            if (type == "bool") {
                                type = "boolean";
                            }
                            methodType = type;
                            break;
                        }
                    }
                }
                apiSearchParam += $"export interface {objClass.Name.Replace("Controller", "Api")}{m.Name}Params extends Pagination {{";

                foreach(Parameter p in m.Parameters.Where(x=>x.Attributes.Any()))
                {
                    // If the Paramater is not prmitive we need to add this to the Imports
                    if(p.Type.Name != "Pagination"){
                         apiSearchParam += $"\n   {p.Name}{((p.HasDefaultValue && p.DefaultValue == "null") || p.Type.IsNullable? "?":"")} : {p.Type.Name}";
                    }
                }

                apiSearchParam += "\n}\n";
            }

            // Loop through each Parameter in each method
            foreach(Parameter objParameter in m.Parameters)
            {
                // If the Paramater is not prmitive we need to add this to the Imports
                if(!objParameter.Type.IsPrimitive && objParameter.Type.Name != "Pagination"){
                    if(!lstType.Contains(objParameter.Type.Name))
                        lstType.Add(objParameter.Type.Name);
                }
            }

            // return type
            if (m.Type.Name == "IHttpActionResult" || m.Type.Name == "Task<IActionResult>" || m.Type.Name == "IActionResult") {
                foreach (var a in m.Attributes) {
                    // Checks to see if there is an attribute to match returnType
                    if (a.Name == "ResponseType" || a.Name == "ProducesResponseType") {
                        string type = string.Empty;
                        bool isArray = a.Value.Contains("<");
                        bool isPaginatedResponse = a.Value.Contains("PaginatedResponse") ||  a.Value.Contains("PagedResult");

                        string formattedType = a.Value
                            .Replace("<", "")
                            .Replace(">", "")
                            .Replace("typeof(", "")
                            .Replace(", 200", "")
                            .Replace(", 400", "")
                            .Replace("200", "")
                            .Replace("400", "")
                            .Replace(")", "");
                        string[] ar;
                        ar = formattedType.Split('.');
                        type = ar[ar.Length - 1];
                        if (!string.IsNullOrEmpty(type))
                        {
                            if(!lstType.Contains(type))
                                lstType.Add(type);
                        }
                    }
                }
            }
        }
        // Notice: As of now this will only return one import
        string str = string.Join(Environment.NewLine, lstType.Select(t => $"import {{ { t } }} from '@/models/{t}';").Distinct());
        str += "\n" + apiSearchParam;
        return str;
    }
    string ReturnType(Method m) {
        if (m.Type.Name == "IHttpActionResult" || m.Type.Name == "Task<IActionResult>" || m.Type.Name == "IActionResult") {
            string type = string.Empty;
            var apiName = m.Url().Split('?')[0].Split('/')[1];
            apiName = apiName.Replace(apiName.Substring(0,1), apiName.Substring(0,1).ToUpper());

            if(m.Name.Contains("Search"))
                type = "PaginatedResponse<"+ apiName + ">";
            else
                type = apiName;
            return type;
        }
        return m.Type.Name;
    }
    string NullAble(Parameter p) {
        return (p.HasDefaultValue && p.DefaultValue == "null") || p.Type.IsNullable? "?":"";
    }
    string ApiUrl(Method m) {
        string url = m.Url();
        if(m.HttpMethod() == "get" && m.Parameters.Any(x=>x.Type.Name == "Pagination")) {
            url = url.Split('?')[0];
        }
        return url;
    }
    string ApiParam(Method m) {
        string methodType = m.Type.Name;
        if(m.Parameters.Any(x=>x.Type.Name == "Pagination")) {
            if (m.Type.Name == "IHttpActionResult" || m.Type.Name == "Task<IActionResult>" || m.Type.Name == "IActionResult") {
                foreach (var a in m.Attributes) {
                    // Checks to see if there is an attribute to match returnType
                    if (a.Name == "ResponseType" || a.Name == "ProducesResponseType") {
                        string type = string.Empty;
                        bool isArray = a.Value.Contains("<");
                        bool isPaginatedResponse = a.Value.Contains("PaginatedResponse") ||  a.Value.Contains("PagedResult");

                        string formattedType = a.Value.Replace("<", "")
                            .Replace(">", "")
                            .Replace("typeof(", "")
                            .Replace(", 200", "")
                            .Replace(")", "");
                        string[] ar;
                        ar = formattedType.Split('.');
                        type = ar[ar.Length - 1];
                            
                        // mismatch on bool vs boolean
                        if (type == "bool") {
                            type = "boolean";
                        }
                        methodType = type;
                        break;
                    }
                }
            }
        }
        string str = m.Url();
        if(m.HttpMethod() == "get" && m.Parameters.Any(x=>x.Type.Name == "Pagination")) {
            str = string.Join(", ", m.Parameters.Where(x=>!x.Attributes.Any()).Select(x=> (x.Name + ((x.HasDefaultValue && x.DefaultValue == "null") || x.Type.IsNullable? "?":"") + ": " + x.Type )));
            var apiName = m.Url().Split('?')[0].Split('/')[1];
            str += (string.IsNullOrEmpty(str)?"":", ") + $"{apiName}{m.name}Params: {apiName.Replace(apiName.Substring(0,1),apiName.Substring(0,1).ToUpper())}Api{m.Name}Params";
        } else {    
           str = string.Join(", ", m.Parameters.Select(x=> (x.Name + ((x.HasDefaultValue && x.DefaultValue == "null") || x.Type.IsNullable? "?":"") + ": " + x.Type )));
        }
        return str;
    }
    string ApiParam2(Method m) {
        var apiName = m.Url().Split('?')[0].Split('/')[1];
        string str = m.RequestData() != "null"? ("," + m.RequestData()) : "";
        if(m.HttpMethod() == "get" && m.Parameters.Any(x=>x.Type.Name == "Pagination")) {
            str = ", { params: "+ apiName+m.name+"Params }";
        }
        return str;
    }
}
$Classes(:BaseApiController)[
import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
$ImportsList
class $apiName extends BaseApi {
    $Methods[
    $name($ApiParam) : Promise<$ReturnType> {
        return new Promise<$ReturnType>((resolve: any, reject: any) => {
            HTTP.$HttpMethod(`$ApiUrl`$ApiParam2)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }]
}
export default new $apiName();
]