# ![logo](./images/logo.svg) Virtual Society

## Urukagina Rule Engine
[![Maintainability](https://api.codeclimate.com/v1/badges/12cb1c194f0c722e7485/maintainability)](https://codeclimate.com/github/sjefvanleeuwen/virtual-society-urukagina/maintainability)
[![Build Status](https://leeuwens.visualstudio.com/Urukagina/_apis/build/status/sjefvanleeuwen.virtual-society-urukagina?branchName=master)](https://leeuwens.visualstudio.com/Urukagina/_build/latest?definitionId=1&branchName=master)
[![Test Status](https://img.shields.io/azure-devops/tests/leeuwens/Urukagina/1?failed_label=bad&passed_label=good&skipped_label=n%2Fa)](https://leeuwens.visualstudio.com/Urukagina/_build/latest?definitionId=1&branchName=master)
[![Coverage Status](https://img.shields.io/azure-devops/coverage/leeuwens/Urukagina/1)](https://leeuwens.visualstudio.com/Urukagina/_build/latest?definitionId=1&branchName=master)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/sjefvanleeuwen/virtual-society-urukagina/graphs/commit-activity)
[![License: MIT](https://img.shields.io/badge/License-MIT-0298c3.svg)](https://github.com/sjefvanleeuwen/virtual-society-urukagina/blob/master/LICENSE)
[![Website shields.io](https://img.shields.io/website-up-down-green-red/http/shields.io.svg)](https://regelingen.azurewebsites.net/)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.me/sjefvanleeuwen)
[![OpenAPI](https://img.shields.io/badge/openapi-oas%203.0-green)](https://regelingenservice.azurewebsites.net/swagger)

Uru-ka-gina, Uru-inim-gina of Iri-ka-gina (Sumerisch: 𒌷𒅗𒄀𒈾 URU-KA-gi.na; ca. 24e eeuw voor Christus, korte chronologie)
was een heerser (ensi) van de stadstaat Lagash in Mesopotamië. Hij nam de titel van koning aan en beweerde goddelijk te zijn 
benoemd bij de val van zijn corrupte voorganger Lugalanda.

Hij is vooral bekend om zijn hervormingen om corruptie te bestrijden, die soms worden genoemd als **het eerste voorbeeld van 
een wettelijke code** in de geschiedenis. Hoewel de eigenlijke tekst niet is ontdekt, kan veel van de inhoud ervan vermoed 
worden uit andere gevonden verwijzingen ernaar. Daarin stelde hij weduwen en wezen vrij van belastingen; dwong de stad om
begrafeniskosten te betalen (inclusief de rituele voedsel- en drankoffers voor de reis van de doden naar de lagere wereld); 
en besloten dat de rijken zilver moeten gebruiken bij de aankoop van de armen, en als de armen niet willen verkopen, 
kan de machtige man (de rijke man of de priester) hem niet dwingen dit te doen.

### Features

* Portabel, volledig ontkoppeld van databases.
* Semantisch geoptimaliseerd om regelingen in korte en leesbare notaties weer te geven en uit te voeren (No Code).
* Koppelbare formulieren middels Question Answer aangestuurd vanuit de rule engine.

## YAML

Virtual Society heeft voor haar **low code** strategie een configuratie taal ontwikkelt in YAML om regelingen eenvoudig in te brengen binnen
haar systeem. Het voordeel van deze taal is dat er met sterk verkorte anotaties regels uitgevoerd kunnen worden.

## Low Code v.s. No Code

Virtual Society hecht veel waarde aan Low Code. No Code daarentegen kan een negatief effect hebben op de ontwikkeltijd van een systeem.
Zo werkt No code bijvoorbeeld prima voor het in elkaar klikken van een user interface, daarentegen is regelgeving zeer semantisch en kan men
beter tot configureren overgaan binnen een taal die geoptimaliseerd is daartoe. Zo'n taal wordt ook wel een Domain Specific Languag genoemd 
(DSL). DSL's zijn in dit geval efficienter dan No Code. Virtual Society kiest er eveneens voor de DSL in YAML te representeren. De reden 
hiervoor is dat DEV-OPS omgevingen voor zowel technische ICT infrastructuur (OPS) als infrastructure as code (DEV) (IAC) uitdrukken in YAML. 
De stap naar de business wordt hierdoor verkleind binnen de BUS-DEV-OPS cycle. Dit is van groot belang binnen continous delivery in de OTAP.
straat. De YAML's van vitual society bevatten dan ook stuurinformate om dit voortbrenginsproces te ondersteunen.

## YAML structuur

### Stuurinformatie

```yaml
stuurinformatie:
  onderwerp: <<beknopte beschrijving>>
  organisatie: <<instantie,gemeente of organisatie>>
  type: <<toeslagen>>
  domein: <<zorg, werk of inkomen>>
  versie: <<major>>.<<minor>>>.<<revision>>
  status: <<ontwikkel,test,acceptatie,productie>>
  periode: <<Begindatum>> <<Einddatum>
  bron: <<permalink>>
```

#### Onderwerp

Geef hier een beknopte omschrijving van de regel

#### Organisatie

Geef hier de naam van de organisatie weer. 

#### Type

Geeft hier type van de uitvoering/regelgeving op.

#### Domein

Geef hier het werkdomein op.

#### Versie

Virtual Society adviseert om binnen versiebeheer in combinatie met status de volgende werkwijze aan te houden:

##### Revision

Ophogen na bevindingen uit de functionele en/of gebruikers acceptatie testers (FAT/GAT) binnen de acceptatie test omgeving in de OTAP straat.

##### Minor

Ophogen na bevindingen uit de testers binnen een Agile Team die werken aan hetzelfde probleem domein binnen de ontwikkel omgeving in de OTAP straat.

##### Major

Ophogen bij een volgende (mogelijke) oplevering uit de ontwikkel omgeving binnen de OTAP straat. Herhaal hierbij de eerder genoemde werkwijze.

#### Status

Geeft een indicatie van status yaml document. Ontwikkel, Test, Accpetatie, Productie

#### Periode

De periode van gegevens waarover het Yaml document berekeningen mag uitvoeren.

#### Bron

Een verwijzing naar regelgeving document waar de regels uitgelegd worden.

### Formules

Middels formules kunnen berekeningen gemaakt worden rondom (situationele) classificeringen welke voortkomen uit normen en formules.
Binnen de YAML worden formules geencapsuleerd binnen de volgende YAML tag:

```yaml
formule:
```

#### Formule

```yaml
formule:
 <<classificatie>>: literale formule
```

#### Situationele Formule

```yaml
formule:
 <<classificatie>>: 
   - situatie: <<classificering>>
     formule: <<literale formule>>
   - situatie: <<classificering>>
     formule: <<literale formule>>
```

### YAML Voorbeeld Zorgtoeslag (2019)

<sup>YAML deelrepresentatie</sup>
```yaml
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
```

De zorgtoeslag is een bijdrage in de kosten van de zorgverzekering. Of en hoeveel zorgtoeslag uw klant
krijgt, hangt af van:
* de standaardpremie
* de normpremie
* het (gezamenlijke) toetsingsinkomen
* het (gezamenlijke) vermogen

#### Vermogen mag niet te hoog zijn
Heeft uw klant of zijn eventuele toeslagpartner vermogen, zoals spaargeld en beleggingen? Als het
(gezamenlijke) vermogen op 1 januari 2019 hoger is dan de vrijstellingsgrens, heeft uw klant het hele jaar
geen recht op zorgtoeslag. Per persoon is de vermogensvrijstelling € 30.360. Daarnaast geldt een
vrijstellingsbedrag van € 84.416. Dit bedrag geldt voor uw klant en zijn eventuele toeslagpartner samen.
In de tabel staat hoeveel vermogen uw klant maximaal mag hebbenop 1 januari 2019.
Buitenlands vermogen telt ook mee. 

| situatie                     | maximaal vermogen |   
|------------------------------|-------------------|
| Alleenstaande                |         € 114.776 |
| Aanvrager met toeslagpartner |         € 145.136 | 
|                              |                   |

<sup>YAML deelrepresentatie</sup>
```yaml
tabellen:
  - naam: maximaalvermogen
    situatie, waarde:
      - [ alleenstaande,                11476 ] 
      - [ aanvrager met toeslagpartner, 14536 ]
```

#### Berekening
Uw klant heeft recht op zorgtoeslag als de standaardpremie hoger is dan de normpremie. U berekent de
zorgtoeslag van uw klant in 5 stappen.

<sup>YAML deelrepresentatie</sup>
```yaml
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
```

##### Stap 1: bepaal de standaardpremie

De standaardpremie is voor 2019 vastgesteld op € 1.609. Bij een aanvrager met een toeslagpartner wordt
tweemaal de standaardpremie genomen (€ 3.218).

<sup>YAML representatie</sup>
```yaml
formules:
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
```

##### Stap 2: bereken het gezamenlijke toetsingsinkomen

|                                 |               |
|---------------------------------|---------------|
| Toetsingsinkomen aanvrager      | €             |
| Toetsingsinkomen toeslagpartner | €             |
|                                 | ___________ + |
| Gezamenlijk toetsingsinkomen    | €             | 

<sup>YAML representatie</sup>
```yaml
 - toetsingsinkomen: 
     formule: toetsingsinkomen_aanvrager + toetsingsinkomen_toeslagpartner
```

Uw klant heeft geen recht op zorgtoeslag als het toetsingsinkomen hoger is dan:
• € 29.562 (aanvrager zonder toeslagpartner)
• € 37.885 (aanvrager met toeslagpartner)

<sup>YAML deelrepresentatie</sup>
```yaml
 - recht_op_zorgtoeslag: 
   - situatie: alleenstaande
     formule: toetsinginkomen <= 29562
   - situatie: aanvrager_met_toeslagpartner
     formule: toetsinginkomen <= 37885
```

Let op!
Woont uw klant buiten Nederland? Dan is het wereldinkomen het toetsingsinkomen.

#### Het volledige script:

```yaml
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
```
#### Service RESTful API Conversatie

Bovenstaande berekening bestaat uit een aantal stappen. Bij iedere stap vraagt Urukagina om variabelen die niet gevonden konden worden in
het script of welke niet eerder doorgestuurd werden aan de API. De converstatie gebeurd in JSON format. De stappen uit de converstatie 
kunnen afgespeeld worden met bijgeleverde [POSTMAN](https://www.postman.com) bestanden.

Hier is een screenshot van postman met de bijgeleverde bestanden:
![logo](./images/postman.png)

Om een idee te geven van een conversatie hebben we de JSON's uit die conversatie
geconverteerd naar YAML zodat ze leesbaar/compacter zijn voor deze readme. Er zijn twee typen conversaties:

1. **Geautomatiseerd**<br />
Bij geautomatiseerde processen is het mogelijk om alles beschikbare variabelen die nodig zijn voor de berekening van te voren aan te de API
door te geven. Dit reduceert round-trip time en biedt daarmee een hogere performance voor batch berekeningen. 

2. **User Centric**<br />
Bij user centricity is het mogelijk een conversatie/assistant te bouwen en deze aan te laten sturen middels een zgn. Question Answer principe
Bij iedere stap geeft de API aan welke input er verwacht wordt.

In dit voorbeeld gebruiken we de User Centric approach.

##### Zorgtoeslag 2019 berekening stap 1

In stap 1 wordt uitsluitend als startpunt het uit te voeren YAML script meegegeven. In dit geval wordt verwezen naar een URL waar het YAML
script met de berekening van de zorgtoeslag staat.

Het volgende bericht wordt naar de API service gestuurd:

```yaml
Config: https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml
```

De API Server geeft als antwoord:

```yaml
isError: false
message: 
stacktrace:
- step:
    name: '1'
    description: bepaal de standaard premie
    formula: standaardpremie
    situation: ''
    isSituational: false
  exception: 
parameters: []
questions:
  sessionId: ''
  parameters:
  - name: alleenstaande
    value: Situation
    type: UnresolvedType
  - name: aanvrager_met_toeslagpartner
    value: Situation
    type: UnresolvedType
```

De reactie bevat geen fout **isError=false**. Dit betekent dat het YAML script goed is uitgevoerd, onder **step** kan men zien dat het
script is gestopt bij de eerste stap in de berekening, namelijk bij het uitvoeren van de formule **standaardpremie**. De API service vewacht
nu een invoer van de client applicatie.

##### Zorgtoeslag 2019 berekening stap 2

Één van de twee situaties dienen nu aangegeven te worden om de standaardpremie verder te kunnen berekenen. Er wordt gekozen voor **alleenstaande**.

[x] Alleenstaande
[ ] Aanvrager met toeslagpartner

Het volgende bericht wordt naar de API service gestuurd:

```yaml
---
Config: https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml
Parameters:
- Name: alleenstaande
  Value: ja
```

De API Server geeft als antwoord (sommige data is weggelaten voor leesbaarheid):

<sup>YAML deelrepresentatie</sup>
```yaml
---
isError: false
message: 
stacktrace:
- step:
    name: '1' ......
- step:
    name: '2'
    description: bereken het gezamenlijke toestingsinkomen
    formula: toetsingsinkomen
    situation: ''
    isSituational: false
  exception: 
parameters:
- name: alleenstaande
  value: true
  type: Boolean
- name: standaardpremie
  value: 1609
  type: Double
questions:
  sessionId: ''
  parameters:
  - name: toetsingsinkomen_aanvrager
    value: unresolved
    type: String
```

##### Zorgtoeslag 2019 berekening stap 3

Het volgende bericht wordt naar de API service gestuurd:

```yaml
Config: https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml
Parameters:
- Name: alleenstaande
  Value: ja
- Name: toetsingsinkomen_aanvrager
  Value: '19000'
```

De API Server geeft als antwoord (sommige data is weggelaten voor leesbaarheid):

<sup>YAML deelrepresentatie</sup>
```yaml
isError: false
message: 
stacktrace:
- step:
    name: '1' ..........
  exception: 
- step:
    name: '2'
    description: bereken het gezamenlijke toestingsinkomen
    formula: toetsingsinkomen
    situation: ''
    isSituational: false
  exception: 
parameters:
- name: alleenstaande
  value: true
  type: Boolean
- name: toetsingsinkomen_aanvrager
  value: 19000
  type: Double
- name: standaardpremie
  value: 1609
  type: Double
questions:
  sessionId: ''
  parameters:
  - name: toetsingsinkomen_toeslagpartner
    value: unresolved
    type: String
```



##### Zorgtoeslag 2019 berekening stap 4

```yaml
Config: https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml
Parameters:
- Name: alleenstaande
  Value: ja
- Name: toetsingsinkomen_aanvrager
  Value: '19000'
- Name: toetsingsinkomen_toeslagpartner
  Value: '0'
```

De API Server geeft als antwoord (sommige data is weggelaten voor leesbaarheid):

<sup>YAML deelrepresentatie</sup>
```yaml
isError: false
message: 
stacktrace:
- step:
    name: '1' ..........
- step:
    name: '2' ..........
- step:
    name: '3'
    description: bereken de normpremie
    formula: normpremie
    situation: ''
    isSituational: false
  exception: 
parameters:
- name: alleenstaande
  value: true
  type: Boolean
- name: toetsingsinkomen_aanvrager
  value: 19000
  type: Double
- name: toetsingsinkomen_toeslagpartner
  value: 0
  type: Double
- name: standaardpremie
  value: 1609
  type: Double
- name: toetsingsinkomen
  value: 19000
  type: Double
- name: drempelinkomen
  value: 20941
  type: Double
- name: normpremie
  value: 419.86704999999995
  type: Double
questions:
  sessionId: ''
  parameters:
  - name: woonland
    value: unresolved
    type: String
```

##### Zorgtoeslag 2019 berekening stap 5

```yaml
Config: https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml
Parameters:
- Name: alleenstaande
  Value: ja
- Name: toetsingsinkomen_aanvrager
  Value: '19000'
- Name: toetsingsinkomen_toeslagpartner
  Value: '0'
- Name: woonland
  Value: Nederland
```

Eindantwoord, volledig:

```yaml
isError: false
message: 
stacktrace:
- step:
    name: '1'
    description: bepaal de standaard premie
    formula: standaardpremie
    situation: ''
    isSituational: false
  exception: 
- step:
    name: '2'
    description: bereken het gezamenlijke toestingsinkomen
    formula: toetsingsinkomen
    situation: ''
    isSituational: false
  exception: 
- step:
    name: '3'
    description: bereken de normpremie
    formula: normpremie
    situation: ''
    isSituational: false
  exception: 
- step:
    name: '4'
    description: bereken de zorgtoeslag wanneer men binnen nederland woont
    formula: zorgtoeslag
    situation: binnenland
    isSituational: true
  exception: 
parameters:
- name: alleenstaande
  value: true
  type: Boolean
- name: toetsingsinkomen_aanvrager
  value: 19000
  type: Double
- name: toetsingsinkomen_toeslagpartner
  value: 0
  type: Double
- name: woonland
  value: Nederland
  type: String
- name: standaardpremie
  value: 1609
  type: Double
- name: toetsingsinkomen
  value: 19000
  type: Double
- name: drempelinkomen
  value: 20941
  type: Double
- name: normpremie
  value: 419.86704999999995
  type: Double
- name: binnenland
  value: true
  type: Boolean
- name: zorgtoeslag
  value: 99.09
  type: Double
- name: buitenland
  value: false
  type: Boolean
questions: 
```

## Techniek

### YAML Interpreter

De YAML interpreter is een programma dat instructies uitvoert die zijn geschreven in een taal op hoog niveau, in dit geval dient de eerder
beschreven YAML als input. De interpreter is geschreven in C# en heeft 2 doelstellingen:

* Het interpreteren van de YAML en deze direct uit te voeren.
* Het interpreteren van de YAML en een Software Development Kit (SDK) aan te bieen om de YAML te kunnen transpilen naar een andere programmeer taal.

#### Programmeertaal
De interpreter is in C# geschreven (Dot Net Core). Dot Net Core is een opensource versie van Microsoft .NET. C# voldoet aan de internationale
ECMA standaard (ECMA 334). ECMA is een standaardorganisatie voor informatie- en communicatiesystemen. Het verwierf zijn huidige naam in 1994, 
toen de European Computer Manufacturers Association (ECMA) zijn naam veranderde om het wereldwijde bereik en de activiteiten van de organisatie
weer te geven.

Meer informatie over ECMA-334 kan hier gevonden worden.
https://www.ecma-international.org/publications/files/ECMA-ST/ECMA-334.pdf

### YAML Transpiler

Under construction. Het volledige YAML script kan getranspiled worden naar een compileerbare taal. Momenteel is er een YAML naar C# regelingen transpiler. 
Het voordeel van een dergelijke transpiler is dat de resulterende source code uitgevoerd kan worden op een computer systeem en kan helpen bij:

* Het versnelt uitvoeren van regelgeving logica t.b.v. algoritmisch onderzoek, waaronder inputs en outputs classificering voor machine learning.
* Het genereren van RESTfull Services
* Het vereenvoudigen van business processes, waaronder workflow engines zoals BPMN.
* Het uitvoeren van Smart Contracts

#### BPMN Integratie

Under construction.

### Afhankelijkheden

* YamlDotNet [![Build status](https://ci.appveyor.com/api/projects/status/x85wtxbrtckwbo3p/branch/master)](https://ci.appveyor.com/project/aaubry/yamldotnet) **MIT** License<br />
 [YamlDotNet](https://github.com/aaubry/YamlDotNet) is een .NET-bibliotheek voor YAML.
YamlDotNet biedt low-level parsing en emitting van [YAML] (http://www.yaml.org/)
evenals een objectmodel op hoog niveau vergelijkbaar met XmlDocument. Er is ook een serialisatiebibliotheek opgenomen waarmee objecten van en naar 
YAML-streams kunnen worden gelezen en geschreven.<br /> Momenteel ondersteunt YamlDotNet [versie 1.1 van de YAML-specificatie] (http://yaml.org/spec/1.1/).
* dotnet core [![Build status](https://dev.azure.com/dnceng/internal/_apis/build/status/dotnet/sdk/DotNet-Core-Sdk%203.0%20(Windows)%20(YAML)%20(Official))](https://dev.azure.com/dnceng/internal/_build?definitionId=140) **MIT** License<br />
[dotnet core](https://github.com/dotnet/sdk) is een cross platform ontwikkeltaal voor cloud-, IoT- en desktop-apps. 

### Formules

#### Gegevensfuncties

##### lookup('tabelnaam', opzoekwaarde, 'zoekinkolom', 'resultaatuitkolom'))
Retourneert de eerste gevonden resultaatwaarde vanuit de aangegeven resultaat kolomop basis van de opzoekwaarde uit de opgegeven zoekkolomn vanuit opgegeven tabel.

**Voorbeeld** : lookup('woonlandfactoren',woonland,'woonland','factor')

Zoekt in de tabel 'woonlandfactoren' naar de waarde die opgegeven is in de variabele woonland binnen kolom woonlaand en retourneert hierbij de waarde uit de kolom 'factor' als resultaat.

#### Wiskundige functies

##### abs (n)
Retourneert de absolute waarde van een decimaal getal.

##### acos (n)
Retourneert de hoek waarvan de cosinus het opgegeven getal is.

##### asin (n)
Retourneert de hoek waarvan de sinus het opgegeven getal is.

##### atan (n)
Retourneert de hoek waarvan de raaklijn het opgegeven getal is.

##### atan2 (n, n)
Retourneert de hoek waarvan de raaklijn het quotiënt is van twee opgegeven getallen.

##### ceiling(n)	
Retourneert de kleinste integraalwaarde die groter is dan of gelijk is aan het opgegeven drijvende-kommagetal met dubbele precisie.

##### cos (n)
Retourneert de cosinus van de opgegeven hoek.

##### cosh (n)
Retourneert de hyperbolische cosinus van de opgegeven hoek.

##### exp (n)
Retourneert e verhoogd tot het opgegeven vermogen.

##### floor(n)	
Retourneert de grootste integraalwaarde kleiner dan of gelijk aan het opgegeven drijvende-kommagetal met dubbele precisie.

##### IEEERemainder (n, n)
Retourneert de rest die resulteert uit de deling van een opgegeven getal door een ander opgegeven getal.

##### log (n)
Retourneert de natuurlijke (basis e) logaritme van een opgegeven getal.

##### log (n, n)
Retourneert de logaritme van een opgegeven getal in een opgegeven basis.

##### log10 (nl)
Retourneert de 10 logaritme van een opgegeven getal.

##### max (n, n)
Retourneert de grootste van twee dubbele precisie drijvende-kommagetallen.

##### min (n, n)
Retourneert de kleinste van twee dubbele precisie drijvende-kommagetallen.

##### pow (n, n)
Retourneert een opgegeven getal verhoogd tot de opgegeven macht.

##### round (n)
Rondt een drijvende-komma-waarde met dubbele precisie af naar de dichtstbijzijnde integraalwaarde en rondt de middelpuntwaarden naar het dichtstbijzijnde even getal af.

##### round (n, n)
Rondt een drijvende-komma-waarde met dubbele precisie af op een opgegeven aantal fractionele cijfers en rondt de middelpuntwaarden af ​​op het dichtstbijzijnde even getal.

##### round (n, n, MidpointRounding)
Rondt een drijvende-komma-waarde met dubbele precisie af op een opgegeven aantal fractionele cijfers en gebruikt de opgegeven afrondingsconventie voor middelpuntwaarden.

##### round (n, MidpointRounding)
Rondt een dubbele-precisie drijvende-kommawaarde naar het dichtstbijzijnde gehele getal af en gebruikt de opgegeven afrondingsconventie voor middelpuntwaarden.

##### sign(n)	
Retourneert een geheel getal dat het teken van een drijvende-kommagetal met dubbele precisie aangeeft.

##### sin (n)
Retourneert de sinus van de opgegeven hoek.

##### sinh (n)
Retourneert de hyperbolische sinus van de opgegeven hoek.

##### sqrt (n)
Retourneert de vierkantswortel van een opgegeven getal.

##### tan (n)
Retourneert de raaklijn van de opgegeven hoek.

##### tanh (n)
Retourneert de hyperbolische tangens van de opgegeven hoek.

##### truncate (n)
Berekent het integrale deel van een opgegeven drijvende-kommagetal met dubbele precisie. 

## Taxonomie

t.b.a.
