namespace Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts
{
    /// <summary>
    /// Script used for mocking tests
    /// </summary>
    public static class YamlZorgtoeslag3
    {
        public readonly static string Body = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 3.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: 1
   omschrijving: Waar bent u woonachtig?
   formule: woonlandfactor
   recht: woonlandfactor > 0
 - stap: 2
   omschrijving: Wat is uw woonsituatie?
   formule: standaardpremie
 - stap: 3
   situatie: alleenstaande
   omschrijving: Is uw vermogen hoger dan de drempelwaarde?
   formule: vermogensdrempel
   recht: vermogensdrempel = 1
 - stap: 3
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Is uw gezamenlijk vermogen hoger dan de drempelwaarde?
   formule: vermogensdrempel
   recht: vermogensdrempel = 1
 - stap: 4
   situatie: alleenstaande
   omschrijving: Is uw toetsingsinkomen hoger dan de inkomensdrempel?
   formule: inkomensdrempel
   recht: inkomensdrempel = 1
 - stap: 4
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Is uw gezamenlijk toetsingsinkomen hoger dan de inkomensdrempel?
   formule: inkomensdrempel
   recht: inkomensdrempel = 1
 - stap: 5
   situatie: alleenstaande
   omschrijving: Wat is uw toetsingsinkomen?
   formule: toetsingsinkomen
   recht: toetsingsinkomen_aanvrager < 29562
 - stap: 5
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Wat is uw gezamenlijk toetsingsinkomen?
   formule: toetsingsinkomen
   recht: toetsingsinkomen_gezamenlijk < 37885
 - stap: 6
   omschrijving: Maandelijkse zorgtoeslag
   formule: zorgtoeslag
formules:
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - vermogensdrempel:
   - situatie: hoger_dan_de_vermogensdrempel
     formule: 0   
   - situatie: lager_dan_de_vermogensdrempel
     formule: 1
 - inkomensdrempel:
   - situatie: hoger_dan_de_inkomensdrempel
     formule: 0
   - situatie: lager_dan_de_inkomensdrempel
     formule: 1
 - toetsingsinkomen:
   - situatie: alleenstaande
     formule: toetsingsinkomen_aanvrager
   - situatie: aanvrager_met_toeslagpartner
     formule: toetsingsinkomen_gezamenlijk
 - drempelinkomen:
     formule: 20941
 - normpremie:
   - situatie: alleenstaande     
     formule: min(percentage(2.005) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 1189)
   - situatie: aanvrager_met_toeslagpartner
     formule: min(percentage(4.315) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 2314)
 - zorgtoeslag:
     formule: round((standaardpremie - normpremie) * woonlandfactor / 12,2)
tabellen:
  - naam: woonlandfactoren
    woonland, factor:
      - [ Nederland,           1.0    ]
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
      - [ Anders,              0      ]";
    }
}
