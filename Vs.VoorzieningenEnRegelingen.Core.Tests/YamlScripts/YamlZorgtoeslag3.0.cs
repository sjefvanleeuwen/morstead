namespace Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts
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
  versie: 1.0
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
   omschrijving: Is uw toetsingsinkomen hoger  dan de inkomensdrempel van 29562,00 euro per jaar?
   formule: inkomensdrempel
   recht: wel(lager_dan_de_inkomensdrempel)
 - stap: 3
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Is uw gezamelijk toetsingsinkomen hoger dan de inkomensdrempel van 37885,00 euro per jaar?
   formule: inkomensdrempel
   recht: wel(lager_dan_de_inkomensdrempel)
formules:
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - lager_dan_inkomensdrempel:
   - situatie: lager_dan_de_inkomensdrempel
     formule: 1
   - situatie: hoger_dan_de_inkomensdrempel
     formule: 0
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
      - [ Anders,              0      ]
";
    }
}
