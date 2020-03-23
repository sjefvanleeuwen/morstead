namespace Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts
{
    public class YamlSituationalTests
    {
        public readonly static string Body = @"# Situational Evaluation Unit Test
stuurinformatie:
  onderwerp: unit test
  organisatie: unit test
  type: unit test
  domein: unit test
  versie: 1.0
  status: ontwikkel
  jaar: 2020
  bron: unit test
berekening:
 - stap: 1
   omschrijving: Wat is uw situatie?
   formule: situatie
 - stap: 2
   omschrijving: Bereken volgens situatie
   formule: berekening
 - stap: 3
   omschrijving: 2e Berekening volgens situatie
   formule: berekening2
 - stap: 4a
   omschrijving: specifieke formule vanuit flow situatie1
   situatie: situatie1
   formule: flowSituatie1
 - stap: 4b
   omschrijving: specifieke formule vanuit flow situatie1
   situatie: situatie2
   formule: flowSituatie2
 - stap: 5
   omschrijving: 3e Berekening die een specifieke vraag stelt
   formule: berekening3
formules:
 - situatie:
   - situatie: situatie1
     formule: 1
   - situatie: situatie2
     formule: 2
 - berekening:
   - situatie: situatie1
     formule: 1
   - situatie: situatie2
     formule: 2
 - berekening2:
   - situatie: situatie1
     formule: situatie * berekening + 2
   - situatie: situatie2
     formule: situatie * berekening * 2
 - berekening3:
   - situatie: situatie1
     formule: ((situatie * berekening) + 2) * vraag1
   - situatie: situatie2
     formule: ((situatie * berekening) + 2) * vraag2
 - flowSituatie1:
     formule: berekening3 * 10
 - flowSituatie2:
     formule: berekening3 * 100
";
    }
}
