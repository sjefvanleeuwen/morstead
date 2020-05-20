using BlazorMonaco;
using BlazorMonaco.Bridge;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        private StandaloneEditorConstructionOptions GetConstructionOptions(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "yaml",
                Value = @"# zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 5.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: woonland
   formule: woonlandfactor
   recht: woonlandfactor > 0
 - stap: woonsituatie
   keuze:
   - situatie: alleenstaande
   - situatie: aanvrager_met_toeslagpartner
 - stap: vermogensdrempel
   situatie: alleenstaande, aanvrager_met_toeslagpartner
   keuze:
   - situatie: hoger_dan_vermogensdrempel
   - situatie: lager_dan_vermogensdrempel
   recht: lager_dan_vermogensdrempel
 - stap: inkomensdrempel
   situatie: alleenstaande, aanvrager_met_toeslagpartner
   keuze:
   - situatie: hoger_dan_inkomensdrempel
   - situatie: lager_dan_inkomensdrempel
   recht: lager_dan_inkomensdrempel
 - stap: toetsingsinkomen
   situatie: alleenstaande, aanvrager_met_toeslagpartner
   waarde: toetsingsinkomen
   recht: toetsingsinkomen < toetsingsinkomensdrempel
 - stap: zorgtoeslag
   situatie: alleenstaande, aanvrager_met_toeslagpartner
   formule: zorgtoeslag
formules:
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - toetsingsinkomensdrempel:
   - situatie: alleenstaande
     formule: 29562
   - situatie: aanvrager_met_toeslagpartner
     formule: 37885
 - toetsingsvermogensdrempel:
   - situatie: alleenstaande
     formule: 114776
   - situatie: aanvrager_met_toeslagpartner
     formule: 145136
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
      - [ nederland,           1.0    ]
      - [ belgië,              0.7392 ]
      - [ bosnië-herzegovina,  0.0672 ]
      - [ bulgarije,           0.0735 ]
      - [ cyprus,              0.1363 ]
      - [ denemarken,          0.9951 ]
      - [ duitsland,           0.8701 ]
      - [ estland,             0.2262 ]
      - [ finland,             0.7161 ]
      - [ frankrijk,           0.8316 ]
      - [ griekenland,         0.2490 ]
      - [ hongarije,           0.1381 ]
      - [ ierland,             0.8667 ]
      - [ ijsland,             0.9802 ]
      - [ italië,              0.5470 ]
      - [ kaapverdië,          0.0177 ]
      - [ kroatië,             0.1674 ]
      - [ letland,             0.0672 ]
      - [ liechtenstein,       0.9720 ]
      - [ litouwen,            0.2399 ]
      - [ luxemburg,           0.7358 ]
      - [ macedonië,           0.0565 ]
      - [ malta,               0.3574 ]
      - [ marokko,             0.0193 ]
      - [ montenegro,          0.0821 ]
      - [ noorwegen,           1.3729 ]
      - [ oostenrijk,          0.6632 ]
      - [ polen,               0.1691 ]
      - [ portugal,            0.2616 ]
      - [ roemenië,            0.0814 ]
      - [ servië,              0.0714 ]
      - [ slovenië,            0.3377 ]
      - [ slowakije,           0.2405 ]
      - [ spanje,              0.4001 ]
      - [ tsjechië,            0.2412 ]
      - [ tunesië,             0.0292 ]
      - [ turkije,             0.0874 ]
      - [ verenigd koninkrijk, 0.7741 ]
      - [ zweden,              0.8213 ]
      - [ zwitserland,         0.8000 ]
      - [ anders,              0      ]"
            };
        }
    }
}
