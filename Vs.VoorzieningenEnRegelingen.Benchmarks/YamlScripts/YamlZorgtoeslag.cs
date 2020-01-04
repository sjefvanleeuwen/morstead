namespace Vs.VoorzieningenEnRegelingen.Benchmarks.YamlScripts
{
    /// <summary>
    /// Script used for mocking tests
    /// </summary>
    public static class YamlZorgtoeslag
    {
        public readonly static string Body = @"# Minimal Script for Unit Tests
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
   omschrijving: bepaal de standaard premie
   formule: standaardpremie
 - stap: 2
   omschrijving: bereken het gezamenlijke toestingsinkomen
   formule: toetsingsinkomen
 - stap: 3 
   omschrijving: bereken de normpremie
   formule: normpremie
 - stap: 4 
   situatie: binnenland
   omschrijving: bereken de zorgtoeslag wanneer men binnen nederland woont
   formule: zorgtoeslag
 - stap: 5
   situatie: buitenland
   omschrijving: bereken de zorgtoeslag wanner men in het buitenland woont
   formule: zorgtoeslag
formules:
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - maximaalvermogen:
   - situatie: alleenstaande
     formule: 114776
   - situatie: aanvrager_met_toeslagpartner
     formule: 145136
 - recht_op_zorgtoeslag: 
   - situatie: alleenstaande
     formule: toetsinginkomen <= 29562
   - situatie: aanvrager_met_toeslagpartner
     formule: toetsinginkomen <= 37885
 - drempelinkomen:
     formule: 20941
 - toetsingsinkomen: 
     formule: toetsingsinkomen_aanvrager + toetsingsinkomen_toeslagpartner
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
     formule: lookup('woonlandfactoren',woonland,'woonland','factor')
tabellen:
  - naam: maximaalvermogen
    situatie, waarde:
      - [ alleenstaande,                11476 ] 
      - [ aanvrager met toeslagpartner, 14536 ]
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
