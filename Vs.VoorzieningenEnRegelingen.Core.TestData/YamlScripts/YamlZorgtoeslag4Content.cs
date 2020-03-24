namespace Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts
{
    public class YamlZorgtoeslag4Content
    {
        public readonly static string Body = @"#Content bestand voor YamlZorgtoeslag
content:
  - taal: nl-NL
    semanticKey:
    - stap: woonland
      vraag: Waar bent u woonachtig?
      titel: Selecteer uw woonland.
      beschrijving: Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: Selecteer ""Anders"" wanneer het uw woonland niet in de lijst staat.
    - stap: woonsituatie
      vraag: Wat is uw woonsituatie?
      titel: Wat is uw woonsituatie?
      beschrijving: Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft.
      opties:
      - alleenstaande: Alleenstaande
      - aanvrager_met_toeslagpartner: Aanvrager met toeslagpartner    
";
    }
}
