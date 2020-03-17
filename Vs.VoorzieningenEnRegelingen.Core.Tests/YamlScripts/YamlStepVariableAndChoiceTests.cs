namespace Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts
{
    public class YamlStepVariableAndChoiceTests
    {
        public readonly static string Body = @"# Step Variable Unit Test
stuurinformatie:
  onderwerp: step variable unit test
  organisatie: unit test
  type: unit test
  domein: unit test
  versie: 1.0
  status: ontwikkel
  jaar: 2020
  bron: unit test
berekening:
 - stap: Wat is je leeftijd?
   waarde: leeftijd
   recht: leeftijd > 17
 - stap: Geef je drankkeuze aan
   keuze:
   - situatie: cider
   - situatie: bier
 - stap: Bestel een drankje
   formule: prijs
formules:
 - prijs:
   - situatie: cider
     formule: lookup('ciders',drankje,'drankje','prijs', 0)
   - situatie: bier
     formule: lookup('bieren',drankje,'drankje','prijs', 0)
tabellen:
  - naam: bieren
    drankje, prijs:
      - [ Leffe Blond (30cl),               1.29 ]
      - [ Hoegaarden Witbier (30cl),        1.09 ]
  - naam: ciders
    drankje, prijs:
      - [ Apple Bandit Cider (30cl),        0.99 ]
      - [ Savanna Premium Cider Dry (30cl), 1.89 ]
";
    }
}
