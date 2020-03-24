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
    - stap: alleenstaande.vermogensdrempel
      vraag: Is uw vermogen hoger dan de drempelwaarde?
      titel: Vermogensdrempel
      beschrijving: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.<br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
      opties:
      - hoger_dan_vermogensdrempel: Ja, mijn vermogen is <strong>hoger</strong> dan €114.776,00
      - lager_dan_vermogensdrempel: Nee, mijn vermogen is <strong>lager</strong> dan €114.776,00
    - stap: aanvrager_met_toeslagpartner.vermogensdrempel
      vraag: Is uw vermogen hoger dan de drempelwaarde?
      titel: Vermogensdrempel
      beschrijving: Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.<br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: De huidige vermogensdrempel voor aanvragers met toeslagpartners is €145.136,00
      opties:
      - hoger_dan_vermogensdrempel: Ja, het gezamenlijk vermogen is <strong>hoger</strong> dan €145.136,00
      - lager_dan_vermogensdrempel: Nee, het gezamenlijk vermogen is <strong>lager</strong> dan €145.136,00
    - stap: alleenstaande.inkomensdrempel
      vraag: Is uw toetsingsinkomen hoger dan de inkomensdrempel?
      titel: Inkomensdrempel
      beschrijving: Wanneer u als alleenstaande meer inkomen heeft dan €29.562,00 per jaar, overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.<br />Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: De huidige inkomensdrempel voor alleenstaanden is €29.562,00 per jaar.
      opties:
      - hoger_dan_inkomensdrempel: Ja, mijn inkomen is <strong>hoger</strong> dan €29.562,00
      - lager_dan_inkomensdrempel: Nee, mijn inkomen is <strong>lager</strong> dan €29.562,00
    - stap: aanvrager_met_toeslagpartner.inkomensdrempel
      vraag: Is uw toetsingsinkomen hoger dan de inkomensdrempel?
      titel: Inkomensdrempel
      beschrijving: Wanneer u samen met een toeslagpartner meer inkomen heeft dan €37.885,00 per jaar overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.<br />Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: De huidige inkomensdrempel voor aanvragers met toeslagpartners is €37.885,00 per jaar.
      opties:
      - hoger_dan_inkomensdrempel: Ja, het gezamenlijk inkomen is <strong>hoger</strong> dan €37.885,00
      - lager_dan_inkomensdrempel: Nee, het gezamenlijk inkomen is <strong>lager</strong> dan €37.885,00
    - stap: alleenstaande.toetsingsinkomen
      vraag: Wat is uw toetsingsinkomen?
      titel: Toetsingsinkomen
      beschrijving: Vul uw toetsingsinkomen in. Gebruik een komma als scheidingsteken tussen euro's en centen.<br />Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
    - stap: aanvrager_met_toeslagpartner.toetsingsinkomen
      vraag: Wat is uw toetsingsinkomen?
      titel: Gezamenlijk toetsingsinkomen
      beschrijving: Vul de som van uw toetsingsinkomen en het toetsingsinkomen van uw toeslagpartner in. Gebruik een komma als scheidingsteken tussen euro's en centen.<br />Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst.
      label: 
      tag:
      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
    - stap: alleenstaande.zorgtoeslag
      vraag: Maandelijkse zorgtoeslag
      titel: <strong>Uw zorgtoeslag is €((double)_sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == ""zorgtoeslag"").Value).ToString(""#.00"").Replace('.', ',') + "" per maand.</strong>
      beschrijving: Met de door u ingevulde gegevens heeft u geen recht op zorgtoeslag. Voor meer informatie over zorgtoeslag in uw situatie, neem contact op met de Belastingdienst.
      label: 
      tag:
      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
    - stap: geenRecht
      vraag: Geen Recht
      titel: U heeft geen recht op zorgtoeslag.
      beschrijving: De berekening is afgelopen. U kunt eventueel naar een vorige pagina gaan om een antwoord aan te passen.
      label: 
      tag:
      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
";
    }
}
