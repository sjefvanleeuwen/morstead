# Virtual Society Morstead

Morstead Virtual Society Rules Demonstrator gehost in Orleans.

## Wat is Morstead?

Morstead is het schaalbare cloud raamwerk voor Virtual Society componenten en wordt gehost op Orleans.

Orleans is een platformonafhankelijk framework voor het bouwen van robuuste, schaalbare gedistribueerde applicaties.

Orleans bouwt voort op de productiviteit van .NET voor ontwikkelaars en brengt het naar de wereld van gedistribueerde applicaties, zoals cloudservices. Orleans schaalt van een enkele lokale server naar wereldwijd gedistribueerde, zeer beschikbare applicaties in de cloud.

Orleans neemt bekende concepten zoals objecten, interfaces, async / await, en probeert / catch en breidt ze uit naar multi-serveromgevingen. Als zodanig helpt het ontwikkelaars die ervaring hebben met single-server applicatie-overgang naar het bouwen van veerkrachtige, schaalbare clouddiensten en andere gedistribueerde applicaties. Om deze reden wordt Orleans vaak "Distributed .NET" genoemd.

Orleans is gemaakt door Microsoft Research en introduceerde het Virtual Actor Model als een nieuwe benadering voor het bouwen van een nieuwe generatie gedistribueerde systemen voor het Cloud-tijdperk. De belangrijkste bijdrage van Orleans is het programmeermodel dat de complexiteit tempt die inherent is aan zeer parallel gedistribueerde systemen zonder de mogelijkheden te beperken of zware beperkingen op te leggen aan de ontwikkelaar.

## Build en run de demontrator in Windows
De eenvoudigste manier om het voorbeeld in Windows te bouwen en uit te voeren, is door het BuildAndRun.ps1 PowerShell-script uit te voeren.

## Build en run de demonstrator op andere platformen
Op andere platforms moet u eerst de NuGet-pakketten bouwen op een Windows-machine (genaamd Build.cmd netstandard) en ze vervolgens beschikbaar maken op het doelplatform, of de pre-release-pakketten gebruiken die zijn gepubliceerd in MyGet (zie https: // dotnet.myget.org/gallery/orleans-prerelease voor meer informatie over het toevoegen van de feed). Voer vervolgens gewoon het bash-script `BuildAndRun.sh` uit.

## Alternatieve stappen voor build en run
Als alternatief kunt je de volgende stappen gebruiken:

#### Compilen
Als je een versie van Orleans probeert te gebruiken die nog niet is uitgebracht, voer dan eerst `Build.cmd netstandard` uit vanuit de hoofdmap van de repository.

Opmerking: als je pakketten opnieuw moet installeren (bijvoorbeeld nadat u de runtime van Orleans hebt gewijzigd en de pakketten opnieuw hebt opgebouwd), verwijdert u handmatig alle pakketten van Orleans uit `(rootfolder) / Samples / {specific-sample} / packages /` en re -run `Build.cmd netstandard`. Mogelijk moet u soms ook de NuGet-cachemap opruimen. Om dat te doen, voer je `dotnet nuget locals all --clear` uit.

U kunt dan zoals gewoonlijk compileren.
```
dotnet restore
```

#### Opstarten vanuit Visual Studio
Vanuit Visual Studio kunt u de SiloHost- en OrleansClient-projecten tegelijkertijd starten (u kunt meerdere opstartprojecten opzetten door met de rechtermuisknop op de oplossing in Solution Explorer te klikken en `Startup-projecten instellen 'te selecteren.

U kunt ook vanaf de opdrachtregel werken:

Om de silo te starten:
```
dotnet run --project src\Vs.Orleans.SiloHost
```

Om de client te starten (u moet een ander opdrachtvenster gebruiken)
```
dotnet run --project src\Vs.Rules.OrleansTestClient\
```

## Morstead Annotatie Systeem

***(under construction)***

Om morstead te kunnen modeleren in de solution architectuur is er een notatiesysteem opgesteld:

## Stateless worker grains


### Stateless Grain

Stateless Worker grains bieden een eenvoudige manier om een automatisch beheerde pool van 
grain activaties te creëren die automatisch opschaalt op basis van de werkelijke belasting.

<img src="./doc/images/grain-worker.svg" width="96"></img>

### Stateless Grain (re-entrant)

Stateless betekent overigens niet dat een workergrain een status kan hebben en uitsluitend
is beperkt tot het uitvoeren van functies. Net als elk ander type grain kan deze een gewenste
status bewaren in het geheugen (re-entrant) (maar persisteert de staat niet op een medium voor langdurig gebruik)
Er is echter geen eenvoudig mechanisme om de de status bij te houden en verschillende
activeringen te coördineren.

<img src="./doc/images/grain-worker-re-entrant.svg" width="96"></img>

### Statefull Grain
<img src="./doc/images/grain-statefull.svg" width="96"></img>

### Observer Grain
<img src="./doc/images/grain-observer.svg" width="96"></img>

Als onderdeel van een silo overzicht en verbindingen tussen grains, anoteren we observer patterns middels
marble diagrams om verder obervable state te visualiseren.




