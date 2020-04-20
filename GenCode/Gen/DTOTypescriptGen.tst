${
    using Typewriter.Extensions.Types;
    Template(Settings settings)
    {
        settings
            .IncludeProject("CMS.Models");
        settings.OutputExtension = ".ts";
        settings.OutputFilenameFactory = file => 
        {
            return $"outputTypescriptDTOs/{file.Name.Replace(".cs", ".ts")}";
        };
    }
    string ImportsList(Class c)
    {
        IEnumerable<Type> types = c.Properties
            .Select(p => p.Type)
            .Where(t => !t.IsPrimitive || t.IsEnum)
            .Select(t => t.IsGeneric ? t.TypeArguments.First() : t)
            .Where(t => t.Name != c.Name && t.Name != "DbGeography")
            .Distinct();
        return string.Join(Environment.NewLine, types.Select(t => $"import {{ {t.Name} }} from '@/models/{t.Name}';").Distinct());
    }
}
$Classes(CMS.Models.*)[
$ImportsList 

export interface $Name$TypeParameters { $Properties[
    $Name: $Type;]
}]