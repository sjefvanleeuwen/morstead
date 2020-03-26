using System;
using System.Collections.Generic;
using System.Text;

namespace Vs.Cms.Core.Tests.TestYaml
{
    public static class ContentYamlTest1
    {
        public static string Body = @"Content:
 - key: stap.woonland.woonlandfactor
   vraag: Waar bent u woonachtig?
   titel: Selecteer uw woonland.
   tekst: Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.
   label: 
   tag:   
   hint: Selecteer ""Anders"" wanneer het uw woonland niet in de lijst staat.
 - key: stap.woonsituatie
   vraag: Wat is uw woonsituatie?
   titel: Wat is uw woonsituatie?
   tekst: Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.
   label: 
   hint: Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft.    
 - key: stap.woonsituatie.keuze.alleenstaande
   tran: ALeenstaande
 - key: stap.woonsituatie.keuze.aanvrager_met_toeslagpartner
   tekst: Aanvrager met toeslagpartner 
";
    }
}
