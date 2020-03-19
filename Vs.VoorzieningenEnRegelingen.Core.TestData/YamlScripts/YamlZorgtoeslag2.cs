namespace Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts
{
    /// <summary>
    /// Script used for mocking tests
    /// </summary>
    public static class YamlZorgtoeslag2
    {
        public readonly static string Body = @"# Minimal Script for Unit Tests
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 2.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: 1
   omschrijving: woonachtig in Nederland
   tekst: landrecht
   formule: binnenland
 - stap: 1
   situatie: niet(binnenland)
   omschrijving: woonachtig in een land met recht
   formule: selecteer(woonland)
   recht: woonlandfactor > 0
 - stap: 2 
   omschrijving: bepaal de woonsituatie
   formule: alleenstaande
 - stap: 3
   omschrijving: bepaal of het eigen vermogen groter is dan het maximaal_vermogen
   formule: niet(maximaal_vermogen)
   recht: niet(maximaal_vermogen) 
 - stap: 4
   omschrijving: bepaal of het inkomen groter is dan het toetsingsinkomen
   formule: niet(maximaal_toetsingsinkomen)
   recht: niet(maximaal_toetsingsinkomen)
 - stap: 5
   situatie: alleenstaande
   omschrijving: bepaal het eigen vermogen van de alleenstaande
   formule: eigen_vermogen
- stap: 5
   situatie: aanvrager_met_toeslagpartner
   omschrijving: bepaal het eigen vermogen van de aanvrager_met_toeslagpartner
   formule: eigen_vermogen
 - stap: 6
   situatie: alleenstaande
   omschrijving: bepaal het inkomen van de alleenstaande
   formule: toetsingsinkomen
 - stap: 6
   situatie: aanvrager_met_toeslagpartner
   omschrijving: bepaal het inkomen van de aanvrager_met_toeslagpartner
   formule: toetsingsinkomen
 - stap: 7
   omschrijving: toon de berekende zorgtoeslag
   formule: zorgtoeslag
formules:
 - maximaal_vermogen:
   - situatie: wel(alleenstaande)
     formule: 114776
   - situatie: niet(alleenstaande)
     formule: 145136
 - maximaal_toetsingsinkomen:
   - situatie: wel(alleenstaande)
     formule: 29562
   - situatie: niet(alleenstaande)
     formule: 37885
 - standaardpremie:
   - situatie: wel(alleenstaande)
     formule: 1609
   - situatie: niet(alleenstaande)
     formule: 3218
 - drempelinkomen:
     formule: 20941
 - normpremie:
   - situatie: alleenstaande
     formule: min(percentage(2.005) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 1189)
   - situatie: aanvrager_met_toeslagpartner
     formule: min(percentage(4.315) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 2314)
 - buitenland:
     formule: niet(woonland,'Nederland')
 - binnenland:
     formule: wel(woonland,'Nederland')
 - zorgtoeslag:
     - situatie: binnenland
       formule: round((standaardpremie - normpremie) / 12,2)
     - situatie: buitenland
       formule: round((standaardpremie - normpremie) * woonlandfactor / 12,2)
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
tabellen:
  - naam: woonlandfactoren
    woonland, factor:
      - [ Finland,             0.7161 ]
      - [ Frankrijk,           0.8316 ]
      - [ België,              0.7392 ]
      - [ Bosnië-Herzegovina,  0.0672 ]
      - [ Bulgarije,           0.0735 ]
      - [ Cyprus,              0.1363 ]
      - [ Denemarken,          0.9951 ]
      - [ Duitsland,           0.8701 ]
      - [ Estland,             0.2262 ]
      - [ Finland,             0.7161 ]
      - [ Frankrijk,           0.8316 ]
      - [ Griekenland,         0.2490 ]
      - [ Hongarije,           0.1381 ]
      - [ Ierland,             0.8667 ]
      - [ IJsland,             0.9802 ]
      - [ Italië,              0.5470 ]
      - [ Kaapverdië,          0.0177 ]
      - [ Kroatië,             0.1674 ]
      - [ Letland,             0.0672 ]
      - [ Liechtenstein,       0.9720 ]
      - [ Litouwen,            0.2399 ]
      - [ Luxemburg,           0.7358 ]
      - [ Macedonië,           0.0565 ]
      - [ Malta,               0.3574 ]
      - [ Marokko,             0.0193 ]
      - [ Montenegro,          0.0821 ]
      - [ Noorwegen,           1.3729 ]
      - [ Oostenrijk,          0.6632 ]
      - [ Polen,               0.1691 ]
      - [ Portugal,            0.2616 ]
      - [ Roemenië,            0.0814 ]
      - [ Servië,              0.0714 ]
      - [ Slovenië,            0.3377 ]
      - [ Slowakije,           0.2405 ]
      - [ Spanje,              0.4001 ]
      - [ Tsjechië,            0.2412 ]
      - [ Tunesië,             0.0292 ]
      - [ Turkije,             0.0874 ]
      - [ Verenigd Koninkrijk, 0.7741 ]
      - [ Zweden,              0.8213 ]
      - [ Zwitserland,         0.8000 ]
";
    }
}
