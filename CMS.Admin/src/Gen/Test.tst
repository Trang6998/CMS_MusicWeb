${
    using Typewriter.Extensions.WebApi;
    Template(Settings settings)
    {
        settings
            .IncludeProject("CMS.Web");
        
        settings.OutputExtension = ".ts";

        settings.OutputFilenameFactory = file => 
        {
            return $"outputApi/{file.Name.Replace("Controller", "Api").Replace(".cs", ".ts")}";
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
            .Where(t => !t.IsPrimitive || t.IsEnum)
            .Select(t => t.IsGeneric ? t.TypeArguments.First() : t)
            .Where(t => t.Name != c.Name && t.Name != "DbGeography")
            .Distinct();
        return string.Join(Environment.NewLine, types.Select(t => $"import {{ {t.Name} }} from './{t.Name}';").Distinct());
    }
    string ImportsList(Class objClass)
    {
        var ImportsOutput = "";
        // Get the methods in the Class
        var objMethods = objClass.Methods;
        // Loop through the Methdos in the Class
        foreach(Method objMethod in objMethods)
        {
            // Loop through each Parameter in each method
            foreach(Parameter objParameter in objMethod.Parameters)
            {
                // If the Paramater is not prmitive we need to add this to the Imports
                if(!objParameter.Type.IsPrimitive){
                    ImportsOutput = objParameter.Type.Name;
                }
            }
        }
        // Notice: As of now this will only return one import
        return  $"import {{ { ImportsOutput } }} from '@/models/{ImportsOutput}';";
    }
    string ReturnType(Method m) {
        if (m.Type.Name == "IHttpActionResult") {
            foreach (var a in m.Attributes) {
                // Checks to see if there is an attribute to match returnType
                if (a.Name == "ResponseType") {
                    string type = string.Empty;
                    bool isArray = a.Value.Contains("<");
                    bool isPaginatedResponse = a.Value.Contains("PaginatedResponse");

                    string formattedType = a.Value.Replace("<", "")
                        .Replace(">", "")
                        .Replace("typeof(", "")
                        .Replace(")", "");
                    string[] ar;
                    ar = formattedType.Split('.');
                    type = ar[ar.Length - 1];
                    if(isPaginatedResponse) {
                        type = "PaginatedResponse<" + type + ">";
                    } else if (isArray) {
                        type += "[]";
                    }
                    // mismatch on bool vs boolean
                    if (type == "bool") {
                        type = "boolean";
                    }
                    return type;
                }
            }
            return "void";
        }
        return m.Type.Name;
    }
    string NullAble(Parameter p) {
        return p.Type.IsNullable? "?":"";
    }
}
$Classes(ThongBao*)[
import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
$ImportsList
class $apiName extends BaseApi {
    $Methods[
    $name($Parameters[$Name$NullAble: $Type][, ]) {
        return new Promise<$ReturnType>((resolve: any, reject: any) => {
            HTTP({ url: `$Url`, method: "$HttpMethod", data: $RequestData })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }]
}
export default new $apiName();]