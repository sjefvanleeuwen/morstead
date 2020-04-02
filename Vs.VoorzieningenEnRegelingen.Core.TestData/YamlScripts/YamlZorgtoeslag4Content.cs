namespace Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts
{
    public class YamlZorgtoeslag4Content
    {
        public readonly static string Body = @"Content:
 - key: stap.woonland
   vraag: Waar bent u woonachtig?
   titel: Selecteer uw woonland.
   tekst: Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.
   hint: Selecteer ""Anders"" wanneer het uw woonland niet in de lijst staat.
 - key: stap.woonsituatie.keuze.situatie.alleenstaande
   vraag: Wat is uw woonsituatie?
   titel: Wat is uw woonsituatie?
   tekst: Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.
   hint: Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft.
   optiontext: Alleenstaande
 - key: stap.woonsituatie.keuze.situatie.aanvrager_met_toeslagpartner
   optiontext: Aanvrager met toeslagpartner 
 - key: _____fakekeyexample_stap.stap.vermogensdrempel
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
 - key: stap.vermogensdrempel.keuze.situatie.hoger_dan_vermogensdrempel
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
   optiontext: Ja, mijn vermogen is **hoger** dan €114.776,00
 - key: stap.vermogensdrempel.keuze.situatie.lager_dan_vermogensdrempel
   optiontext: Nee, mijn vermogen is **lager** dan €114.776,00
 - key: _____fakekeyexample_stap.vermogensdrempel.keuze.situatie.alleenstaande.hoger_dan_vermogensdrempel
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
   optiontext: Ja, mijn vermogen is **hoger** dan €114.776,00
 - key: _____fakekeyexample_stap.stap.vermogensdrempel.keuze.situatie.aanvrager_met_toeslagpartner.hoger_dan_vermogensdrempel
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag. 
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige vermogensdrempel voor aanvragers met toeslagpartners is €145.136,00
   optiontext: Ja, het gezamenlijk vermogen is **hoger** dan €145.136,00
 - key: _____fakekeyexample.stap.vermogensdrempel.keuze.situatie.aanvrager_met_toeslagpartner.lager_dan_vermogensdrempel
   optiontext: Nee, het gezamenlijk vermogen is **lager** dan €145.136,00
 - key: stap.inkomensdrempel.keuze.situatie.hoger_dan_inkomensdrempel
   vraag: Is uw toetsingsinkomen hoger dan de inkomensdrempel?
   titel: Inkomensdrempel
   tekst: Wanneer u als alleenstaande meer inkomen heeft dan €29.562,00 per jaar, overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige inkomensdrempel voor alleenstaanden is €29.562,00 per jaar.
   optiontext: Ja, mijn inkomen is **hoger** dan €29.562,00
 - key: stap.inkomensdrempel.keuze.situatie.lager_dan_inkomensdrempel
   optiontext: Nee, mijn inkomen is **lager** dan €29.562,00
 - key: _____fakekeyexample.stap.inkomensdrempel.keuze.situatie.hoger_dan_inkomensdrempel.aanvrager_met_toeslagpartner
   vraag: Is uw toetsingsinkomen hoger dan de inkomensdrempel?
   titel: Inkomensdrempel
   tekst: Wanneer u samen met een toeslagpartner meer inkomen heeft dan €37.885,00 per jaar overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige inkomensdrempel voor aanvragers met toeslagpartners is €37.885,00 per jaar
   optiontext: Ja, het gezamenlijk inkomen is **hoger** dan €37.885,00
 - key: _____fakekeyexample.stap.inkomensdrempel.keuze.situatie.lager_dan_inkomensdrempel.aanvrager_met_toeslagpartner
   optiontext: Nee, het gezamenlijk inkomen is **lager** dan €37.885,00
 - key: stap.toetsingsinkomen.waarde.toetsingsinkomen
   vraag: Wat is uw toetsingsinkomen?
   titel: Toetsingsinkomen
   tekst: Vul uw toetsingsinkomen in. Gebruik een komma als scheidingsteken tussen euro's en centen.
     <br />Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst.
   hint: De huidige inkomensdrempel voor alleenstaanden is €29.562,00 per jaar.
 - key: _____fakekeyexample.stap.toetsingsinkomen.waarde.toetsingsinkomen.aanvrager_met_toeslagpartner
   vraag: Wat is uw toetsingsinkomen?
   titel: Gezamenlijk toetsingsinkomen
   tekst: Vul de som van uw toetsingsinkomen en het toetsingsinkomen van uw toeslagpartner in. Gebruik een komma als scheidingsteken tussen euro's en centen.
     <br />Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst.
   hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
";
//    - stap: alleenstaande.zorgtoeslag
//      vraag: Maandelijkse zorgtoeslag
//      titel: **Uw zorgtoeslag is €((double)_sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == ""zorgtoeslag"").Value).ToString(""#.00"").Replace('.', ',') + "" per maand.**
//      beschrijving: Met de door u ingevulde gegevens heeft u geen recht op zorgtoeslag. Voor meer informatie over zorgtoeslag in uw situatie, neem contact op met de Belastingdienst.
//      label: 
//      tag:
//      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
//    - stap: geenRecht
//      vraag: Geen Recht
//      titel: U heeft geen recht op zorgtoeslag.
//      beschrijving: De berekening is afgelopen. U kunt eventueel naar een vorige pagina gaan om een antwoord aan te passen.
//      label: 
//      tag:
//      hint: Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen.
//";
    }
}
